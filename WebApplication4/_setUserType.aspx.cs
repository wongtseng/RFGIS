using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public partial class _setUserType : System.Web.UI.Page
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
            string commandString = "SELECT ID, username,phone,userTypeText FROM v_userType";
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

            string    commandString = String.Format("delete from   t_user where   id='{0}' ", id);
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

        #region   确认修改
        /// <summary>
        /// 点击确认修改人员类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_infoedit_ok_Click(object sender, EventArgs e)
        {
            string type = string.Empty;
            type = ddl_usertype.SelectedItem.Value;
            if (optype == nowtype.edit.ToString())
            {
                string comm = string.Format("update t_user set userType='{0}' where ID='{1}'", type, id);
                string sqlresult = dbkit.insertandUpdate(comm);

                if (sqlresult.Split('@')[0] == "true")
                {
                    dbkit.Show(this, "更新成功!");
                    Panel_maininfo.Visible = false;
                    getinfo();
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

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            getinfo();
        }


        #region  根据条件查找用户
        protected void btn_search_Click(object sender, EventArgs e)
        {
            string argu = DropDownList1.SelectedValue;


            string commandString =string.Format("SELECT ID, username,phone,userTypeText FROM v_userType where {0}='{1}'",argu,tb_search.Text);
            DataSet ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
                dbkit.Show(this, "查找失败");
        }
       #endregion



    }
}