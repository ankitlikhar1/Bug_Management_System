using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace BugManagementSystem
{
    public partial class Login : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; username = root; password = root; database = manageuser; sslMode = None");
        string str, UserName, Password, Designation;
        int RowCount;
        public Login()
        {
            InitializeComponent();   
        }
        public static string Setvalue;

                 public void click()
                  { 
                    text_username.Clear();
                    text_password.Clear();
                  }
                
                 private void Login_btn_Click(object sender, EventArgs e)
                 {
                     try
                     {
                         if (text_username.Text == "")
                         {
                             MessageBox.Show("Username must not be Blank");
                             return;
                         }
                        
                         else if (text_password.Text == "")
                         {
                             MessageBox.Show("Password must not be Blank");
                             return;
                         }
                         if(con.State == ConnectionState.Open)
                         {
                             con.Close();
                         }
                         // if (con.State == ConnectionState.Closed)
                         con.Open();
                         str = "Select * from admin where username = '" + text_username.Text + "'";
                         MySqlCommand cmd = new MySqlCommand(str);
                         MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);
                         DataTable dt = new DataTable();
                         da.Fill(dt);

                         RowCount = dt.Rows.Count;

                         if (RowCount == 0)
                         {
                             MessageBox.Show("Please Enter Valid User Name!");
                             click();
                             return;
                         }
                         else
                         {
                             for (int i = 0; i < RowCount; i++)
                             {
                                 UserName = dt.Rows[i]["username"].ToString();
                                 Password = dt.Rows[i]["password"].ToString();
                                 Designation = dt.Rows[i]["designation"].ToString();

                                 if(UserName != text_username.Text)
                                 {
                                     MessageBox.Show("Please Enter Valid User Name!");
                                     return;
                                 }

                                 if(Password != text_password.Text)
                                 {
                                     MessageBox.Show("Please Enter Valid Password!");
                                     return;
                                 }

                                 if (dt.Rows[i]["designation"].ToString() == "Admin")
                                 {

                                     Dashboard dash = new Dashboard(UserName,Password,Designation);
                                     dash.btn_manage_user.Enabled = true;
                                     dash.btn_change_pass.Enabled = true;
                                     dash.btn_view_issue.Enabled = true;
                                     dash.btn_logout.Enabled = true;
                                     dash.btn_requirement.Enabled = true;
                                     dash.btn_module.Enabled = true;
                                     dash.btn_client.Enabled = true;
                                     dash.btn_defect_report.ForeColor = Color.Red;
                                     dash.btn_defect_report.Enabled = false;
                                     dash.btn_defect_report.BackColor = Color.LightSteelBlue;
                                     dash.btn_status_report.Enabled = true;
                                     dash.project_btn.Enabled = true;
                                     dash.Show();
                                     this.Close();
                                  }
                                 else if (dt.Rows[i]["designation"].ToString() == "Developer")
                                 {
                                     Dashboard dash = new Dashboard(UserName,Password,Designation);
                                     dash.btn_manage_user.Enabled = false;
                                     dash.btn_manage_user.BackColor = Color.LightSteelBlue; 
                                     dash.btn_change_pass.Enabled = true;
                                     dash.btn_view_issue.Enabled = true;
                                     dash.btn_logout.Enabled = true;
                                     dash.btn_module.Enabled = false;
                                     dash.btn_module.BackColor = Color.LightSteelBlue;
                                     dash.btn_client.Enabled = false;
                                     dash.btn_client.BackColor = Color.LightSteelBlue;
                                     dash.btn_requirement.Enabled = false;
                                     dash.btn_requirement.BackColor = Color.LightSteelBlue;
                                     dash.btn_defect_report.Enabled = false;
                                     dash.btn_defect_report.BackColor = Color.LightSteelBlue;
                                     dash.btn_status_report.Enabled = true;
                                     dash.project_btn.Enabled = false;
                                     dash.project_btn.BackColor = Color.LightSteelBlue;
                                     dash.Show();
                                     this.Close();

                                 }
                                 else if (dt.Rows[i]["designation"].ToString() == "Tester")
                                 {
                                     Dashboard dash = new Dashboard(UserName,Password,Designation);
                                     dash.btn_manage_user.Enabled = false;
                                     dash.btn_manage_user.BackColor = Color.LightSteelBlue;
                                     dash.btn_change_pass.Enabled = true;
                                     dash.btn_view_issue.Enabled = true;
                                     dash.btn_logout.Enabled = true;
                                     dash.btn_module.Enabled = false;
                                     dash.btn_module.BackColor = Color.LightSteelBlue;
                                     dash.btn_client.Enabled = false;
                                     dash.btn_client.BackColor = Color.LightSteelBlue;
                                     dash.btn_defect_report.Enabled = true;
                                     dash.btn_requirement.Enabled = false;
                                     dash.btn_requirement.BackColor = Color.LightSteelBlue;
                                     dash.btn_status_report.Enabled = true;
                                     dash.project_btn.Enabled = false;
                                     dash.project_btn.BackColor = Color.LightSteelBlue;
                                     dash.Show();
                                     this.Close();

                                 }
                             }
                         }

                         Setvalue = text_username.Text;
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.ToString());
                     }
                     this.Close();
                 }

                 private void text_password_KeyDown(object sender, KeyEventArgs e)
                 {
                     if (e.KeyCode == Keys.Enter)
                     {
                         log_btn.PerformClick();
                     }
                 }

               
                 //private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
                 //{
                 //    if (e.KeyChar == ' ')
                 //    {
                 //        MessageBox.Show("Space are not allowed");
                 //        e.Handled = true;
                 //    }
                 //}

                 //private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
                 //{
                 //    if (e.KeyChar == ' ')
                 //    {
                 //        MessageBox.Show("Space are not allowed");
                 //        e.Handled = true;
                 //    }
                 //}
             }
        }
    
    
