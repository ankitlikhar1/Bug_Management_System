using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Mail;
using MySql.Data.MySqlClient;

namespace BugManagementSystem
{
    public partial class Create_Requirement : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; username = root; password = root; database = manageuser");
        public Create_Requirement()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
                if (combo_client.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (combo_module.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (combo_project.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (richText_title.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (richText_description.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (comboBox_priority.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (combox_status.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (txt_reporter.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (combo_assignee.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (date_time.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (combo_client.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Client Name");
                    return;
                }
                else if (combo_module.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Module Name");
                    return;
                }
                else if (combo_project.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Project Name");
                    return;
                }
                else if (richText_title.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Title");
                    return;
                }
                else if (richText_description.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Description");
                    return;
                }
                else if (richText_comment.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Reporter Email Id");
                    return;
                }
                Regex re1 = new Regex(@".{600,700}");
                if (re1.IsMatch(richText_title.Text))
                {
                    MessageBox.Show("Message is too long for title!");
                    return;
                }

                Regex re2 = new Regex(@".{600,700}");
                if (re2.IsMatch(richText_description.Text))
                {
                    MessageBox.Show("Message is too long for Description!");
                    return;
                }

                Regex re3 = new Regex(@".{600,700}");
                if (re3.IsMatch(richText_comment.Text))
                {
                    MessageBox.Show("Message is too long for Comment!");
                    return;
                }
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(txt_reporter.Text))
                {
                    MessageBox.Show("Enter Valid Email ID");
                    return;
                }
                //Regex reg4 = new Regex(@".{8,13}");
                //if (!reg.IsMatch(txt_to.Text))
                //{
                //    MessageBox.Show("Message is too long for Password filed!");
                //    return;
                //}

                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = new CommandType();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into requirement(Client_Name, Project_Name, Module_Name, Date, Title, Description, Priority, Status,  Estimate,Assignee, Reporter, comment)Values('" + combo_client.Text + "','" + combo_project.Text + "','" + combo_module.Text + "','" + date_time.Text + "','" + richText_title.Text + "','" + richText_description.Text + "','" + comboBox_priority.Text + "','" + combox_status.Text + "','" + UpDown.Value + "','" + combo_assignee.Text + "','" + txt_reporter.Text + "','" + richText_comment.Text + "')";
                    cmd.ExecuteNonQuery();
                    try
                    {
                       // string smtpAddress = "smtp.gmail.com";
                        int port = 587;
                        bool enableSsl = true;

                        string from = txt_reporter.Text;
                        string pass = txt_to.Text;
                        // string to = text_assignee.Text;
                        string body = "The Following Issue has been SUBMITED." + Environment.NewLine + "==============================================" + Environment.NewLine + "Reported by : '" + txt_reporter.Text + "'" + Environment.NewLine + "Assigned to : '" + combo_assignee.Text + "'" + Environment.NewLine + "==============================================" + Environment.NewLine + "Project: '" + combo_project.Text + "'" + Environment.NewLine + "Priority: '" + comboBox_priority.Text + "'" + Environment.NewLine + "Status:'" + combox_status.Text + "'" + Environment.NewLine + "==============================================" + Environment.NewLine + "Date Submitted: '" + date_time.Text + "'" + Environment.NewLine + "==============================================" + Environment.NewLine + "Description: '" + richText_description.Text + "'";

                        MailMessage mail = new MailMessage();
                        {
                            string[] to = { combo_assignee.Text };
                            foreach (var m in to)
                            {
                                mail.To.Add(m);
                            }
                            mail.From = new MailAddress(from);
                            // mail.To.Add(to);
                            mail.IsBodyHtml = false;
                            mail.Body = body;

                            using (SmtpClient client = new SmtpClient("smtp.gmail.com", port))
                            {
                                client.Credentials = new NetworkCredential(txt_reporter.Text, txt_to.Text);
                                client.EnableSsl = enableSsl;
                                client.Send(mail);
                            }
                        }
                        MessageBox.Show("Report Send Successfully");
                        clear();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("please enter valid email id or Password or check internet connection");
                    }
                    // MessageBox.Show("Created successfully");
                    con.Close();
                }
            }
        
        public void assignee_email()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = new CommandType();
            cmd.Connection = con;
            cmd.CommandText = "Select Distinct email_id from admin";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            combo_assignee.DataSource = dt;
            combo_assignee.DisplayMember = "email_id";
            combo_assignee.ResetText();
            con.Close();

        }

        private void txt_client_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                MessageBox.Show("Only Characters are allowed");
                e.Handled = true;
            }
        }

        private void txt_project_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                MessageBox.Show("Only Characters are allowed");
                e.Handled = true;  
            }
        }

        private void txt_module_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                MessageBox.Show("Only Characters are allowed");
                e.Handled = true;
            }
        }

        private void combox_status_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_priority_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_review_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_reviewer_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void combo_client_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void combo_project_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void combo_module_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void client()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Distinct Client_Name from client";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            combo_client.DataSource = dt;
            combo_client.DisplayMember = "Client_Name";
            combo_client.ResetText();
            con.Close();
        }

        //public void module()
        //{
        //    if (con.State == ConnectionState.Open)
        //    {
        //        con.Close();
        //    }
        //    con.Open();
        //    MySqlCommand cmd = con.CreateCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "select Distinct Module_Name from module";
        //    cmd.ExecuteNonQuery();
        //    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    combo_module.DataSource = dt;
        //    combo_module.DisplayMember = "Module_Name";
        //    combo_module.ResetText();
        //    con.Close();
        //}

        public void project()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Distinct Project_Name from project";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            combo_project.DataSource = dt;
            combo_project.DisplayMember = "Project_Name";
            combo_project.ResetText();
            con.Close();
        }

        private void Create_Requirement_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string str = "select distinct project_name from module";
            MySqlCommand cmd = new MySqlCommand(str, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    combo_project.Items.Add(dr["project_name"]).ToString();
                }
            }
            con.Close();

            assignee_email();
            client();
            //module();
           // project();
        }

        public void clear()
        {
            combo_client.ResetText();
            combo_project.ResetText();
            combo_module.ResetText();
            comboBox_priority.ResetText();
            combox_status.ResetText();
            richText_title.Clear();
            richText_description.Clear();
            richText_title.Clear();
            combo_assignee.ResetText();
            UpDown.ResetText();
            txt_reporter.Clear();
            richText_comment.Clear();
            txt_to.Clear();
        }

        private void combo_project_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            combo_module.Items.Clear();
            con.Open();
            string str1 = "select Module_Name from module where project_name = '" + combo_project.Text + "'";
            MySqlCommand cmd1 = new MySqlCommand(str1, con);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    combo_module.Items.Add(dr1["Module_Name"]).ToString();
                }
            }
            con.Close();
            
        }

        private void txt_to_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_submit.PerformClick();
            }
        }

        private void richText_comment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_submit.PerformClick();
            }
        }
    }
}
