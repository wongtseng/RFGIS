using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace ws_rfgis
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。
    // [System.Web.Script.Services.ScriptService]

    public class Service1 : System.Web.Services.WebService
    {
        #region    用户注册
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="psw"></param>
        /// <param name="phonenum"></param>
        /// <param name="ssid"></param>
        /// <returns>true/false@info</returns>
        [WebMethod]
        public string UserReg(string username, string psw, string phonenum)
        {
            SQL sql = new SQL();
            string result = sql.checkusername(username);
            if (result.Split('@')[0] == "true")
            {
                string guid = System.Guid.NewGuid().ToString();
                string commandString = String.Format("insert into t_user(username,psw,phone,SSID,guid)  values ('{0}','{1}',{2},'{3}','{4}')", username, sql.psw2md5(psw), phonenum,null, guid);
                try
                {
                    result = sql.insertandUpdate(commandString);
                    if (result.Split('@')[0] == "true")
                        result = "true@注册成功";
                }
                catch (Exception e)
                {
                    result = "false@" + e.Message;
                }

            }

            return result;

        }
        #endregion

        #region  用户登录
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="psw"></param>
        /// <returns> 返回:true/false@info@GUID@工事信息     工事信息每条以#分割；每条信息内部以&分割    eg:ID&名称&经度&纬度&描述&库容&进入人数&剩余库容&损毁情况</returns>
        [WebMethod]
        public string UserLogin(string username, string psw)
        {
            SQL sql = new SQL();
            if (sql.checkpsw(username, psw).Split('@')[0] == "true")
                return sql.checkpsw(username, psw) + "@" + sql.getinfo();
            else
                return sql.checkpsw(username, psw) + "@NONE";
        }
        #endregion


        [WebMethod]
        public string GetAllAirDefenseInfo4android(string GUID)
        {
            SQL sql = new SQL();
            if (sql.checkGUID(GUID))
                return sql.getinfo();
            else
                return "false@未授权的访问@NONE";
        }

        //renew
        [WebMethod]
        public string UserInfoReNew(string GUID, string lng, string lat, string type,string SSID)
        {
            string commandString = String.Format("SELECT username,indicate FROM t_user where GUID='{0}'", GUID);
            string result = null;
            try
            {
                SQL sql = new SQL();
                DataSet ds = sql.getDS(commandString);
                int num = ds.Tables[0].Rows.Count;
                string indicate = string.Empty;
                if (num > 0)
                {
                    if (type == "0")//3G
                        SSID =null;
                    indicate = ds.Tables[0].Rows[0][1].ToString();
                    commandString = String.Format("update   t_user set lng='{0}',lat='{1}',type='{2}',SSID='{3}' where guid='{4}'", lng, lat, type,SSID,GUID);
                    result = sql.insertandUpdate(commandString) + "@"+indicate;
                    return result;

                }
                else
                    result = "false@未授权的调用@NONE";
            }
            catch (Exception e)
            {
                result="false@" + e.Message + "@NONE";
            }
            return result;
        }


        #region     获取全部人防工事的简要信息
        [WebMethod]
        public string GetAirDefenseInfoSum()
        {
            SQL sql = new SQL();
            return sql.getinfosum();
        }
        #endregion

        #region     获取特定人防工事的全部信息
        [WebMethod]
        public string GetOneAirDefenseInfo(string id)
        {
            SQL sql = new SQL();
            return sql.getoneinfo(id) + "&" + sql.getimageinfo(id);
        }
        #endregion

        #region     获取人员位置信息
        [WebMethod]
        public string GetLoaction()
        {
            SQL sql = new SQL();
            return sql.getlocation();
        }
        #endregion


        #region     获取某个人防工事的全部信息-android
        [WebMethod]
        public string GetOneAirDefenseInfo4android(string GUID,string id)
        {
            SQL sql = new SQL();
            if (sql.checkGUID(GUID))
            {
             if(sql.getoneinfo(id).Split('@')[0]!="false")
             {
                 return sql.getoneinfo(id) + "&" + sql.getimageinfo(id);
             }
             else
                 return "false@未找到对应的信息@NONE";
            }
               
            else
                return "false@未授权的访问@NONE";
        }
        #endregion

        [WebMethod]
        public string DS()
        {
            string connectonString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            SqlConnection sqlConnection = new SqlConnection(connectonString);
            string commandString = String.Format("SELECT username,indicate FROM t_user ");
            try
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = commandString;
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return "ok";

            }
            catch (Exception e)
            {
                return e.Message;

            }
            finally
            {
                sqlConnection.Close();
            }
        }



    }


}