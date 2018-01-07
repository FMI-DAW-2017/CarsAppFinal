using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false && Request.Params["id"] != null)
        {
            try
            {
                int ID = int.Parse(Request.Params["id"].ToString());

                string query = "SELECT Brands.Name, Model"
                    + " FROM Cars INNER JOIN Brands ON Cars.brand_id = Brands.Id"
                    + " WHERE Cars.Id = @id";

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\mssqllocaldb;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

                con.Open();

                try
                {
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("id", ID);

                    SqlDataReader reader = com.ExecuteReader();

                    while (reader.Read())
                    {
                        LBrand.Text = reader["Name"].ToString();
                        LModel.Text = reader["Model"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    LAnswer.Text = "Database select error : " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            catch (Exception se)
            {
                LAnswer.Text = "Database connexion error : " + se.Message;
            }
        }
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && Request.Params["id"] != null)
        {
            try
            {
                int ID = int.Parse(Request.Params["id"].ToString());

                string query = "DELETE FROM Cars WHERE Id = @id";

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\mssqllocaldb;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

                con.Open();

                try
                {
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("id", ID);

                    com.ExecuteNonQuery();

                    // Do this only if delete works:
                    PConfirm.Visible = false;
                    HLHomepage.Visible = true;
                    LAnswer.Text = "Car deleted successfully!";
                }
                catch (Exception ex)
                {
                    LAnswer.Text = "Database delete error : " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            catch (SqlException se)
            {
                LAnswer.Text = "Database connexion error : " + se.Message;
            }
            catch (Exception ex)
            {
                LAnswer.Text = "Data conversion error : " + ex.Message;
            }
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }
}