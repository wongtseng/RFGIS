
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebApplication4
{
    public partial class _setbginfo : System.Web.UI.Page
    {
      
            wongtsengDB dbkit = new wongtsengDB();
            enum nowtype { view, edit, add }
            static string edittype = nowtype.view.ToString();    //标记当前的操作类型
            static int id = -1; //标记当前操作的人防工事的ID.


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
                    Image1.Visible = false;
                }

            }

            #region 触发gridview去获取AP信息
            public void getinfo()
            {
                string commandString = "SELECT ID, NO,name,lng,lat FROM t_BGInfo";                  
                DataSet ds = dbkit.getDS(commandString);
                if (ds != null)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }

            }
            #endregion

            #region 触发gridview去获取图片信息
            public void getpicinfo()
            {

                string commandString = "SELECT picid,name,size,time,urlname FROM t_image where picid= '" + id + "'" +" and type='BG'";
                DataSet ds = dbkit.getDS(commandString);
                if (ds != null)
                {
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                }

            }
            #endregion

            #region     查看人防工事详情
            protected void Button1_Click(object sender, EventArgs e)
            {

                edittype = nowtype.view.ToString();
                btn_infoedit_ok.Visible = false;
                Panel_maininfo.Visible = true;
                Panel_picselect.Visible = true;
                Panel_picupload.Visible = false;

                btn_infoedit_cancel.Text = "关闭";
                ImageButton button = (ImageButton)sender;
                GridViewRow row = (GridViewRow)button.Parent.Parent;

                id = int.Parse(row.Cells[0].Text.ToString());   //当前人防工事ID
                tb_no.Text = row.Cells[1].Text.ToString();   //  编号
                tb_name.Text = row.Cells[2].Text.ToString(); // 名称
                tb_lng.Text = row.Cells[3].Text.ToString(); //经度
                tb_lat.Text = row.Cells[4].Text.ToString(); // 纬度
  

                tb_no.Enabled = false;
                tb_name.Enabled = false;
                tb_lng.Enabled = false;
                tb_lat.Enabled = false;


                getpicinfo();
            }
            #endregion

            #region    人防信息编辑-取消
            protected void btn_infoedit_cancel_Click(object sender, EventArgs e)
            {
                Panel_maininfo.Visible = false;
                btn_infoedit_cancel.Text = "取消";
                btn_infoedit_ok.Visible = true;
                Panel_picselect.Visible = false;
                Image1.ImageUrl = null;

            }
            #endregion

            #region    人防信息编辑
            protected void Button2_Click(object sender, EventArgs e)
            {
                edittype = nowtype.edit.ToString();

                btn_infoedit_ok.Text = "更新";
                btn_infoedit_cancel.Text = "取消";
                ImageButton button = (ImageButton)sender;
                GridViewRow row = (GridViewRow)button.Parent.Parent;
                id = int.Parse(row.Cells[0].Text.ToString());
                tb_no.Text = row.Cells[1].Text.ToString();   //  编号
                tb_name.Text = row.Cells[2].Text.ToString(); // 名称
                tb_lng.Text = row.Cells[3].Text.ToString(); //经度
                tb_lat.Text = row.Cells[4].Text.ToString(); // 纬度


                tb_no.Enabled = true;
                tb_name.Enabled = true;
                tb_lng.Enabled = true;
                tb_lat.Enabled = true;


                btn_infoedit_ok.Visible = true;
                Panel_maininfo.Visible = true;
                Panel_picupload.Visible = false;
                getpicinfo();
                Panel_picselect.Visible = true;

            }

            #endregion

            #region     人防信息添加
            protected void btn_add_Click(object sender, EventArgs e)
            {
                clearinfo();
                edittype = nowtype.add.ToString();

                Panel_maininfo.Visible = true;
                Panel_picupload.Visible = false;
                Panel_picselect.Visible = false;

                btn_infoedit_ok.Text = "添加";
                tb_no.Enabled = true;
                tb_name.Enabled = true;
                tb_lng.Enabled = true;
                tb_lat.Enabled = true;
                btn_infoedit_ok.Visible = true;
            }
            #endregion

            #region   清除控件内信息
            private void clearinfo()
            {
                tb_no.Text = "";
                tb_name.Text = "";
                tb_lat.Text = "";
                tb_lng.Text = "";

                Image1.ImageUrl = null;

            }
            #endregion

            #region   人防信息修改-点击确定按钮
            protected void btn_infoedit_ok_Click(object sender, EventArgs e)
            {

                int no = int.Parse(tb_no.Text.Trim());
                string name = tb_name.Text.Trim();
                string lng = tb_lng.Text.Trim();
                string lat = tb_lat.Text.Trim();
                if (edittype == nowtype.add.ToString())
                {
                    string commandString = String.Format("insert into t_BGInfo(NO,name,lng,lat)  values ('{0}','{1}','{2}','{3}')",  no, name, lng, lat);
                    string result = dbkit.insertandUpdate(commandString);
                    if (result.Split('@')[0] == "true")
                    {
                        getinfo();
                        clearinfo();
                        Panel_maininfo.Visible = false;
                        dbkit.Show(this, "添加成功");
                    }
                    else
                    {
                        dbkit.Show(this, "添加失败");
                    }

                }
                else if (edittype == nowtype.edit.ToString())
                {
                    string commandString = String.Format("update  t_BGInfo set NO='{0}',name='{1}',lng='{2}',lat='{3}' where id='" + id + "'",
                   no, name, lng, lat);
                    string result = dbkit.insertandUpdate(commandString);
                    if (result.Split('@')[0] == "true")
                    {
                        getinfo();
                        clearinfo();
                        Panel_maininfo.Visible = false;
                        Panel_picselect.Visible = false;
                        dbkit.Show(this, "更新成功");
                    }
                    else
                    {
                        string a = result.Split('@')[1];
                        dbkit.Show(this, "更新失败");
                    }
                }

            }
            #endregion

            #region     人防信息删除
            protected void btn_delete_Click(object sender, EventArgs e)
            {

                Panel_picupload.Visible = false;
                Panel_picselect.Visible = false;
                Panel_maininfo.Visible = false;
                ImageButton button = (ImageButton)sender;
                GridViewRow row = (GridViewRow)button.Parent.Parent;
             
                id = int.Parse(row.Cells[0].Text.ToString());
                string commandString = "SELECT picid FROM t_image where picid= '" + id + "'" + " and type='BG'";
                DataSet ds = dbkit.getDS(commandString);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dbkit.Show(this, "请先删除关联图片");
                        return;
                    }

                }

                commandString = String.Format("delete from   t_BGInfo where   id='{0}' ", id);
                string result = dbkit.insertandUpdate(commandString);
                if (result.Split('@')[0] == "true")
                {
                    getinfo();
                    clearinfo();
                    Panel_maininfo.Visible = false;
                    dbkit.Show(this, "删除成功");
                }
                else
                {

                    dbkit.Show(this, "删除失败");
                }

            }
            #endregion

            #region  人防信息添加图片
            protected void btn_pic_Click(object sender, EventArgs e)
            {
                Panel_picupload.Visible = true;
                Panel_picselect.Visible = true;
                Panel_maininfo.Visible = false;
                ImageButton button = (ImageButton)sender;
                GridViewRow row = (GridViewRow)button.Parent.Parent;
                id = int.Parse(row.Cells[0].Text.ToString());
                getpicinfo();
                lb_rfname.Text = row.Cells[2].Text.ToString();
            }
            #endregion

            #region    点击后,上传图片并且存入数据库
            protected void btn_picupload_Click(object sender, EventArgs e)
            {



                if (FileUpload2.PostedFile.ContentLength / 1024 > 1000)
                {
                    dbkit.Show(this, "文件过大");
                    return;
                }

                if (FileUpload2.PostedFile.ContentType != "image/jpeg")
                {

                    dbkit.Show(this, "请选择jpg格式图片");
                    return;
                }

                try
                {
                    string name = DateTime.Now.ToString("ddhhmmss");        //yyyMMddhhmmss
                    string urlname = "BG" + id + "_" + name + FileUpload2.FileName;
                    FileUpload2.SaveAs(Server.MapPath("pic") + "\\" + urlname);
                    string filename = FileUpload2.FileName;
                    string size = (FileUpload2.PostedFile.ContentLength / 1024).ToString() + "KB";
                    string time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string commandString = String.Format("insert into t_image(picid,type,name,size,time,urlname)  values ('{0}','{1}','{2}','{3}','{4}','{5}')", id, "BG",filename, size, time, urlname);
                    string result = dbkit.insertandUpdate(commandString);
                    if (result.Split('@')[0] == "true")
                    {
                        getpicinfo();
                        Panel_picselect.Visible = true;
                        dbkit.Show(this, "图片上传成功");
                    }

                }
                catch
                {

                    dbkit.Show(this, "上传失败");


                }



            }
            #endregion

            #region      显示人防工事图片
            protected void btn_showpic_Click(object sender, EventArgs e)
            {
                string picurl = string.Empty;
                ImageButton button = (ImageButton)sender;
                GridViewRow row = (GridViewRow)button.Parent.Parent;
                picurl = row.Cells[3].Text.ToString();
                Image1.ImageUrl = "~/pic/" + picurl;
                Image1.Visible = true;

            }
            #endregion

            #region     为pic的gridview设置按钮状态
            protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (edittype == nowtype.view.ToString())
                        ((ImageButton)e.Row.Cells[0].FindControl("btn_deletepic")).Visible = false;
                    e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#eaeaea';");//这是鼠标移到某行时改变某行的背景 
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");//鼠标移走时恢复 
                }

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

            #region 点击后隐藏图片
            protected void Image1_Click(object sender, ImageClickEventArgs e)
            {
                Image1.ImageUrl = null;
            }
            #endregion

            #region   取消上传图片
            protected void btn_picuploadcancel_Click(object sender, EventArgs e)
            {
                clearinfo();
                Panel_picselect.Visible = false;
                Panel_picupload.Visible = false;
            }
            #endregion

            #region  删除图片
            protected void btn_deletepic_Click(object sender, ImageClickEventArgs e)
            {
                string picurl = string.Empty;
                ImageButton button = (ImageButton)sender;
                GridViewRow row = (GridViewRow)button.Parent.Parent;
                picurl = row.Cells[3].Text.ToString();
                string FilePath = Server.MapPath("pic/") + picurl;
                try
                {
                    File.Delete(FilePath);
                    string commandString = String.Format("delete from  t_image where   urlname='{0}' ", picurl);
                    string result = dbkit.insertandUpdate(commandString);
                    if (result.Split('@')[0] == "true")
                    {
                        getinfo();
                        clearinfo();

                        dbkit.Show(this, "删除成功");
                        getpicinfo();
                    }
                    else
                    {

                        dbkit.Show(this, "删除失败");
                    }
                }
                catch (Exception ee)
                {
                    string aaaaaaaaa = ee.Message;
                }

            }
            #endregion
        }
    }
