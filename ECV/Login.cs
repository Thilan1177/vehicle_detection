using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ECV
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            string one = textBoxuname.Text;
            string p = textBoxPassword.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxuname.Text=="")
            {
                MessageBox.Show("Please Enter User Name");
            }
            else if (textBoxPassword.Text =="")
            {
                MessageBox.Show("Please Enter Password");
            }
            else if (textBoxuname.Text =="" && textBoxPassword.Text =="")
            {
                MessageBox.Show("Please Enter User Name and Password");
            }
            else
            {
                SqlConnection cn = new SqlConnection("Data Source=THILAN;Initial Catalog=vehicle;Persist Security Info=True;User ID=sa;Password=smtk");

                cn.Open();
                SqlCommand cmd = new SqlCommand("select * from users where uname='" + textBoxuname.Text.Trim() + "' and password='" + textBoxPassword.Text.Trim() + "'", cn);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    count += 1;
                }
                if (count == 1)
                {
                    Menu menu = new Menu();
                    menu.Show();
                }
                else if (count > 0)
                {
                    MessageBox.Show("Duplicate user name and password");
                }
                else
                {
                    MessageBox.Show("Incorrect user name or password");
                }
                textBoxuname.Clear();
                textBoxPassword.Clear();
            }
        }
    }
}
