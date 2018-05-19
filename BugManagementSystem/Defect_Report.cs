using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;


namespace BugManagementSystem
{
    public partial class Defect_Report : Form
    {
      //string filename;
        string username;
        string password;
        string designation;
        MySqlConnection con = new MySqlConnection("Server = localhost; username = root; password= root ; database = manageuser; sslMode = None");
        
         public Defect_Report(string username, string password, string designation)
        {
            this.username = username;
            this.password = password;
            this.designation = designation;
            InitializeComponent();
        }
        public Defect_Report()
        {
            InitializeComponent();
        }

        private void Defect_Report_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Server = localhost; username = root; password= root; database = manageuser");
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from defect_report ";
            cmd.ExecuteNonQuery();
            projectname();
            assignee_email();
            clear();
            con.Close();
        }

        public void projectname()
        { 
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Distinct Project_Name from project";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            combo_project_name.DataSource = dt;
            combo_project_name.DisplayMember = "Project_Name";
            con.Close();
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Regex re1 = new Regex(@".{600,700}");
                if (re1.IsMatch(richText_title.Text))
                {
                    MessageBox.Show("Message is too long for title!");
                    return;
                }

                Regex re2 = new Regex(@".{600,700}");
                if (re2.IsMatch(richText_steps.Text))
                {
                    MessageBox.Show("Message is too long for Steps to Reproduce!");
                    return;
                }

                Regex re3 = new Regex(@".{600,700}");
                if (re3.IsMatch(richText_actual_result.Text))
                {
                    MessageBox.Show("Message is too long for Actual Result!");
                    return;
                }

                Regex re4 = new Regex(@".{600,700}");
                if (re1.IsMatch(richText_expected_result.Text))
                {
                    MessageBox.Show("Message is too long for Expected Result!");
                    return;
                }

                Regex reg5 = new Regex(@".{40,50}");
                if (reg5.IsMatch(combo_module.Text))
                {
                    MessageBox.Show("Message is too long for Module Name");
                    return;
                }

                //Regex reg6 = new Regex(@".{35,50}");
                //if (reg6.IsMatch(text_assignee.Text))
                //{
                //    MessageBox.Show("Message is too long for Assignee");
                //    return;
                //}
                Regex regx1 = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!regx1.IsMatch(text_reporter.Text))
                {
                    MessageBox.Show("Enter Valid Email ID");
                    return;
                }

