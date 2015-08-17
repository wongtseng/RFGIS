using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication4
{
    public class wongtsengDB
    {
        #region       执行查询SQL语句,返回DS
        /// <summary>
        /// 执行查询SQL语句,返回DS
        /// </summary>
        /// <param name="commandString">需要执行的SQL语句</param>
        /// <returns></returns>
       
        public DataSet getDS(string commandString)
        {

            string connectonString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectonString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = commandString;
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;

            }
            catch (Exception e)
            {
                return null;
                // result = "false@" + e.Message;
            }
            finally
            {
                sqlConnection.Close();
            } 
        
        
        
        }
        #endregion

        #region  执行插入语更新语句,返回提示信息
        public string insertandUpdate( string commandString)
        {
            string result = null; ;
            string connectonString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
          //  string commandString = String.Format("SELECT username FROM t_user where username='{0}'", username);
            SqlConnection sqlConnection = new SqlConnection(connectonString);
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = commandString;
                int num = command.ExecuteNonQuery();
                if (num>0)
                result = "true@执行成功";
                else
                    result = "false@未找到值";
                sqlConnection.Close();

            }
            catch (Exception e)
            {
                result = "false@" + e.Message;
            }
            finally
            {
                sqlConnection.Close();
            }

            return result;
        }
        #endregion

        #region  页面提示
        public  void Show(System.Web.UI.Page page, string msg)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
    }
        #endregion

        #region    获取工事的名称与编号
        public DataSet  getrfinfo()
        {
            string commd = "select id,name from t_AirDefenseInfo";
            return getDS(commd);

        }
        #endregion

        #region    获取入口损毁的描述与编号     
        public DataSet getendemangeinfo()
        {
            string commd = "select id,text from t_damageType";
            return getDS(commd);

        }
        #endregion

        #region     用户登录验证
        public string userlogin(string un, string pw)
        {
            string commd = "select username,pw,usertype from t_SysUser";
         
            DataSet ds=getDS(commd);
            int count = 0;
            string UserInfo = "Flase@null@null";
            if(ds!=null)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        string sun = ds.Tables[0].Rows[i][0].ToString();
                        string spw = ds.Tables[0].Rows[i][1].ToString();
                        if (sun == un & spw == pw)
                        { 
                            UserInfo = "true@" + ds.Tables[0].Rows[i][0].ToString() + "@" + ds.Tables[0].Rows[i][2].ToString();   ///获取到用户名\获取到用户类型
                            break;
                        }
                    }
                }

            return UserInfo;
        
        
        
        
        }



        #endregion


    }
}