using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace regiscrud
{
    public partial class registration : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ASUS;Initial Catalog=TestOp;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayData();
            }
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("spemployee", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Options", 11);
            cmd.Parameters.AddWithValue("@first_name", TextBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@last_name", TextBox2.Text.Trim());
            cmd.Parameters.AddWithValue("@branch", DropDownList1.Text.Trim());
            cmd.Parameters.AddWithValue("@enroll", TextBox3.Text.Trim());
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Label5.Text = "Successfully Submitted";
                Label5.ForeColor = System.Drawing.Color.Green;
                conn.Close();
                DisplayData();
            }
            else
            {
                Label5.Text = "Error Occured While Submitting !";
                Label5.ForeColor = System.Drawing.Color.Red;
                conn.Close();
            };
        }
        private void DisplayData()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("spemployee", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Options", 41);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            conn.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            DropDownList1.Text = "-SELECT-";
            TextBox3.Text = "";
            Label5.Text = "";
        }


        protected void GridView1_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            conn.Open();
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            SqlCommand cmd = new SqlCommand("spemployee", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Options", 31);
            cmd.Parameters.AddWithValue("ID", SqlDbType.Int).Value = ID;
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Label5.Text = "Successfully Deleted";
                Label5.ForeColor = System.Drawing.Color.Green;
                conn.Close();
                DisplayData();
            }
            else
            {
                Label5.Text = "Error Occured While Deleting !";
                Label5.ForeColor = System.Drawing.Color.Red;
                conn.Close();
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            conn.Open();
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            SqlCommand cmd = new SqlCommand("spemployee", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Options", 21);
            cmd.Parameters.AddWithValue("@first_name", ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString());
            cmd.Parameters.AddWithValue("@last_name", ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString());
            cmd.Parameters.AddWithValue("@branch", ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString());
            cmd.Parameters.AddWithValue("@enroll", ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString());
            cmd.Parameters.AddWithValue("ID", SqlDbType.Int).Value = ID;
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Label5.Text = "Successfully Updated !";
                Label5.ForeColor = System.Drawing.Color.Green;
                conn.Close();
                GridView1.EditIndex = -1;
                DisplayData();
            }
            else
            {
                Label5.Text = "Error Occured While Updating !";
                Label5.ForeColor = System.Drawing.Color.Red;
                conn.Close();
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            DisplayData();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            DisplayData();
        }
    }
}