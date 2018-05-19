using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugManagementSystem
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            Login log = new Login();
            this.Hide();
            log.ShowDialog();           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AboutSystem system = new AboutSystem();
            system.Show();
        }
    }
}
