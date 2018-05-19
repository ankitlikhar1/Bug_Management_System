using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BugManagementSystem
{
    public partial class Client : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; username = root; password = root; database = manageuser; sslMode = None");
        string cs = "Data Source=.; Initial Catalog = manageuser; server = localhost; username = root; password = root";

        string str;
        int RowCount;
        
        public Client()
        {
            InitializeComponent();
        }

        private void dataGridView_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex;
            text_id.Text = dataGridView_search.Rows[indexRow].Cells[0].Value.ToString();
            text_client.Text = dataGridView_search.Rows[indexRow].Cells[1].Value.ToString();
            richTxt_description.Text = dataGridView_search.Rows[indexRow].Cells[2].Value.ToString();
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
            cmd.CommandText = "Select * from client where Client_Name = '" + text_search.Text + "'";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Client not found");
            }
            else
            {
                dataGridView_search.DataSource = dt;
            }
            con.Close();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = new CommandType();
            cmd.Connection = con;
            cmd.CommandText = "Select * from client";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_search.DataSource = dt;

            string str = "select count(*) from client";
            cmd = new MySqlCommand(str, con);
            int count = Convert.ToInt16(cmd.ExecuteScalar()) + 1;
            text_id.Text = " " + count;
            con.Close();
            text_client.Clear();
            richTxt_description.Clear();
            text_search.Clear();
            con.Close();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = new CommandType();
            cmd.Connection = con;
            cmd.CommandText = "Select * from client";
            cmd.ExecuteNonQuery();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_search.DataSource = dt;
            dataGridView_search.Columns[0].Visible = false;
            dataGridView_search.Columns[2].Visible = false;

            //code for showing auto_increment value  
            string str = "select count(*) from client";
            cmd = new MySqlCommand(str, con);
            int count = Convert.ToInt16(cmd.ExecuteScalar()) + 1;
            text_id.Text = " " + count;
            con.Close();
        }

        private void bttn_update_Click(object sender, EventArgs e)
        {
            try
            {
                Regex x = new Regex(@".{30,600}");
                if (x.IsMatch(text_client.Text))
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
                if (text_client.Text == "")
                {
                    MessageBox.Show("Mandatory field should not be blank");
                    return;
                }
                else if (text_client.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Project Name");
                    return;
                }
                else if (richTxt_description.Text.StartsWith(" "))
                {
                    MessageBox.Show("Cannot have space in first position in Description");
                    return;
                }
                MySqlCommand cmd = new MySqlCommand("Select * from client where Client_Name = '" + text_client.Text + "' ", con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() || dr.HasRows == true)
                {
                    MessageBox.Show("Duplicate entry are not allowed");
                    con.Close();
                    return;
                }
                dr.Close();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                str = "Select * from client where client_id = '" + text_id.Text + "'";
                cmd = new MySqlCommand(str);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                RowCount = dt.Rows.Count;

                if (RowCount == 0)
                {
                    MessageBox.Show("Please Submit the client Name before updating");
                    return;
                }
                con.Open();
                cmd = new MySqlCommand();
                cmd.CommandType = new CommandType();
                cmd.Connection = con;
                cmd.CommandText = "Update client set Client_Name = '" + text_client.Text + "',description = '" + richTxt_description.Text + "'where client_id = '" + text_id.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Client is Update Successfully");
                text_id.Clear();
                text_client.Clear();
                richTxt_description.Clear();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duplicate record are not allowed");
            }
        }
        private void bttn_clear_Click(object sender, EventArgs e)
        {
            text_id.Clear();
            text_client.Clear();
            richTxt_description.Clear();
        }

        private void bttn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                Regex x = new Regex(@".{30,600}");
                if (x.IsMatch(text_client.Text))
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
                if (text_client.Text == "")
                {
                    MessageBox.Show("Mandatory field should not be blank ");
                    return;
                }
                if (text_client.Text.StartsWith(" "))
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
                    MySqlCommand cmd = new MySqlCommand("Select * from client where Client_Name = '" + text_client.Text + "' ", con);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read() || dr.HasRows == true)
                    {
                        MessageBox.Show("Duplicate entry are not allowed");
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
                    cmd.CommandText = "Insert into client(client_id, Client_Name, description)Values('" + text_id.Text + "','" + text_client.Text + "','" + richTxt_description.Text + "')";
                    cmd.ExecuteNonQuery();
                    text_id.Clear();
                    text_client.Clear();
                    richTxt_description.Clear();
                    MessageBox.Show("Created Successfully");
                    con.Close();
                }

            }
            catch (Exception i)
            {
                MessageBox.Show("Duplicate record are not allowed");
            }
        }

        private void text_client_KeyPress(object sender, KeyPressEventArgs e)
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

        private void text_search_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(cs);
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from client where Client_Name like '" + text_search.Text + "%'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView_search.DataSource = dt;
            con.Close();
        }
    }
}
          