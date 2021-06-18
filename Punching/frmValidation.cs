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

namespace Punching
{
    public partial class frmValidation : Form
    {
        public frmValidation()
        {
            InitializeComponent();
        }

        private void txtDoor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            string sql = "";
            int isComplete = 0;
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                conn.Open();
                //check for door id 
                sql = "SELECT complete_punch FROM dbo.door where id = " + txtDoor.Text;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var getdata = cmd.ExecuteScalar();
                    if (getdata != null)
                        isComplete = Convert.ToInt32(cmd.ExecuteScalar());
                    else
                    {
                        MessageBox.Show("This is not a valid door number", "", MessageBoxButtons.OK);
                        return;
                    }
                }
                if (isComplete == 0)
                {
                    //usp_ryucxd_punch_door_validation
                    using (SqlCommand cmd = new SqlCommand("usp_ryucxd_punch_door_validation", conn)) //this procedure returns a select but we dont need to read this here 
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Door Validated");
                        conn.Close();
                        this.Close();
                    }
                }
                conn.Close();
            }
        }
    }
}
