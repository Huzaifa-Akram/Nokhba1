using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nokhba
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Fill_Experience_DropDown();
            }


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

        protected void OnSearchBtnClick(object sender, EventArgs e)
        {
            string name = JobNameSearchInput.Text;
            string experience = experienceDropDown.SelectedValue;
            string location = JobLocationInput.Text;

            string link = GetRouteUrl("JobSearchRoute", new { name, experience, location });

            Response.Redirect(link);
        }

    }
}