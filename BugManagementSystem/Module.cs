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
    public partial class Module : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; username = root; password = root; database = manageuser");
        string cs = "Data Source=.; Initial Catalog = manageuser; server = localhost; username = root; password = root";

        string str;
        int RowCount;

        public Module()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                Regex x = new Regex(@".{30,600}");
                if (x.IsMatch(txt_module.Text))
                {
                    MessageBox.Show("Too Long text for Project Name");
                    return;
                }

                Regex reg1 = new Regex(@".{700,1000}");
                if (reg1.IsMatch(richTxt_description.Text))
                {
                    MessageBox.Show("Too Long Text for Description");
                    return;
                }
                if (txt_module.Text == "")
                {
                    MessageBox.Show("Mandatory field should not be blank ");
                    return;
                }
                else if (combo_proj.Text == "")
                {
                    MessageBox.Show("Mandatory field should not be blank");
                }
                if (txt_module.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Project Name");
                    return;
                }
                if (richTxt_description.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Description");
                    return;
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("Select * from module where Module_Name = '" + txt_module.Text + "' ", con);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read() || dr.HasRows == true)
                    {
                        MessageBox.Show("'"+txt_module.Text+"' '"+txt_module.Text+"' module is already exist");
                        con.Close();
                        return;
                    }
                    dr.Close();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    cmd = new MySqlCommand();
                    cmd.CommandType = new CommandType();
                    cmd.Connection = con;
                    cmd.CommandText = "Insert into module(module_id, project_name, Module_Name, description)Values('" + txt_id.Text + "','" + combo_proj.Text + "','" + txt_module.Text + "','" + richTxt_description.Text + "')";
                    cmd.ExecuteNonQuery();
                    txt_id.Clear();
                    txt_module.Clear();
                    combo_proj.ResetText();
                    richTxt_description.Clear();
                    MessageBox.Show("Module is Created Successfully");
                    clear();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duplicate entry are not allowed");
            }
        }
        private void Module_Load(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = new CommandType();
            cmd.Connection = con;
            cmd.CommandText = "Select * from module";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_search.DataSource = dt;
            dataGridView_search.Columns[0].Visible = false;
             dataGridView_search.Columns[2].Visible = false;
           // dataGridView_search.Columns[3].Visible = false;
            
            //code for showing auto_increment value  
            string str = "select count(*) from Module";
            cmd = new MySqlCommand(str, con);
            int count = Convert.ToInt16(cmd.ExecuteScalar()) + 1;
            txt_id.Text = " " + count;
            project_name();
            con.Close();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                Regex x = new Regex(@".{30,600}");
                if (x.IsMatch(txt_module.Text))
                {
                    MessageBox.Show("Too Long text for Project Name");
                    return;
                }

                Regex reg1 = new Regex(@".{700,1000}");
                if (reg1.IsMatch(richTxt_description.Text))
                {
                    MessageBox.Show("Too Long Text for Description");
                    return;
                }

                if (txt_module.Text == "")
                {
                    MessageBox.Show("Mandatory field should not be blank");
                    return;
                }
                else if (combo_proj.Text == "")
                {
                    MessageBox.Show("Mandatory field should not be blank");
                }
                else if (txt_module.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Project Name");
                    return;
                }
                else if (richTxt_description.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Description");
                    return;
                }
                MySqlCommand cmd = new MySqlCommand("Select * from module where Module_Name = '" + txt_module.Text + "' ", con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() || dr.HasRows == true)
                {
                    MessageBox.Show("Enter valid text to Update");
                    con.Close();
                    return;
                }
                dr.Close();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                str = "Select * from module where module_id = '" + txt_id.Text + "'";
                cmd = new MySqlCommand(str);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                RowCount = dt.Rows.Count;

                if (RowCount == 0)
                {
                    MessageBox.Show("Please Submit the Module Name before updating");
                    return;
                }
                con.Open();
                cmd = new MySqlCommand();
                cmd.CommandType = new CommandType();
                cmd.Connection = con;
                cmd.CommandText = "Update module set Module_Name = '" + txt_module.Text + "', Project_Name = '" + combo_proj.Text + "',description = '" + richTxt_description.Text + "'where module_id = '" + txt_id.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Module is Update Successfully");
                txt_id.Clear();
                txt_module.Clear();
                combo_proj.ResetText();
                richTxt_description.Clear();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duplicate entry are not allowed");
            }
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_id.Clear();
            txt_module.Clear();
            combo_proj.ResetText();
            richTxt_description.Clear();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            //txt_project.Text = "";
            //text_project_id.Text = "";
            //txt_search.Text = "";
            //richTxt_description.Text = "";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = new CommandType();
            cmd.Connection = con;
            cmd.CommandText = "Select * from module";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_search.DataSource = dt;

            string str = "select count(*) from module";
            cmd = new MySqlCommand(str, con);
            int count = Convert.ToInt16(cmd.ExecuteScalar()) + 1;
            txt_id.Text = " " + count;
            con.Close();
            clear();
        }

        public void clear()
        {
            combo_proj.ResetText();
            txt_module.Clear();
            txt_search.Clear();
            richTxt_description.Clear();
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = new CommandType();
            cmd.Connection = con;
            cmd.CommandText = "Select * from module where Project_Name = '" + txt_search.Text + "'";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Project not found");
            }
            else
            {
                dataGridView_search.DataSource = dt;
            }
            con.Close();
        }
        void project_name()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = new CommandType();
            cmd.Connection = con;
            cmd.CommandText = "select Distinct Project_Name from project";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            combo_proj.DataSource = dt;
            combo_proj.DisplayMember = "Project_Name";
            combo_proj.ResetText();
            con.Close();
        }

        private void dataGridView_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            txt_id.Text = dataGridView_search.Rows[indexRow].Cells[0].Value.ToString();
            txt_module.Text = dataGridView_search.Rows[indexRow].Cells[2].Value.ToString();
            combo_proj.Text = dataGridView_search.Rows[indexRow].Cells[1].Value.ToString();
            richTxt_description.Text = dataGridView_search.Rows[indexRow].Cells[3].Value.ToString();
        }

        private void txt_module_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar != ' '))
            {
                MessageBox.Show("Special Character/Space are not allowed");
                e.Handled = true;
            }
        }

        private void richTxt_description_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && (e.KeyChar != ' ') && (e.KeyChar != '.') && (e.KeyChar != ',') && (e.KeyChar != ('(') && (e.KeyChar != ')') && (e.KeyChar != ';')))
            {
                MessageBox.Show("Special character/Number/Space are not allowed");
                e.Handled = true;
            }
        }

        private void combo_proj_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(cs);
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from module where Module_Name like '" + txt_search.Text + "%'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_search.DataSource = dt;
            con.Close();
        }
    }
}
