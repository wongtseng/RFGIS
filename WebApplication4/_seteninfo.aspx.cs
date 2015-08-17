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
    public partial class _seteninfo : System.Web.UI.Page
    {
        wongtsengDB dbkit = new wongtsengDB();
        enum nowtype { view,edit,add}
         static string edittype = nowtype.view.ToString();    //标记当前的操作类型
         static  int id =-1; //标记当前操作的入口的ID.
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
                DataSet ds = dbkit.getrfinfo();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ListItem li = new ListItem();
                            li.Value = ds.Tables[0].Rows[i][0].ToString();
                            li.Text = ds.Tables[0].Rows[i][1].ToString();
                            ddl_rf.Items.Add(li);
                        }
                    }
                 
                }


            }
           
        }

        #region 触发gridview去获取人防工事信息
        public void getinfo()
        {
            string commandString = "SELECT ID, NO,name,lng,lat,rfid,damage,damageinfo FROM t_ENInfo";
            DataSet ds=dbkit.getDS(commandString);
            DataSet rfds = dbkit.getrfinfo();
       
            if ( ds!= null)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind(); 
            }

        }
        #endregion

        #region 触发gridview去获取图片信息
        public void getpicinfo()
        {

            string commandString = "SELECT picid,name,size,time,urlname,message FROM t_image where picid= '" + id + "'" + " and type='EN'";
            DataSet ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }

        }
        #endregion

        #region     查看人防工事入口详情
        protected void Button1_Click(object sender, EventArgs e)
        {

            edittype = nowtype.view.ToString();
            btn_infoedit_ok.Visible = false;
            Panel_maininfo.Visible = true;
            Panel_picselect.Visible = true;
            Panel_picupload.Visible = false;
            Panel_cam.Visible = true;
            Panel_cam_add.Visible = false;
           
            btn_infoedit_cancel.Text = "关闭";
            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;

            id = int.Parse(row.Cells[0].Text.ToString());   //当前人防工事入口ID
            tb_no.Text = row.Cells[1].Text.ToString();   //  编号
            tb_name.Text = row.Cells[2].Text.ToString(); // 名称
            tb_lng.Text = row.Cells[3].Text.ToString(); //经度
            tb_lat.Text = row.Cells[4].Text.ToString(); // 纬度

            for (int i = 0; i < ddl_rf.Items.Count; i++)
            {
                if (ddl_rf.Items[i].Text ==row.Cells[5].Text)
                {
                    ddl_rf.SelectedIndex = i;
                    break;
                }
            
            }
            for (int i = 0; i < ddl_d.Items.Count; i++)
            {
                if (ddl_d.Items[i].Text == row.Cells[6].Text)
                {
                    ddl_d.SelectedIndex = i;
                    break;
                }

            }

            tb_damageinfo.Text = row.Cells[6].Text.ToString();  //损毁情况 
            tb_no.Enabled = false;
            tb_name.Enabled = false;
            tb_lng.Enabled = false;
            tb_lat.Enabled = false;
            ddl_d.Enabled = false;
            ddl_rf.Enabled = false;
            tb_damageinfo.Enabled = false;
            getpicinfo();
            getcaminfo();
        }
        #endregion

        #region    人防信息编辑-取消
        protected void btn_infoedit_cancel_Click(object sender, EventArgs e)
        {
            Panel_maininfo.Visible = false;
            btn_infoedit_cancel.Text = "取消";
            btn_infoedit_ok.Visible = true;
            Panel_picselect.Visible = false;
            Panel_cam_add.Visible = false;
            Panel_cam.Visible = false;
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
            id = int.Parse( row.Cells[0].Text.ToString()); 
            tb_no.Text = row.Cells[1].Text.ToString();   //  编号
            tb_name.Text = row.Cells[2].Text.ToString(); // 名称
            tb_lng.Text = row.Cells[3].Text.ToString(); //经度
            tb_lat.Text = row.Cells[4].Text.ToString(); // 纬度
            for (int i = 0; i < ddl_rf.Items.Count; i++)
            {
                if (ddl_rf.Items[i].Text == row.Cells[5].Text)
                {
                    ddl_rf.SelectedIndex = i;
                    break;
                }

            }
            for (int i = 0; i < ddl_d.Items.Count; i++)
            {
                if (ddl_d.Items[i].Text == row.Cells[6].Text)
                {
                    ddl_d.SelectedIndex = i;
                    break;
                }

            }
            tb_damageinfo.Text = row.Cells[8].Text.ToString();  //损毁情况  

            if (tb_damageinfo.Text == "&nbsp;")
                tb_damageinfo.Text = "无";
            tb_no.Enabled = true;
            tb_name.Enabled = true;
            tb_lng.Enabled = true;
            tb_lat.Enabled = true;
            ddl_d.Enabled = true;
            ddl_rf.Enabled = true;
            btn_infoedit_ok.Visible = true;
            Panel_maininfo.Visible = true;
            Panel_picupload.Visible = false;
            Panel_cam_add.Visible = false;
            Panel_cam.Visible = true;
            getpicinfo();
            getcaminfo();
            Panel_picselect.Visible = true;

        }

        #endregion

        #region     人防信息添加
        protected void btn_add_Click(object sender, ImageClickEventArgs e)
        {
            clearinfo();
            edittype = nowtype.add.ToString();

            Panel_maininfo.Visible = true;
            Panel_picupload.Visible = false;
            Panel_picselect.Visible = false;
            Panel_cam.Visible = false;
            Panel_cam_add.Visible = false;

            btn_infoedit_ok.Text = "添加";
            tb_no.Enabled = true;
            tb_name.Enabled = true;
            tb_lng.Enabled = true;
            tb_lat.Enabled = true;
            ddl_rf.Enabled = true;
            ddl_d.Enabled = true;
            tb_damageinfo.Enabled = true;
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
            ddl_rf.SelectedIndex = 0;
            ddl_d.SelectedIndex = 0;
            tb_picmessage.Text = "";
            tb_damageinfo.Text = "";
            Image1.ImageUrl = null;
          
        }
        #endregion

        #region   人防入口信息添加\修改-点击确定按钮
        protected void btn_infoedit_ok_Click(object sender, EventArgs e)
        {
         
            int no = int.Parse(tb_no.Text.Trim());
            string name = tb_name.Text.Trim();
            string lng = tb_lng.Text.Trim();
            string lat = tb_lat.Text.Trim();
            string rfid = ddl_rf.SelectedValue;//获取所属人防工事的ID号
            string damage = ddl_d.SelectedValue;//获取损毁情况的ID
             string damageinfo =string.Empty;
               if (tb_damageinfo.Text.Trim() == "&nbsp;" || tb_damageinfo.Text.Trim()=="")
                   damageinfo = "无";
               else
                   damageinfo = tb_damageinfo.Text.Trim(); 
            if (edittype ==nowtype.add.ToString())
            {
                string commandString = String.Format("insert into t_ENInfo(NO,name,lng,lat,rfid,damage,damageinfo,isnew)  values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
              no, name, lng, lat, rfid, damage, damageinfo,"true");
                string result = dbkit.insertandUpdate(commandString);
                Panel_cam.Visible = false;
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
            else     if(edittype==nowtype.edit.ToString())
            {
                string commandString = String.Format("update   t_ENInfo set NO='{0}',name='{1}',lng='{2}',lat='{3}',rfid='{4}',damage='{5}',damageinfo='{6}', isnew='{7}' where id='"+id+"'",
                  no, name, lng, lat, rfid, damage, damageinfo,"true");
                string result = dbkit.insertandUpdate(commandString);
                if (result.Split('@')[0] == "true")
                {
                    getinfo();
                    clearinfo();
                    Panel_maininfo.Visible = false;
                    Panel_picselect.Visible = false;
                    Panel_cam.Visible = false;
                    Panel_cam_add.Visible = false;
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

        #region     人防入口信息删除
        protected void btn_delete_Click(object sender, EventArgs e)
        {

            Panel_picupload.Visible = false;
            Panel_picselect.Visible = false;
            Panel_maininfo.Visible = false;
            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
            id = int.Parse(row.Cells[0].Text.ToString());
            string commandString = "SELECT picid FROM t_image where picid= '" + id + "'" + " and type='EN'";
            DataSet ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dbkit.Show(this, "请先删除关联图片");
                    return;
                }
                   
            }

            commandString = String.Format("delete from   t_ENIfo where   id='{0}' ", id);
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
             
                dbkit.Show(this,  "删除失败" );
            }

        }
        #endregion

        #region  人防入口信息添加图片
        protected void btn_pic_Click(object sender, EventArgs e)
        {
            Panel_picupload.Visible = true;
            Panel_picselect.Visible = true;
            Panel_maininfo.Visible = false;
            Panel_cam_add.Visible = false;
            Panel_cam.Visible = false;
            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
            id = int.Parse(row.Cells[0].Text.ToString());
            getpicinfo();
            lb_rfname.Text =row.Cells[2].Text.ToString();
        }
        #endregion

        #region    点击后,上传图片并且存入数据库
        protected void btn_picupload_Click(object sender, EventArgs e)
        {
                    if(FileUpload2.PostedFile.ContentLength/1024>1000)
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
                        string urlname="EN"+ id+"_"+name+FileUpload2.FileName;
                        FileUpload2.SaveAs(Server.MapPath("pic") + "\\" +urlname);
                        string filename = FileUpload2.FileName;
                        string size = (FileUpload2.PostedFile.ContentLength / 1024).ToString() + "KB";
                        string time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        string message = tb_picmessage.Text;
                        string commandString = String.Format("insert into t_image(picid,type,name,size,time,urlname,message)  values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", id, "EN", filename,size, time, urlname,message);
                        string result = dbkit.insertandUpdate(commandString);
                        if (result.Split('@')[0] =="true")
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

        #region      显示人防工事入口图片
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

        #region       为人防工事入口的gridview设置状态
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#eaeaea';");//这是鼠标移到某行时改变某行的背景 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");//鼠标移走时恢复 
                
                //更改数据显示-id变化为名称
                DataSet ds = dbkit.getrfinfo();
                string id = e.Row.Cells[5].Text;
                int rfcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rfcount; i++)
                {
                    string rfid=ds.Tables[0].Rows[i][0].ToString();
                    if (id == rfid)
                    {
                        e.Row.Cells[5].Text = ds.Tables[0].Rows[i][1].ToString();
                        break;
                    }
                }
                //更改数据显示-损毁情况的id变化为名称
                ds = dbkit.getendemangeinfo();
                 id = e.Row.Cells[6].Text;
                 rfcount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rfcount; i++)
                {
                    string rfid = ds.Tables[0].Rows[i][0].ToString();
                    if (id == rfid)
                    {
                        e.Row.Cells[6].Text = ds.Tables[0].Rows[i][1].ToString();
                        break;
                    }
                }

            }







        }
        #endregion

        #region 点击后隐藏图片
        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {
            Image1.ImageUrl = null;
            Image1.Visible = false;
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

        #region  添加cam信息-显示panel
        protected void btn_cam_Click(object sender, ImageClickEventArgs e)
        {

            Panel_picselect.Visible = false;
            Panel_picupload.Visible = false;
            Panel_maininfo.Visible = false;

            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
            id = int.Parse(row.Cells[0].Text.ToString());   //当前人防工事入口ID
            lb_enname.Text = row.Cells[2].Text.ToString();
            Panel_cam_add.Visible = true;
            Panel_cam.Visible = true;
            getcaminfo();
        }
        #endregion

        #region      添加cam信息-确定
        protected void btn_cam_ok_Click(object sender, EventArgs e)
        {
            string cam_no = tb_camid.Text;
            string cam_name = tb_camname.Text;
            string cam_url = tb_camrul.Text;
            string commandString = String.Format("insert into t_cam(NO,name,camurl,enid)  values ('{0}','{1}','{2}','{3}')",
           cam_no, cam_name, cam_url,id);
            string result = dbkit.insertandUpdate(commandString);
            if (result.Split('@')[0] == "true")
            {
                dbkit.Show(this, "添加成功");
                Panel_cam.Visible = false;
                getcaminfo();
            }
            else
            {
                dbkit.Show(this, "添加失败");
            }


        }
        #endregion



        #region 触发gridview去获取监控信息
        public void getcaminfo()
        {


            string commandString = "SELECT no,name,camurl FROM t_cam where enid= '" + id + "'"; 
            DataSet ds = dbkit.getDS(commandString);
            if (ds != null)
            {
                gv_camera.DataSource = ds;
                gv_camera.DataBind();
            }

        }
        #endregion

        protected void gv_camera_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (edittype == nowtype.view.ToString())
                    ((ImageButton)e.Row.Cells[0].FindControl("btn_deletecam")).Visible = false;
                else
                    ((ImageButton)e.Row.Cells[0].FindControl("btn_deletecam")).Visible = true;
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#eaeaea';");//这是鼠标移到某行时改变某行的背景 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;");//鼠标移走时恢复 
            }
        }


        protected void btn_showcam_Click(object sender, EventArgs e)
        {

            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
           string  vediourl = row.Cells[2].Text.ToString();

           Response.Write("<script language='javascript'>window.open('" + vediourl + "');</script>");
        //  ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "updateScript", "alert('对不起，账号和密码错误');", true);
       //    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(System.Web.UI.Page), "aaa", "Response.Write(alert('下单成功！')", false);
        
           //Response.Redirect("XXX.aspx", true);
        
        
        }

        protected void btn_deletecam_Click(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            GridViewRow row = (GridViewRow)button.Parent.Parent;
          // string   cam_url = row.Cells[2].Text.ToString();
           string cam_NO = row.Cells[1].Text.ToString();

           string commandString = String.Format("delete from  t_cam where   NO='{0}' ", cam_NO);
           string result = dbkit.insertandUpdate(commandString);
           if (result.Split('@')[0] == "true")
           {
               getinfo();
               clearinfo();

               dbkit.Show(this, "删除成功");
               getcaminfo();
           }
           else
           {

               dbkit.Show(this, "删除失败");
           }
        
        }

        protected void btn_cam_cancel_Click(object sender, EventArgs e)
        {
            Panel_cam.Visible = false;
            Panel_cam_add.Visible = false;
        }
      

    }
}