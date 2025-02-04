using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Nokhba
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
            RunInitialQueries();
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "JobSearchRoute",  // Route Name
                "JobSearch/{name}/{experience}/{location}", // URL Pattern
                "~/JobSearch.aspx", // Target Page
                false,
                new RouteValueDictionary { { "name", "" }, { "experience", "0" }, { "location", "" } }
            );
        }


        public static void RunInitialQueries()
        {
            string connString = ConfigurationManager.ConnectionStrings["JobPortalDB"].ConnectionString;
            string connStringWithoutDB = connString.Split(';')[0] + ';' + connString.Split(';')[2] + ';' + connString.Split(';')[3];

            using (MySqlConnection conn = new MySqlConnection(connStringWithoutDB))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS JobPortal;", conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"✅ JobPortal is ready.");
                }
            }


            string[] queries = new string[]
            {
                // Users Table
                @"CREATE TABLE IF NOT EXISTS Users (
                    UserID INT AUTO_INCREMENT PRIMARY KEY,
                    FullName VARCHAR(255) NOT NULL,
                    Email VARCHAR(255) UNIQUE NOT NULL,
                    Password VARCHAR(255) NOT NULL,
                    Role ENUM('JobSeeker', 'Employer') NOT NULL,
                    IsVerified BIT DEFAULT 0,
                    VerificationCode VARCHAR(50)
                );",

                // Jobs Table
                @"CREATE TABLE IF NOT EXISTS Jobs (
                    JobID INT AUTO_INCREMENT PRIMARY KEY,
                    EmployerID INT,
                    Title VARCHAR(255) NOT NULL,
                    Category VARCHAR(100) NOT NULL,
                    ExperienceRequired INT,
                    Salary DECIMAL(10,2),
                    JobDescription TEXT,
                    DatePosted TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (EmployerID) REFERENCES Users(UserID) ON DELETE CASCADE
                );",

                // Applications Table
                @"CREATE TABLE IF NOT EXISTS Applications (
                    ApplicationID INT AUTO_INCREMENT PRIMARY KEY,
                    JobID INT,
                    UserID INT,
                    CoverLetter TEXT,
                    DateApplied TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (JobID) REFERENCES Jobs(JobID) ON DELETE CASCADE,
                    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
                );",

                // Chat Table
                @"CREATE TABLE IF NOT EXISTS Chat (
                    MessageID INT AUTO_INCREMENT PRIMARY KEY,
                    SenderID INT,
                    ReceiverID INT,
                    Message TEXT,
                    Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (SenderID) REFERENCES Users(UserID) ON DELETE CASCADE,
                    FOREIGN KEY (ReceiverID) REFERENCES Users(UserID) ON DELETE CASCADE
                );"
            };

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    foreach (var query in queries)
                    {
                        cmd.CommandText = query;
                        try
                        {
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Executed: " + query.Split('(')[0]); // Display table creation success
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error executing query: " + ex.Message);
                        }
                    }
                }
            }


        }



    }
}