using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InjectionAttack.Security
{
    public static class AntiForgeryToken
    {
        public static void Check(Page page, HiddenField hiddenField)
        {
            if (!page.IsPostBack)
            {
                Guid token = Guid.NewGuid();
                hiddenField.Value = token.ToString();
                page.Session["Token"] = token;
            }
            else
            {
                Guid server = (Guid)page.Session["Token"];
                Guid client = new Guid(hiddenField.Value);
                if (client != server)
                {
                    throw new SecurityException("CSRF Atağı algılandı!");
                }

            }
        }
    }
}