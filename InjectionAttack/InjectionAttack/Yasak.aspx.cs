using InjectionAttack.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InjectionAttack
{
    public partial class Yasak : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Write("Giremezsin!!!!");
                Response.Redirect("/");
            }

            AntiForgeryToken.Check(this, HiddenField1);
        }
    }
}