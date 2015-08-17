using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public partial class Login : System.Web.UI.Page
    {
        wongtsengDB dbkit = new wongtsengDB();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
         

             string un = tb_un.Text;
             string pw = tb_pw.Text;

             string result = dbkit.userlogin(un, pw);

             string[] results = result.Split('@');

             if (results[0] == "true")
             {
                 Session["userName"] = results[1];
                 Session["userType"] = results[2];
                 this.Response.Redirect("Default.aspx");
             }




        }
    }
}