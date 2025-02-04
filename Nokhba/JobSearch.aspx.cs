using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nokhba
{
    public partial class JobSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = "";
            string experience = "0";
            string location = "";

            if (!IsPostBack)
            {
                Fill_Experience_DropDown();

                Page.RouteData.Values.TryGetValue("name", out object nameObj);
                name = nameObj?.ToString() ?? "";

                Page.RouteData.Values.TryGetValue("experience", out object experienceObj);
                experience = experienceObj?.ToString() ?? "0"; //Setting Default Experience To `0`

                Page.RouteData.Values.TryGetValue("location", out object locationObj);
                location = locationObj?.ToString() ?? "";

                JobNameSearchInput.Text = name;
                experienceDropDown.SelectedValue = experience;
                JobLocationInput.Text = location;
                GetJobs(name, int.Parse(experience));
            }
            //FillJobsList();



        }

        protected void Fill_Experience_DropDown()
        {
            DataSet ds = new DataSet();
            DataTable dtFields = new DataTable();
            dtFields.Columns.Add("Years", typeof(int));
            dtFields.Columns.Add("Experience", typeof(string));

            dtFields.Rows.Add(0, "Fresher");

            for (int i = 0; i < 30; i++)
            {
                dtFields.Rows.Add(i + 1, $"{i + 1} Years");
            }

            System.Diagnostics.Debug.WriteLine($"Rows in dtFields: {dtFields.Rows.Count}");


            experienceDropDown.DataSource = dtFields;
            experienceDropDown.DataTextField = "Experience";
            experienceDropDown.DataValueField = "Years";
            experienceDropDown.DataBind();
        }

        protected void OnSearchBtnClicks(object sender, EventArgs e)
        {
            string name = JobNameSearchInput.Text;
            string experience = experienceDropDown.SelectedValue;
            string location = JobLocationInput.Text;
            JobNameSearchInput.Text = "";
            if (string.IsNullOrEmpty(name))
            {
                JobNameSearchInput.Text += "Name Empty";
                name = "default";
            }
            if (string.IsNullOrEmpty(experience))
            {
                JobNameSearchInput.Text += "Exp Empty";
                experience = "1";
            }
            if (string.IsNullOrEmpty(location))
            {
                JobNameSearchInput.Text += "Location Empty";
                location = "0";
            }

            string link = "~/JobSearch/" + Server.UrlEncode(name) +
              "/" + Server.UrlEncode(experience) +
              "/" + Server.UrlEncode(location);
            //JobNameSearchInput.Text = link ?? "Basheer";

            Response.Redirect(link);
        }


        //protected void FillJobsList()
        //{
        //    List<object> jobs = new List<object>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        if(i % 2 == 0)
        //        {
        //            jobs.Add(new { id = i, employer = $"Employer#{i}", title = $"Title#{i}", category = $"Category#{i}", experience = $"{i}", salary = $"{i}00", description = $"Desciption# lm  ml m lm l ml ml m l m lm lm lm l ml m lm lm l ml m lm l m lm l m lm lm lm lm lm  lm ml {i}", date = $"Date#{i}" });
        //        }
        //        else
        //        {
        //            jobs.Add(new { id = i, employer = $"Employer#{i}", title = $"Title#{i}", category = $"Category#{i}", experience = $"{i}", salary = $"{i}00", description = $"Desciption# lm  ml m lm l ml ml m l m lm lm lm l ml m lm lm l ml m lm l m lm l m lm lm lm lm lm  lm ml {i} Desciption# lm  ml m lm l ml ml m l m lm lm lm l ml m lm lm l ml m lm l m lm l m lm lm lm lm lm  lm ml {i} Desciption# lm  ml m lm l ml ml m l m lm lm lm l ml m lm lm l ml m lm l m lm l m lm lm lm lm lm  lm ml {i}", date = $"Date#{i}" });
        //        }

        //    }

        //    JobsList.DataSource = jobs;
        //    JobsList.DataBind();
        //}


        private void GetJobs(string title, int experience)
        {
            string connString = ConfigurationManager.ConnectionStrings["JobPortalDB"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();  // Open the connection
                    Console.WriteLine("Connection opened successfully.");

                    // SQL query to fetch filtered data from the Jobs table
                    string query = "SELECT Jobs.*, Users.FullName AS employer FROM Jobs INNER JOIN Users ON Jobs.EmployerID = Users.UserID WHERE Jobs.Title LIKE @Title AND Jobs.ExperienceRequired <= @Experience;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Adding parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Title", "%" + title + "%");  // Using LIKE for partial matching
                        cmd.Parameters.AddWithValue("@Experience", experience);

                        // Create a DataTable to store query results
                        DataTable dataTable = new DataTable();

                        // Use DataAdapter to fill the DataTable
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }

                        // Check if there are any rows in the DataTable
                        if (dataTable.Rows.Count > 0)
                        {
                            // Bind the DataTable to the DataList control
                            JobsList.DataSource = dataTable;
                            JobsList.DataBind();  // Bind data to the DataList control
                        }
                        else
                        {
                            Console.WriteLine("No jobs found with the specified filters.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }



    }
}