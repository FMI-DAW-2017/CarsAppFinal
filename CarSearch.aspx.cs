using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && Request.Params["q"] != null)
        {
            string query = Server.UrlDecode(Request.Params["q"]);

            SDSSearch.SelectCommand = "SELECT Cars.Id, Cars.Model, Cars.Max_Speed, Cars.Power, Cars.Color, Cars.fabrication_Date, Brands.Name FROM Brands INNER JOIN Cars ON Brands.Id = Cars.brand_id"
                + " WHERE Name LIKE @q OR Model LIKE @q";

            SDSSearch.SelectParameters.Clear();
            SDSSearch.SelectParameters.Add("q", query + "%");
            SDSSearch.DataBind();
        }
    }
}