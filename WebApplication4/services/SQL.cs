using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ws_rfgis
{
    public class SQL
    {

        /// <summary>
        /// 检查重名的方法
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>true/false@message</returns>
        public string checkusername(string username)
        {
       
            string commandString = String.Format("SELECT username FROM t_user where username='{0}'", username);
       
            try
            {

                DataSet ds = getDS(commandString);          
                int num = ds.Tables[0].Rows.Count;
                if (num > 0)
                    return "false@用户名已存在";
                else
                    return "true@可以注册";             

            }
            catch (Exception e)
            {
                return ("false@" + e.Message);
            }


        
        }

        public string checkpsw(string username,string psw)
        {
         
            string commandString = String.Format("SELECT psw,guid FROM t_user where username='{0}'", username);  
            try
            {
                DataSet ds = getDS(commandString);
                if (ds.Tables[0].Rows.Count == 0)
                    return "false@用户名不存在@NONE";
                else {
                    string tpsw = ds.Tables[0].Rows[0]["psw"].ToString();
                    string guid = ds.Tables[0].Rows[0]["guid"].ToString();
                    if (tpsw == psw2md5(psw))
                        return "true@登录成功@" + guid;
                    else
                        return "false@用户名或密码错误@NONE";
                }
               
            }
            catch (Exception e)
            {
                return ("false@" + e.Message+"@NONE");
            }



        }

        public bool checkGUID(string GUID)
        {
            string commandString = String.Format("SELECT GUID FROM t_user where GUID='{0}'", GUID);

            try
            {

                DataSet ds = getDS(commandString);
                int num = ds.Tables[0].Rows.Count;
                if (num ==1)
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {
                return false;
            }
        
        }


        #region      密码转换为MD5
        public string psw2md5(string psw)
        {
            byte[] pswmd5byte = Encoding.Default.GetBytes(psw.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(pswmd5byte);
            string pswmd5 = BitConverter.ToString(output).Replace("-", "");
            return pswmd5;
        }
        #endregion

        #region   获取详细信息
        public string getinfo()
        {
             string result=string.Empty;
            string connectonString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string commandString = String.Format("SELECT ID,name,lng,lat,NO,capacity,peoplenum,capacityleft,damageinfo FROM t_AirDefenseInfo");
            string updatetime = string.Empty;
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
                int num = ds.Tables[0].Rows.Count;
                if (num >0)
                {
                    for(int i=0;i<num;i++)
                    {
                        for(int j=0;j<9;j++)
                        {
                              result+= ds.Tables[0].Rows[i][j];
                            if(j!=8)
                                result+= "&";
                        
                        }
                        if (i != num-1)
                         result+="#";

                    }
                    return result;

                }
               
                else
                    return "none";

            }
            catch (Exception e)
            {
                return ("false@" + e.Message);
            }
            finally
            {
                sqlConnection.Close();
               
            }
        
        
        
        
        
        
        
        }
        #endregion

        #region   获取单个点详细信息
        public string getoneinfo(string id)
        {
            string result = string.Empty;
            string connectonString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string commandString = String.Format("SELECT ID,name,lng,lat,NO,capacity,peoplenum,capacityleft,damageinfo FROM t_AirDefenseInfo where ID='{0}'",id);
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
                int num = ds.Tables[0].Rows.Count;
                if (num > 0)
                {
                    result = "true@查询成功@";
                    for (int i = 0; i < 9; i++)
                    {
                     
                            result += ds.Tables[0].Rows[0][i];
                            if (i !=8)
                                result += "&";
                    }
                    return result;

                }

                else
                    return "false@未找对应的人防工事信息" ;

            }
            catch (Exception e)
            {
                return ("false@" + e.Message);
            }
            finally
            {
                sqlConnection.Close();

            }







        }
        #endregion

        #region   获取位置信息
        public string getlocation()
        {
            string result = string.Empty;
            string connectonString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string commandString = String.Format("SELECT lng,lat FROM t_user ");
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
                int num = ds.Tables[0].Rows.Count;
                if (num > 0)
                {
                    for (int i = 0; i < num; i++)
                    {
                        result += ds.Tables[0].Rows[i][0] +"&"+ ds.Tables[0].Rows[i][1];
                        if (i != num - 1)
                            result += "@";
                    }
                    return result;

                }

                else
                    return "false";

            }
            catch (Exception e)
            {
                return ("false");
            }
            finally
            {
                sqlConnection.Close();

            }







        }
        #endregion

        #region   获取摘要信息
        public string getinfosum()
        {
            string result = string.Empty;
            string connectonString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string commandString = String.Format("SELECT ID,name,lng,lat,capacity,capacityleft FROM t_AirDefenseInfo");
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
                if (ds == null)
                    return "none";
                int num = ds.Tables[0].Rows.Count;
                if (num > 0)
                {
                    for (int i = 0; i < num; i++)
                    {

                        for (int j = 0; j < 6; j++)
                        {
                            result += ds.Tables[0].Rows[i][j];
                            if (j != 5)
                                result += "&";

                        }
                        if (i != num - 1)
                            result += "#";
                    }
                    return result;

                }

                else
                    return "none";

            }
            catch (Exception e)
            {
                return ("false@" + e.Message);
            }
            finally
            {
                sqlConnection.Close();

            }







        }
        #endregion 

        #region   获取简要摘要信息
        public string getinfotin()
        {
            string result = string.Empty;
            string connectonString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string commandString = String.Format("SELECT ID,name,lng,lat FROM t_AirDefenseInfo");
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
                if (ds == null)
                    return "none";
                int num = ds.Tables[0].Rows.Count;
                if (num > 0)
                {
                    for (int i = 0; i < num; i++)
                    {

                        for (int j = 0; j < 4; j++)
                        {
                            result += ds.Tables[0].Rows[i][j];
                            if (j !=3)
                                result += "&";

                        }
                        if (i != num - 1)
                            result += "#";
                    }
                    return result;

                }

                else
                    return "none";

            }
            catch (Exception e)
            {
                return ("false@" + e.Message);
            }
            finally
            {
                sqlConnection.Close();

            }







        }
        #endregion 

        #region       执行查询SQL语句,返回DS
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
             
            }
            finally
            {
                sqlConnection.Close();
            }



        }
        #endregion

        #region  执行插入语更新语句,返回提示信息
        public string insertandUpdate(string commandString)
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
                if (num > 0)
                    result = "true@执行成功";
                else
                    result = "false@执行失败,错误代码0";
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

        public string getimageinfo(string id)
        {
            string result=null;
            string commandString = "SELECT urlname FROM t_image where picid= '" + id + "'" + " and type='RF'";
            DataSet ds =getDS(commandString);
            if (ds != null)
            {
                int piccount = ds.Tables[0].Rows.Count;
                if (piccount > 0)
                {
                    for (int i = 0; i < piccount; i++)
                    {
                        result += ds.Tables[0].Rows[i][0];
                        if (i != piccount - 1)
                            result += "$";
                    }
                }
                else
                {
                    result = "NONE";
                }
            }
            else
            {
                result = "false@未找到对应的图片";
            
            }
            return result;
        }

    }
}