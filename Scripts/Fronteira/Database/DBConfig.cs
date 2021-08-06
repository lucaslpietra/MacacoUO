using System;
using System.Text;
using System.Threading;

public class DBConfig
{
    public static bool Enabled = true;
    public const string DatabaseDriver = "{MySQL ODBC 5.3 Unicode Driver}";
    public const string DatabaseServer = "ultimafronteirashard.com.br";
    public const string DatabaseName = "ultima52_wp57674";
    public const string DatabaseUserID = "ultima52_site";
    public const string DatabasePassword = "tuuetdyg6";
    public static bool UseTransactions = false; // axo q soh vamo le.
    public static bool LoadDataInFile = true;
    public static bool DatabaseNonLocal = (DatabaseServer != "localhost");
    public static Encoding EncodingIO = Encoding.ASCII;
    public static ThreadPriority DatabaseThreadPriority = ThreadPriority.BelowNormal;
 
    public static string CompileConnectionString()
    {
        string connectionString = String.Format("DRIVER={0};SERVER={1};DATABASE={2};UID={3};PASSWORD={4};",
            DatabaseDriver, DatabaseServer, DatabaseName, DatabaseUserID, DatabasePassword);
        return connectionString;
    }
}
