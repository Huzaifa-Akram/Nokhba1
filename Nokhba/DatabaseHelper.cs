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
            try
            {
                conn.Open();
                Debug.WriteLine("✅ Database Connected Successfully!");  // This will show in Visual Studio Output Window
            }
            catch (Exception ex)
            {
                Debug.WriteLine("❌ Connection Failed: " + ex.Message);
            }
        }
    }
}
