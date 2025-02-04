using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Diagnostics; // Required for Debug.WriteLine()

public class DatabaseHelper
{
    public static void TestConnection()
    {
        string connString = ConfigurationManager.ConnectionStrings["JobPortalDB"].ConnectionString;

        using (MySqlConnection conn = new MySqlConnection(connString))
        {
                conn.Open();
                Debug.WriteLine("✅ Database Connected Successfully!");  // This will show in Visual Studio Output Window
            
        }
    }
}
