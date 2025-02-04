using System;
using BCrypt.Net;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace Nokhba
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //logic for initial load
            }
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public void SendVerificationEmail(string email, string code)
        {
            try
            {
                // Retrieve credentials from Web.config
                string smtpEmail = ConfigurationManager.AppSettings["SMTPEmail"];
                string smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];

                var fromAddress = new MailAddress(smtpEmail, "Nokhba"); // App Name as Sender
                var toAddress = new MailAddress(email);

                const string subject = "Nokhba - Email Verification";
                string body = $"<h3>Hello,</h3><p>Your verification code is: <strong>{code}</strong></p><p>Please enter this code to verify your email.</p>";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587, // Use 465 if SSL
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtpEmail, smtpPassword) // Securely retrieved
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Enables HTML formatting
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email sending failed: " + ex.Message);
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string name = nameTextbox.Text;
            string email = emailTextbox.Text;
            string password = passwordTextbox.Text;
            string confirmPassword = confirmPasswordTextbox.Text;
            string role = UserRoleDropDownList.SelectedValue.Trim().Replace(" ", "");

            if (password != confirmPassword)
            {
                lblMessage.Text = "Password do not match";
                return;
            }
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) ||
                string.IsNullOrEmpty(role))
            {
                lblMessage.Text = "Please fill all fields";
                return;
            }
            if (!IsValidEmail(email))
            {
                lblMessage.Text = "Invalid email";
                return;
            }
            if (password.Length < 6)
            {
                lblMessage.Text = "Password must be at least 6 characters";
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            string verificationCode = Guid.NewGuid().ToString().Substring(0, 8);

            string connString = ConfigurationManager.ConnectionStrings["JobPortalDB"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                using (MySqlCommand checkCommand = new MySqlCommand(checkUserQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@Email", email);
                    int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (userCount > 0)
                    {
                        lblMessage.Text = "Email already registered";
                        return;
                    }
                }
                string insertUserQuery = "INSERT INTO Users (FullName, Email, Password, Role, IsVerified, VerificationCode) VALUES (@Name, @Email, @Password, @Role, 0, @VerificationCode)";
                using (MySqlCommand insertCommand = new MySqlCommand(insertUserQuery, conn))
                {
                    insertCommand.Parameters.AddWithValue("@Name", name);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@Password", hashedPassword);
                    insertCommand.Parameters.AddWithValue("@Role", role);
                    insertCommand.Parameters.AddWithValue("@VerificationCode", verificationCode);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        SendVerificationEmail(email, verificationCode);
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Registration successful!";
                        Response.Redirect($"VerifyEmail.aspx?email={Server.UrlEncode(email)}");
                    }
                    else
                    {
                        lblMessage.Text = "Registration failed. Please try again.";
                    }
                }

            }
        }
    }
}