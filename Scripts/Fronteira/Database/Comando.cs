using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.Database
{
    public class ComandoLogin
    {
        private string login;
        private string pass;
        private Action<string, bool> callback;

        public ComandoLogin(string login, string pass, Action<string, bool> callback)
        {
            this.login = login;
            this.pass = pass;
        }

        public void Executa(OdbcCommand cmd)
        {
            cmd.CommandText = "select user_pass from wpvu_users where user_name == '"+login+"'";
            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                var hashDaSenha = reader.GetString(0);
                if(WordpressHashes.Correct(this.pass, hashDaSenha))
                {
                    callback(login, true);
                    return;
                }
            }
            callback(login, false);
        }
    }
}
