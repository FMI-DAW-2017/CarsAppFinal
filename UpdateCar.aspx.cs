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

                string query = "SELECT brand_id, Model, Max_Speed, Power, Color, Fabrication_Date"
                    + " FROM Cars"
                    + " WHERE Id = @id";

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\mssqllocaldb;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

                con.Open();

                try
                {
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("id", ID);

                    SqlDataReader reader = com.ExecuteReader();

                    while (reader.Read())
                    {
                        DDLBrand.SelectedValue = reader["brand_id"].ToString();
                        TBModel.Text = reader["Model"].ToString();
                        TBMaxSpeed.Text = reader["Max_Speed"].ToString();
                        TBPower.Text = reader["Power"].ToString();
                        TBColor.Text = reader["Color"].ToString();
                        TBFDate.Text = DateTime.Parse(reader["Fabrication_Date"].ToString()).ToShortDateString();
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

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid && Request.Params["id"] != null)
        {
            try
            {
                int ID = int.Parse(Request.Params["id"].ToString());

                int IDBrand = int.Parse(DDLBrand.SelectedValue);
                string model = TBModel.Text;
                float maxSpeed = float.Parse(TBMaxSpeed.Text);
                float horsePower = float.Parse(TBPower.Text);
                string color = TBColor.Text;
                DateTime fabricationDate = DateTime.Parse(TBFDate.Text);

                string query = "UPDATE Cars SET brand_id = @id_brand, Model = @model, Max_Speed = @max_speed, Power = @power, Color = @color, Fabrication_Date = @f_date"
                    + " WHERE Id = @id";

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\mssqllocaldb;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

                con.Open();

                try
                {
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("id_brand", IDBrand);
                    com.Parameters.AddWithValue("model", model);
                    com.Parameters.AddWithValue("max_speed", maxSpeed);
                    com.Parameters.AddWithValue("power", horsePower);
                    com.Parameters.AddWithValue("color", color);
                    com.Parameters.AddWithValue("f_date", fabricationDate);
                    com.Parameters.AddWithValue("id", ID);

                    com.ExecuteNonQuery();

                    // Do this only if update works:
                    LAnswer.Text = "Car saved successfully!";
                }
                catch (Exception ex)
                {
                    LAnswer.Text = "Database update error : " + ex.Message;
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
}