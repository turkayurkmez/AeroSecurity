using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InjectionAttack
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonEkle_Click(object sender, EventArgs e)
        {
            Label1.Text = TextBoxComment.Text;
        }
    }
}