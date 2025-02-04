using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Nokhba
{
    public partial class VerifyEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = Request.QueryString["email"];
            if (email == null)
            {
                Response.Redirect("Registration.aspx");
            }
            else
            {
                lblUserEmail.Text = email;
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            string email = Request.QueryString["email"];
            string enteredCode = txtVerificationCode.Text;

            string connString = ConfigurationManager.ConnectionStrings["JobPortalDB"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT VerificationCode FROM USERS WHERE Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("Email", email);
                    string verificationCode = Convert.ToString(cmd.ExecuteScalar());
                    if (verificationCode != null)
                    {
                        if (verificationCode == enteredCode)
                        {
                            string updateQuery = "UPDATE Users SET IsVerified = 1 WHERE Email = @Email";
                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("Email", email);
                                updateCmd.ExecuteNonQuery();
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                                lblMessage.Text = "Your email has been verified.";
                                Response.AddHeader("REFRESH", "2;URL=Login.aspx");
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Invalid verification code";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Invalid email";
                    }
                }
            }
        }

        protected void btnResendCode_Click(object sender, EventArgs e)
        {
            string email = Request.QueryString["email"];
            string newCode = Guid.NewGuid().ToString().Substring(0, 8);

            string connString = ConfigurationManager.ConnectionStrings["NokhbaDB"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string updateQuery = "UPDATE Users SET VerificationCode = @Code WHERE Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Code", newCode);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }

            SendVerificationEmail(email, newCode);
            lblMessage.Text = "A new verification code has been sent!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
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

    }
}