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
            var fromAddress = new MailAddress("nokhba121@example.com", "NOKHBA");
            var toAddress = new MailAddress(email);
            const string subject = "Verification Code";
            string body = $"Your verification code is: {code}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("nokhba121@example.com", "RNTS121@")
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string name = nameTextbox.Text;
            string email = emailTextbox.Text;
            string password = passwordTextbox.Text;
            string confirmPassword = confirmPasswordTextbox.Text;
            string role = UserRoleDropDownList.SelectedValue;

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

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["JobPortalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                using (SqlCommand checkCommand = new SqlCommand(checkUserQuery, conn))
                {
                    checkCommand.Parameters.AddWithValue("@Email", email);
                    int userCount = (int)checkCommand.ExecuteScalar();
                    if (userCount > 0)
                    {
                        lblMessage.Text = "Email already registered";
                        return;
                    }
                }
                string insertUserQuery = "INSERT INTO Users (FullName, Email, PasswordHash, Role) VALUES (@Name, @Email, @Password, @Role)";
                using (SqlCommand insertCommand = new SqlCommand(insertUserQuery, conn))
                {
                    insertCommand.Parameters.AddWithValue("@Name", name);
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@Password", hashedPassword);
                    insertCommand.Parameters.AddWithValue("@Role", role);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        SendVerificationEmail(email, verificationCode);
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Registration successful!";
                        Response.Redirect($"VerifyEmail.aspx ? email ={ email}");
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