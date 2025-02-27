using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


namespace _2T_ConnectionASP
{
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter da;
        string s = ConfigurationManager.ConnectionStrings["dbconnect"].ToString();
        string fnm,h1,h2,h3;
        string[] hb = new string[3];
        protected void Page_Load(object sender, EventArgs e)
        {
            connection();
            fillg();

        }
        void fillg()
        {
            connection();
            da = new SqlDataAdapter("select * from emp_tbl", con);
            ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        void connection()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            uploadimg();
            connection();
            hobbies();
            if (Button1.Text=="Save")
            {
                

                for(int i =0;i<1;i++)
                {
                    if(hb[i]=="Cricket")
                    {
                        h1 = "Cricket";
                        i++;
                    }
                    else
                    {
                        h1 = "Null";
                        i++;
                    }
                    if(hb[i]=="Football")
                    {
                        h2 = "Football";
                        i++;

                    }
                    else
                    {
                        h2 = "Null";
                        i++;
                    }

                    if(hb[i]== "Vallyball")
                    {
                        h3 = "Vallyball";
                        i++;
                    }
                    else
                    {
                        h3 = "Null";
                        
                    }

                }


                cmd =new SqlCommand ("insert into emp_tbl(Name,Gender,Hobby1,Hobby2,Hobby3,City,Address,Image) " +
                    "values ('"+txtname.Text+"','"+rdbgd.Text+"', '"+h1+"','"+h2+"','"+h3+"','"+drpct.SelectedValue+"','"+txtadd.Text+"','"+fnm+"')", con);
                cmd.ExecuteNonQuery();
                fillg();
            }

            else
            {
                connection();
                hobbies();
                for (int i = 0; i < 1; i++)
                {
                    if (hb[i] == "Cricket")
                    {
                        h1 = "Cricket";
                        i++;
                    }
                    else
                    {
                        h1 = "Null";
                        i++;
                    }
                    if (hb[i] == "Football")
                    {
                        h2 = "Football";
                        i++;

                    }
                    else
                    {
                        h2 = "Null";
                        i++;
                    }

                    if (hb[i] == "Vallyball")
                    {
                        h3 = "Vallyball";
                        i++;
                    }
                    else
                    {
                        h3 = "Null";

                    }

                }

                cmd = new SqlCommand("update emp_tbl set Name='"+txtname.Text+"',Gender='"+rdbgd.Text+"',Hobby1='"+h1+ "' ,Hobby2='" + h2 + "',Hobby3='" + h3 + "' ,City='"+drpct.SelectedValue+"',Address='"+txtadd.Text+"' where Id='"+ViewState["id"]+"'      ", con);
                cmd.ExecuteNonQuery();
                empty();
                fillg();

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="cmd_edt")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ViewState["id"] = id;
                filltext();
                Button1.Text = "Update";
            }
            else
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ViewState["id"] = id;
                cmd = new SqlCommand("delete from emp_tbl where Id='"+ViewState["id"]+"'", con);
                cmd.ExecuteNonQuery();
            }
        }

        public DataSet select(int id)
        {
            connection();
            da = new SqlDataAdapter("select * from emp_tbl where Id='"+id+"'", con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        void filltext()
        {
            connection();
            ds = new DataSet();
            ds = select(Convert.ToInt32(ViewState["id"]));

            txtadd.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            rdbgd.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
            drpct.SelectedValue = ds.Tables[0].Rows[0]["City"].ToString();

            if(ds.Tables[0].Rows[0]["Hobby1"].ToString()=="Cricket")
            {
                chkhb.Items[0].Selected = true;
            }
            else
            {
                chkhb.Items[0].Selected = false;
            }

            if (ds.Tables[0].Rows[0]["Hobby2"].ToString() == "Football")
            {
                chkhb.Items[1].Selected = true;
            }
            else
            {
                chkhb.Items[1].Selected = false;
            }

            if (ds.Tables[0].Rows[0]["Hobby3"].ToString() == "Vallyball")
            {
                chkhb.Items[2].Selected = true;
            }
            else
            {
                chkhb.Items[2].Selected = false;
            }
        }

        void empty()
        {
            txtname.Text = "";
            txtadd.Text = "";
            rdbgd.ClearSelection();
            drpct.ClearSelection();
            chkhb.ClearSelection();

            Button1.Text = "Save";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        void uploadimg()
        {
            if(fldimg.HasFile)
            {
                fnm = "images/" + fldimg.FileName;
                fldimg.SaveAs(Server.MapPath(fnm));
            }
        }

        void hobbies()
        {
            for(int i = 0; i<hb.Length; i++)
            {
                if(chkhb.Items[i].Selected==true)
                {
                    hb[i] = chkhb.Items[i].Text;
                }
            }
        }
    }
}