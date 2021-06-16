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
using LoginClass;

namespace Punching
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
            //fill combobox with everyone whos assigned to punching today
            string sql = "select forename + ' ' + surname from dbo.power_plan_staff a left join dbo.power_plan_date b on b.id = a.date_id left join[user_info].dbo.[user] c on c.id = a.staff_id where date_plan = CAST(getdate() as date) and(a.department = 'Punching')";
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader DR = cmd.ExecuteReader(); 
                    while (DR.Read())
                    {
                        cmbFullName.Items.Add("Corey Jones");
                        cmbFullName.Items.Add(DR[0]);
                    }
                }
                cmbFullName.SelectedIndex = 0;
                conn.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //check for nulls
            string fullName = cmbFullName.Text;
            string password = txtPassword.Text;

            Login login = new Login("", "");

            if (login.attemptLogin(fullName, password))
            {
                frmMain frm = new frmMain();
                this.Hide();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong password!", "!!!", MessageBoxButtons.OK);
                txtPassword.Text = "";
                    }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.PerformClick();
        }
    }
}