                //Regex reg7 = new Regex(@".{35,50}");
                //if (reg7.IsMatch(text_reporter.Text))
                //{
                //    MessageBox.Show("Message is too long for Reporter");
                //    return;
                //}
                // if(textBox1.Text = "" && comboBox2.Text = "" && textBox3.Text = "" &&)
                if (combo_project_name.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (combo_module.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (richText_title.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (richText_steps.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                //else if(comboBox1.Text == "")
                //{
                //    MessageBox.Show("Select Severity");
                //}
                else if (combo_status.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                //else if (comboBox3.Text == "")
                //{
                //    MessageBox.Show("Select Priority");
                //}
                else if (richText_actual_result.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (richText_expected_result.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (combo_Assignee.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (text_reporter.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (txt_to.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                }
                else if (dateTimePicker1.Text == "")
                {
                    MessageBox.Show("Mandatory fileds must not be blank");
                    return;
                }
                else if (combo_module.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Module Name");
                }
                else if (richText_title.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Title");
                }
                else if (richText_steps.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position Steps toReproduce");
                }
                else if (richText_actual_result.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Actual Result");
                }
                else if (richText_expected_result.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Expected Result");
                }
                else if (combo_Assignee.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Assignee Email Id");
                }
                else if (text_reporter.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Reporter Email Id");
                }
                else if (text_filename.Text == "")
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = new CommandType();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into defect_report(Project_Name , Module_Name, Title, Step_to_Reproduce, Severity, Status, Priority, Actual_Result, Expected_Result,Assignee, Reporter, Date)Values('" + combo_project_name.Text + "','" + combo_module.Text + "', '" + richText_title.Text + "', '" + richText_steps.Text + "', '" + combo_severity.Text + "', '" + combo_status.Text + "', '" + combo_priority.Text + "','" + richText_actual_result.Text + "', '" + richText_expected_result.Text + "','" + combo_Assignee.Text + "', '" + text_reporter.Text + "', '" + dateTimePicker1.Text + "')";
                    cmd.ExecuteNonQuery();

                   // try
                    //{
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        con.Open();
                        //string smtpAddress = "smtp.mail.yahoo.com";
                        int port = 587;
                        bool enableSsl = true;

                        string from = text_reporter.Text;
                        string pass = txt_to.Text;
                     // string to = text_assignee.Text;
                        string body = "The Following Issue has been SUBMITED." + Environment.NewLine + "==============================================" + Environment.NewLine + "Reported by : '" + text_reporter.Text + "'" + Environment.NewLine + "Assigned to : '" + combo_Assignee.Text + "'" + Environment.NewLine + "==============================================" + Environment.NewLine + "Project: '" + combo_project_name.Text + "'" + Environment.NewLine + "Severity: '" + combo_severity.Text + "'" + Environment.NewLine + "Priority: '" + combo_priority.Text + "'" + Environment.NewLine + "Status:'" + combo_status.Text + "'" + Environment.NewLine + "==============================================" + Environment.NewLine + "Date Submitted: '" + dateTimePicker1.Text + "'" + Environment.NewLine + "Last Modified: '" + dateTimePicker1.Text + "'" + Environment.NewLine + "==============================================" + Environment.NewLine + "Description: '" + richText_steps.Text + "'";

                        MailMessage mail = new MailMessage();
                        {
                            string[] to = { combo_Assignee.Text };
                            foreach (var m in to)
                            {
                                mail.To.Add(m);
                            }
                            mail.From = new MailAddress(from);
                         // mail.To.Add(to);
                            mail.IsBodyHtml = true;
                            mail.Body = body;

                            //var smtp = new System.Net.Mail.SmtpClient();
                            //{
                            //    smtp.Host = "smtp.mail.yahoo.com";
                            //    smtp.Port = 465;
                            //    smtp.EnableSsl = true;
                            //    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                            //    smtp.Credentials = new NetworkCredential(text_reporter.Text, txt_to.Text);
                            //    //smtp.Timeout = 20000;
                            //}

                           // smtp.Send(text_reporter.Text,combo_Assignee.Text,"",body);

                            using (SmtpClient client = new SmtpClient("smtp.gmail.com", port))
                            {
                                client.Credentials = new NetworkCredential(text_reporter.Text, txt_to.Text);
                                client.EnableSsl = enableSsl;
                                client.Send(mail);
                            }
                        }
                        MessageBox.Show("Report Send Successfully");
                        con.Close();
                        clear();
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("please enter valid email id or Password or check internet connection");
                    //}
                        return;
                }
               
                    
                else
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    FileStream fs = new FileStream(text_filename.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    byte[] img = new byte[fs.Length];
                    fs.Read(img, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                  
                    cmd.CommandType = new CommandType();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into defect_report(Project_Name , Module_Name, Title, Step_to_Reproduce, Severity, Status, Priority, Actual_Result, Expected_Result,image,Assignee, Reporter, Date, filename)Values('" + combo_project_name.Text + "','" + combo_module.Text + "', '" + richText_title.Text + "', '" + richText_steps.Text + "', '" + combo_severity.Text + "', '" + combo_status.Text + "', '" + combo_priority.Text + "','" + richText_actual_result.Text + "', '" + richText_expected_result.Text + "',@image,'" + combo_Assignee.Text + "', '" + text_reporter.Text + "', '" + dateTimePicker1.Text + "','" + text_filename.Text + "')";
                    MySqlParameter prm = new MySqlParameter("@image", MySqlDbType.VarBinary, img.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, img);
                    cmd.Parameters.Add(prm);
                    cmd.ExecuteNonQuery();

                    try
                    {
                      //  string smtpAddress = "smtp.gmail.com";
                        int port = 587;
                        bool enableSsl = true;

                        string from = text_reporter.Text;
                        string pass = txt_to.Text;
                        // string to = text_assignee.Text;

                        string body = "The Following Issue has been SUBMITED." + Environment.NewLine + "==============================================" + Environment.NewLine + "Reported by : '" + text_reporter.Text + "'" + Environment.NewLine + "Assigned to : '" + combo_Assignee.Text + "'" + Environment.NewLine + "==============================================" + Environment.NewLine + "Project: '" + combo_project_name.Text + "'" + Environment.NewLine + "Severity: '" + combo_severity.Text + "'" + Environment.NewLine + "Priority: '" + combo_priority.Text + "'" + Environment.NewLine + "Status:'" + combo_status.Text + "'" + Environment.NewLine + "==============================================" + Environment.NewLine + "Date Submitted: '" + dateTimePicker1.Text + "'" + Environment.NewLine + "==============================================" + Environment.NewLine + "Description: '" + richText_steps.Text + "'";


                        MailMessage mail = new MailMessage();
                        {
                            string[] to = { combo_Assignee.Text };
                            foreach (var m in to)
                            {
                                mail.To.Add(m);
                            }
                            mail.From = new MailAddress(from);
                         // mail.To.Add(to);
                            mail.IsBodyHtml = true;
                            mail.Body = body;

                            using (SmtpClient client = new SmtpClient("smtp.gmail.com", port))
                            {
                                client.Credentials = new NetworkCredential(text_reporter.Text, txt_to.Text);
                                client.EnableSsl = enableSsl;
                                client.Send(mail);
                            }
                        }
                        MessageBox.Show("Report Send Successfully");
                        con.Close();
                        clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("please enter valid email id or Password or check internet connection");
                    }
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Exception" + i);

            }
        }

        public void assignee_email()
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = new CommandType();
            cmd.Connection = con;
            cmd.CommandText = "Select Distinct email_id from admin";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            combo_Assignee.DataSource = dt;
            combo_Assignee.DisplayMember = "email_id";
            combo_Assignee.ResetText();
            con.Close();

        }
        public void clear()
        {
            combo_project_name.ResetText();
            combo_module.ResetText();
            text_filename.Clear();
            combo_Assignee.ResetText();
            text_reporter.Clear();
            richText_title.Clear();
            richText_steps.Clear();
            richText_actual_result.Clear();
            richText_expected_result.Clear();
            combo_severity.ResetText();
            combo_status.ResetText();
            combo_priority.ResetText();
            txt_to.Clear();
        }

        private void Browse_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"desktop";
            openFileDialog1.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*)|*.BMP;*.JPG;*.GIF;*.PNG;*|" + "All files (*.*)|*.*";
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    filename = op.FileName;
            //    text_filename.Text = op.FileName;
            //  //  picturebox_defect.Image = Image.FromFile(filename);
            //}

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                text_filename.Text = openFileDialog1.FileName;
            }
            openFileDialog1.Dispose();
        }

        private void text_module_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar == ' ' && e.KeyChar != ' ' )
            {
                MessageBox.Show("Special  are not allowed");
                e.Handled = true;
            }
        }

        private void combo_severity_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void combo_status_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void combo_priority_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        //private void text_assignee_KeyPress_1(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.'))
        //    {
        //        MessageBox.Show("Special Characters/number/Space are not allowed");
        //        e.Handled = true;
        //    }
        //}
        //private void text_reporter_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //    if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != '.'))
        //    {
        //        MessageBox.Show("Special Characters/number/Space are not allowed");
        //        e.Handled = true;
        //    }

        //}

        private void combo_project_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txt_to_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit_btn.PerformClick();
            }
        }

        private void combo_project_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            combo_module.Items.Clear();
            con.Open();
            string str1 = "select Module_Name from module where project_name = '" + combo_project_name.Text + "'";
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

        private void combo_module_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void combo_Assignee_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        //private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar == ' ')
        //    {
        //        MessageBox.Show("Special character/Space are not allowed");
        //        e.Handled = true;
        //    }
        //}

        //private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar == ' ')
        //    {
        //        MessageBox.Show("Special character/Space are not allowed");
        //        e.Handled = true;
        //    }
        //}

        //private void richTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar == '"' && e.KeyChar == '.' && e.KeyChar == '(' && e.KeyChar == ')' && e.KeyChar == ',' )
        //    {
        //        MessageBox.Show("Space are not allowed");
        //        e.Handled = true;
        //    }
        //}

        //private void richTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == ' ')
        //    {
        //        MessageBox.Show("Space are not allowed");
        //        e.Handled = true;
        //    }
        //}

    }
}
