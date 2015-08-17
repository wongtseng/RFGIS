using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public partial class _setSysTemUser : System.Web.UI.Page
    {
        wongtsengDB dbkit = new wongtsengDB();
        static int id = -1; //标记当前操作的用户ID.
        enum nowtype {edit, add }
        static string optype = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userName"] == null || Session["userType"].ToString() != "系统管理员")
                Response.Write("<script language=javascript>parent.location.href='Login.aspx';</script>");
            else
                if (Session["userType"].ToString() != "系统管理员")
                    Response.Redirect("_Map.aspx"); 
            //网页首次加载时执行
            if (!IsPostBack)
            {
                getinfo();
               
            }
        }

        #region 触发gridview去获取AP信息
        public void getinfo()
        {
            string commandString = "SELECT ID, UserName,PW,UserType FROM t_SysUser";
            DataSet ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }

        }
        #endregion

        #region     编辑系统用户信息
        protected void btn_Edit_Click(object sender, EventArgs e)
        {
            optype = nowtype.edit.ToString();
            Panel_maininfo.Visible = true;
            //btn_infoedit_cancel.Text = "关闭";
            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
            id = int.Parse(row.Cells[0].Text.ToString());   //当前用户ID
            tb_un.Text =row.Cells[1].Text.ToString();   //  用户名称
            tb_pw.Text = row.Cells[2].Text.ToString();   //  用户密码
             string ut= row.Cells[3].Text.ToString();   //  用户类型
            for(int i=0;i<2;i++)
            {
                if (ut == ddl_usertype.Items[i].Text)
                {
                    ddl_usertype.SelectedIndex = i;
                    break;
                }

            }
        }
        #endregion

        #region     删除系统用户
        protected void btn_delete_Click(object sender, EventArgs e)
        {

         
            Panel_maininfo.Visible = true;
            //btn_infoedit_cancel.Text = "关闭";
            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;

            id = int.Parse(row.Cells[0].Text.ToString());   //当前人防工事ID

            string    commandString = String.Format("delete from   t_SysUser where   id='{0}' ", id);
            string result = dbkit.insertandUpdate(commandString);
            if (result.Split('@')[0] == "true")
            {
                getinfo();
                clean();
                Panel_maininfo.Visible = false;
                dbkit.Show(this, "删除成功");
            }
            else
            {

                dbkit.Show(this, "删除失败");
            }

            getinfo();
        }
        #endregion

        #region       为人防工事的gridview设置状态
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#eaeaea';");//这是鼠标移到某行时改变某行的背景 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");//鼠标移走时恢复 
            }
        }
        #endregion

        #region   确认修改 /添加
        /// <summary>
        /// 点击确认修改人员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_infoedit_ok_Click(object sender, EventArgs e)
        {

            string un = string.Empty;
            string pw = string.Empty;
            string type = string.Empty;
            un = tb_un.Text.Trim();
            pw = tb_pw.Text.Trim();
            type = ddl_usertype.SelectedItem.Text;

            if (optype == nowtype.edit.ToString())
            {
                string comm = string.Format("update t_SysUser set UserName='{0}', PW='{1}',UserType='{2}' where ID='{3}'", un, pw, type, id);
                string sqlresult = dbkit.insertandUpdate(comm);

                if (sqlresult.Split('@')[0] == "true")
                {
                    dbkit.Show(this, "更新成功!");
                    Panel_maininfo.Visible = false;
                    getinfo();
                }

            }
            else
            {
                //INSERT INTO Persons (LastName, Address) VALUES ('Wilson', 'Champs-Elysees')
                string comm = string.Format("INSERT INTO t_SysUser (username,PW,UserType) values ('{0}','{1}','{2}')", un, pw, type);
                string sqlresult = dbkit.insertandUpdate(comm);

                if (sqlresult.Split('@')[0] == "true")
                {
                    dbkit.Show(this, "添加成功!");
                    Panel_maininfo.Visible = false;
                    getinfo();
                }
                else
                {
                    dbkit.Show(this, "添加失败!"+sqlresult.Split('@')[1]);
                }
            
            
            
            
            
            
            
            }







         
            
            
            
        



        }
        #endregion

        #region   点击取消
        protected void btn_infoedit_cancel_Click(object sender, EventArgs e)
        {
            Panel_maininfo.Visible = false;
            clean();
        }
        #endregion


        #region    清理输入框内容
        private void clean()
        {
            tb_pw.Text = string.Empty;
            tb_un.Text = string.Empty;
            ddl_usertype.SelectedIndex = 0;
        }
        #endregion

        #region  添加新用户
        protected void btn_add_Click(object sender, ImageClickEventArgs e)
        {
            Panel_maininfo.Visible = true;
            optype = nowtype.add.ToString();
            clean();





        }
        #endregion

    }
}