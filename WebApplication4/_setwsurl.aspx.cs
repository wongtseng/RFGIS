using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public partial class _setwsurl : System.Web.UI.Page
    {
        wongtsengDB dbkit = new wongtsengDB();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] == null || Session["userType"].ToString() != "系统管理员")
                Response.Write("<script language=javascript>parent.location.href='Login.aspx';</script>");   //如果不符合要求,主页面进行跳转.
            else
                if (Session["userType"].ToString() != "系统管理员")
                    Response.Redirect("_Map.aspx");


            if (!IsPostBack)
            {
                string commandString = "SELECT wsurl FROM t_wsurl where id= '1'";
                DataSet ds = dbkit.getDS(commandString);
                if (ds != null)
                {
                    tb_wsurl.Text = ds.Tables[0].Rows[0][0].ToString();
                }
            }
        }

        #region   设置WEB服务地址
        protected void btn_ok_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            try
            {
                if ( tb_wsurl.Text!= "")
                {                                                                    

                    string commandString = String.Format("update t_wsurl set wsurl='{0}' where id='1'", tb_wsurl.Text);
                    result = dbkit.insertandUpdate(commandString);
                    if (result == "true@执行成功")
                    {
                        result = "修改成功";
                    }
                    else
                        result = "修改失败,因为:" + result.Split('@')[1];
                }

            }

            catch (Exception ee)
            {
                result = ee.Message;
            }
            finally
            {
                dbkit.Show(this, result);
            }

           



        }
        #endregion
    }
}