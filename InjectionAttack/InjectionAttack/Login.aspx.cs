using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InjectionAttack
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\Mssqllocaldb;Initial Catalog=injectTest;Integrated Security=True;Pooling=False");

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users WHERE UserName=@name AND Password=@pass",sqlConnection);

            sqlCommand.Parameters.AddWithValue("@name", TextBoxUserName.Text);
            sqlCommand.Parameters.AddWithValue("@pass", TextBoxPassword.Text);

            sqlConnection.Open();
            var reader = sqlCommand.ExecuteReader();
           
            if (reader.Read())
            {
                Session["user"] = reader["UserName"].ToString();
                Session["requestId"] = Guid.NewGuid().ToString();
                //ViewState["requestId"] = 
                //Response.Write(Session["requestId"].ToString());
            }
            sqlConnection.Close();
            if (Session["user"]!= null)
            {
                Label1.Text = "Hoş geldiniz";               
            }
            else
            {
                Label1.Text = "Hatalı kullanıcı......";
            }

                

        }
    }
}