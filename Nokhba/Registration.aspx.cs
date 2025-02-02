using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nokhba
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillUserTypeDropDown();
            }
        }

        protected void FillUserTypeDropDown()
        {
            DataSet ds = new DataSet();
            DataTable dtFields = new DataTable();
            dtFields.Columns.Add("Role", typeof(string));

            dtFields.Rows.Add("User");
            dtFields.Rows.Add("Employer");

            UserRoleDropDownList.DataSource = dtFields;
            UserRoleDropDownList.DataTextField = "Role";
            UserRoleDropDownList.DataValueField = "Role";
            UserRoleDropDownList.DataBind();
        }


    }
}