using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebApplication4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        wongtsengDB dbkit = new wongtsengDB();
        ClientSocket mysocket = new ClientSocket();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] == null)
                Response.Write("<script language=javascript>parent.location.href='Login.aspx';</script>");
            string commandString = "SELECT wsurl FROM t_wsurl where id= '1'";
            DataSet ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                wsurl.Value = ds.Tables[0].Rows[0][0].ToString();
            }
             commandString = "SELECT id,wsurl FROM t_AirDefenseInfo";
             ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                  string result=string.Empty;
                int num = ds.Tables[0].Rows.Count;
                for(int i=0;i<num;i++)
                {
                    if (i != num - 1)
                        result += ds.Tables[0].Rows[i][0] + "$" + ds.Tables[0].Rows[i][1] + "@";
                    else
                        result += ds.Tables[0].Rows[i][0] + "$" + ds.Tables[0].Rows[i][1];
                }
              dataurl.Value = result;
            }

          //  Check_capacity();
        }

        #region      查询数据库，关注各人防工事的可用库容
        public string Check_capacity()
        {

            //string commandString = "SELECT capacity,capacityleft,address,ip,PortNum FROM t_AirDefenseInfo";
            //DataSet ds = dbkit.getDS(commandString);
            //if (ds != null)
            //{float
            //    string result = string.Empty;
            //    int num = ds.Tables[0].Rows.Count;
            //    int capacity, capacityleft, PortNum;
            //    string ip, address;
            //    for (int i = 0; i < num; i++)
            //    {
            //        capacity =int.Parse( ds.Tables[0].Rows[i][0].ToString());
            //        capacityleft =int.Parse(ds.Tables[0].Rows[i][1].ToString());
            //        address = ds.Tables[0].Rows[i][2].ToString();
            //        ip = ds.Tables[0].Rows[i][3].ToString();
            //        PortNum =int.Parse( ds.Tables[0].Rows[i][4].ToString());
            //            mysocket.checkCapacity(ip, PortNum, capacity, capacityleft, false);
                   
            //    }

            //    dataurl.Value = result;

          //  }


          return "OK";
        }
        #endregion

    }
}