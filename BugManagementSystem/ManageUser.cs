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
using MySql.Data.MySqlClient;


namespace BugManagementSystem
{
    public partial class ManageUser : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; username = root; password = root; database = manageuser; sslMode = None");
        public ManageUser()
        {
            InitializeComponent();
            text_contact.MaxLength = 10;
        }

        public static string setvalues;

        private void submit_btn_Click(object sender, EventArgs e)
        {
          
            try
            {
                Regex re1 = new Regex(@".{3,10}");
                if (!re1.IsMatch(text_firstname.Text))
                {
                    MessageBox.Show("Enter Valid First Name");
                    return;
                }

                Regex re2 = new Regex(@".{3,12}");
                if (!re2.IsMatch(ltext_lastname.Text))
                {
                    MessageBox.Show("Enter Valid Last Name");
                    return;
                }

                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(text_email.Text))
                {
                    MessageBox.Show("Enter Valid Email ID");
                    return;
                }
                Regex rex = new Regex("^([0-9]{1})([0-9]){9}$");
                if (!rex.IsMatch(text_contact.Text))
                {
                    MessageBox.Show("Enter Valid Contact No");
                    return;
                }
                if (combo_designation.Text == "")
                {
                    MessageBox.Show("Please Select Designation");
                    return;
                }
                else if (text_username.Text == "")
                {
                    MessageBox.Show("Please Enter UserName");
                    return;
                }

                else if (text_password.Text == "")
                {
                    MessageBox.Show("Please Enter Password");
                    return;
                }

                Regex re = new Regex(@".{8,13}");
                if (!re.IsMatch(text_password.Text))
                {
                    MessageBox.Show("Passowrd Must be 8 Charecters Long");
                    return;
                }
                if (text_firstname.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position");
                }
                else if (ltext_lastname.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position");
                }
                else if (text_email.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position");
                }
                else if (text_contact.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position");
                }
                else if (text_username.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position");
                }
                else if (text_password.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position");
                }

                else
                {
                    MySqlCommand cmd = new MySqlCommand("Select * from admin where Username = '" + text_username.Text + "' ", con);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read() || dr.HasRows == true)
                    {
                        MessageBox.Show("Username " + text_username.Text + " Already exist");
                        con.Close();
                    }
                    else
                    {
                        dr.Close();
                        cmd = new MySqlCommand();
                        cmd.CommandType = new CommandType();
                        cmd.Connection = con;
                        cmd.CommandText = "Insert into admin(first_name, last_name, email_id, contact_no, designation, username, password)Values('" + text_firstname.Text + "','" + ltext_lastname.Text + "','" + text_email.Text + "','" + text_contact.Text + "','" + combo_designation.Text + "','" + text_username.Text + "','" + text_password.Text + "')";
                        cmd.ExecuteNonQuery();
                        //con.Close();
                        clear();
                        MessageBox.Show("Account Successfully Created");
                        con.Close();
                    }
                }
                setvalues = text_email.Text;
            }
            catch (Exception i)
            {
                MessageBox.Show("Exception"+i);
            }

           
        }
        private void text_firstname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Only Characters are allowed");
                e.Handled = true;
                return;
            }

        }

        private void text_lastname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Only Characters are allowed");
                e.Handled = true;
            }
        }

        private void text_contact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Only Numbers are allowed");
                e.Handled = true;
            }
        }

        private void combo_designation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

      
        private void text_contact_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(text_contact,"Enter Contact NO(10 Digits)");
        }

        public void clear()
        {
            text_firstname.Clear();
            ltext_lastname.Clear();
            text_email.Clear();
            text_contact.Clear();
            text_username.Clear();
            text_password.Clear();
            combo_designation.ResetText();
        }

        private void text_password_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                MessageBox.Show("Space are not allowed");
                e.Handled = true;
            }
        }

        private void text_firstname_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(text_firstname, "Enter First Name(Only Characters)");
        }

        private void ltext_lastname_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(ltext_lastname, "Enter Last Name(Only Characters)"); 
        }

        private void text_email_MouseHover_1(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(text_email, " Enter Email ID(abc@gmail.com)");   
        }

        private void text_contact_MouseHover_1(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(text_contact, "Enter contact No(Only 10 Digits)");
        }

        private void combo_designation_MouseHover_1(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(combo_designation, "Select Designation");
        }

        private void text_username_MouseHover_1(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(text_username, "Enter UserName");
        }

        private void text_password_MouseHover_1(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ShowAlways = true;
            tt.SetToolTip(text_password, "Enter Password");
        }

        private void text_contact_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Enter valid Contact No");
                e.Handled = true;
            }
        }

        private void text_username_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                MessageBox.Show("Enter valid Username");
                e.Handled = true;
            }
        }

        private void text_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                MessageBox.Show("Space are not allowed");
                e.Handled = true;
            }
        }

        private void text_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit_btn.PerformClick();
            }
        }

        //private void text_email_Enter(object sender, EventArgs e)
        //{
        //    if (text_email.Text == "Abc@.com")
        //    {
        //        text_email.Text = "";
        //        text_email.ForeColor = black;
        //    }
        //}

        //private void text_email_Leave(object sender, EventArgs e)
        //{
        //    if (text_email.Text == "")
        //    {
        //        text_email.Text = "Abc@.com";
        //        text_email.ForeColor = Color.Silver;
        //    }
        //}


        
    }
}
    

    
    
    



