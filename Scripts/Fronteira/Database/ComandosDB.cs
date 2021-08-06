using System;
using System.Collections;
using System.Data.Odbc;
using System.Threading;

namespace Server.Fronteira.Database
{
    public class ComandosDB
    {
        private static ComandosDB _i;

        public static void Configure()
        {
            Console.WriteLine("-- Iniciando banco de dados --");
            _i = new ComandosDB("Comando em {0:F1} segundos", "Thread de Comandos do banco");
        }

        private readonly Queue m_Queue;
        private readonly ManualResetEvent m_Sync;
        private readonly Thread m_Thread;
        private readonly string m_CompletionString;
        private readonly string m_ConnectionString;
        private bool m_HasCompleted;
        public ComandosDB(string completionString, string threadName)
            : this(DBConfig.CompileConnectionString(), completionString, threadName)
        {
        }

        public ComandosDB(string connectionString, string completionString, string threadName)
        {
            this.m_CompletionString = completionString;
            this.m_ConnectionString = connectionString;

            this.m_Queue = Queue.Synchronized(new Queue());

            this.m_Queue.Enqueue(null); // signal connect

            this.m_Sync = new ManualResetEvent(true);

            this.m_Thread = new Thread(new ThreadStart(Thread_Start));
            this.m_Thread.Name = threadName;
            this.m_Thread.Priority = DBConfig.DatabaseThreadPriority;
            this.m_Thread.Start();
        }

        public bool HasCompleted
        {
            get
            {
                return this.m_HasCompleted;
            }
        }
        public void Enqueue(object obj)
        {
            lock (this.m_Queue.SyncRoot)
            {
                this.m_Queue.Enqueue(obj);
                try
                {
                    this.m_Sync.Set();
                }
                catch
                {
                }
            }
        }

        private void Thread_Start()
        {
            bool connected = false;

            OdbcConnection connection = null;
            OdbcCommand command = null;
            OdbcTransaction transact = null;

            DateTime start = DateTime.UtcNow;

            bool shouldWriteException = true;

            while (true)
            {
                this.m_Sync.WaitOne();

                while (this.m_Queue.Count > 0)
                {
                    try
                    {
                        object obj = this.m_Queue.Dequeue();

                        if (obj == null)
                        {
                            if (connected)
                            {
                                if (transact != null)
                                {
                                    try
                                    {
                                        transact.Commit();
                                    }
                                    catch (Exception commitException)
                                    {
                                        Console.WriteLine("DB: Erro no commit");
                                        Console.WriteLine(commitException);

                                        try
                                        {
                                            transact.Rollback();
                                            Console.WriteLine("DB: Dando rollback");
                                        }
                                        catch (Exception rollbackException)
                                        {
                                            Console.WriteLine("DB: Erro dando rollback");
                                            Console.WriteLine(rollbackException);
                                        }
                                    }
                                }

                                try
                                {
                                    connection.Close();
                                }
                                catch
                                {
                                }

                                try
                                {
                                    connection.Dispose();
                                }
                                catch
                                {
                                }

                                try
                                {
                                    command.Dispose();
                                }
                                catch
                                {
                                }

                                try
                                {
                                    this.m_Sync.Close();
                                }
                                catch
                                {
                                }

                                Console.WriteLine(this.m_CompletionString, (DateTime.UtcNow - start).TotalSeconds);
                                this.m_HasCompleted = true;

                                return;
                            }
                            else
                            {
                                try
                                {
                                    connected = true;
                                    connection = new OdbcConnection(this.m_ConnectionString);
                                    connection.Open();
                                    command = connection.CreateCommand();
                                    if (DBConfig.UseTransactions)
                                    {
                                        transact = connection.BeginTransaction();
                                        command.Transaction = transact;
                                    }
                                }
                                catch (Exception e)
                                {
                                    try
                                    {
                                        if (transact != null)
                                            transact.Rollback();
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        if (connection != null)
                                            connection.Close();
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        if (connection != null)
                                            connection.Dispose();
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        if (command != null)
                                            command.Dispose();
                                    }
                                    catch
                                    {
                                    }

                                    try
                                    {
                                        this.m_Sync.Close();
                                    }
                                    catch
                                    {
                                    }

                                    Console.WriteLine("DB: Nao conectou no DB");
                                    Console.WriteLine(e);
                                    this.m_HasCompleted = true;
                                    return;
                                }
                            }
                        }
                        else if (obj is string)
                        {
                            command.CommandText = (string)obj;
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            string[] parms = (string[])obj;

                            command.CommandText = parms[0];

                            if (command.ExecuteScalar() == null)
                            {
                                command.CommandText = parms[1];
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (shouldWriteException)
                        {
                            Console.WriteLine("DB: Erro na thread");
                            Console.WriteLine(e);
                            shouldWriteException = false;
                        }
                    }
                }

                lock (this.m_Queue.SyncRoot)
                {
                    if (this.m_Queue.Count == 0)
                        this.m_Sync.Reset();
                }
            }
        }
    }
}
