using System;
using System.Collections.Generic;
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
            if (!IsPostBack)
            {
                Fill_Experience_DropDown();

                Page.RouteData.Values.TryGetValue("name", out object nameObj);
                string name = nameObj?.ToString() ?? "";

                Page.RouteData.Values.TryGetValue("experience", out object experienceObj);
                string experience = experienceObj?.ToString() ?? "0"; //Setting Default Experience To `0`

                Page.RouteData.Values.TryGetValue("location", out object locationObj);
                string location = locationObj?.ToString() ?? "";


                JobNameSearchInput.Text = name;
                experienceDropDown.SelectedValue = experience;
                JobLocationInput.Text = location;
            }
            FillJobsList();



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


        protected void FillJobsList()
        {
            List<object> jobs = new List<object>();
            for (int i = 0; i < 10; i++)
            {
                jobs.Add(new { id = i, employer = $"Employer#{i}", title = $"Title#{i}", category = $"Category#{i}", experience = $"{i}", salary = $"{i}00", description = $"Desciption#{i}", date = $"Date#{i}" });
            }

            JobsList.DataSource = jobs;
            JobsList.DataBind();
        }



    }
}