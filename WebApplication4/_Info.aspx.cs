using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] == null )
            Response.Write("<script language=javascript>parent.location.href='Login.aspx';</script>");
            else
                if(Session["userType"].ToString() != "系统管理员")
                 Response.Redirect("_Map.aspx");

          
        }
    }
}