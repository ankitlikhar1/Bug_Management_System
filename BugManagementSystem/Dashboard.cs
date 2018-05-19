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
    public partial class Dashboard : Form
    {
        MySqlConnection con = new MySqlConnection("server = localhost; username = root; password = root; database = manageuser");

        string username;
        string password;
        string designation;

        public Dashboard(string username,string password,string designation)
        {
            this.username = username;
            this.password = password;
            this.designation = designation;
            InitializeComponent();
        }

        public Dashboard()
        {
            InitializeComponent();        
        }
     
        private void button2_Click(object sender, EventArgs e)
        {
            ManageUser user = new ManageUser();
            user.Show();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            Login log = new Login();
            log.Close();
            ManageUser user = new ManageUser();
            user.Close();
            password pass = new password();
            pass.Close();
            Defect_Report report = new Defect_Report();
            report.Close();
            ViewReport view = new ViewReport();
            view.Close();
            StatusReport status = new StatusReport();
            status.Close();       
            this.Close();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();   
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Project proj = new Project();
            proj.Show();
        }

        private void btn_change_pass_Click(object sender, EventArgs e)
        {
            password pass = new password();
            pass.ShowDialog();
        }

        private void btn_defect_report_Click(object sender, EventArgs e)
        {
            Defect_Report df = new Defect_Report();
            df.Show();
        }

        private void btn_status_report_Click(object sender, EventArgs e)
        {
            StatusReport status = new StatusReport(username,password,designation);
            status.Show();
        }

        private void btn_view_issue_Click(object sender, EventArgs e)
        {
            ViewReport view = new ViewReport(username,password,designation);
            view.Show(); 
        }

        private void byn_requirement_Click(object sender, EventArgs e)
        {
            Create_Requirement create = new Create_Requirement();
            create.ShowDialog();
        }

        private void btn_require_Click(object sender, EventArgs e)
        {
            Requirements_Report view = new Requirements_Report(username, password, designation);
            view.ShowDialog();
        }

        private void btn_module_Click(object sender, EventArgs e)
        {
            Module module = new Module();
            module.Show();
        }

        private void btn_client_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            View_Requirement view = new View_Requirement(username, password, designation);
            view.ShowDialog();
        }

        int t1 = 36;
        int t2 = 36;
        int t3 = 36;
        int t4 = 36;

        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
            timer1.Start();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Stop();
            t1 = 36;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (t1 > 180)
            {
                timer1.Stop();
            }
            else
            {
                this.panel2.Size = new Size(this.panel2.Size.Width, t1);
                t1 += 5;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (t2 > 137)
            {
                timer1.Stop();
            }
            else
            {
                this.panel3.Size = new Size(this.panel3.Size.Width, t2);
                t2 += 5;
            }
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
         // this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
            timer2.Start();
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            timer2.Stop();
            t2 = 36;
        }
       
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (t3 > 186)
            {
                timer3.Stop();
            }
            else
            {
                this.panel4.Size = new Size(this.panel4.Size.Width, t3);
                t3 += 5;
            }
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
           // this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
            timer3.Start();
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            timer3.Stop();
            t3 = 38;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
           // this.panel5.Size = new Size(this.panel5.Size.Width, t4);
            timer4.Start();
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            timer4.Stop();
            t4 = 38;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (t4 > 189)
            {
                timer4.Stop();
            }
            else
            {
                this.panel5.Size = new Size(this.panel5.Size.Width, t4);
                t4 += 5;
            }
        }

        private void panel7_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void home_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void btn_logout_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void Dashboard_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            this.panel2.Size = new Size(this.panel2.Size.Width, t1);
            this.panel3.Size = new Size(this.panel3.Size.Width, t2);
            this.panel4.Size = new Size(this.panel4.Size.Width, t3);
            this.panel5.Size = new Size(this.panel5.Size.Width, t4);
        }  
    }
}
