using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Punching
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            string sql = "select id as [Batch ID],qname as [Description],machine as [Machine],datecomplete as [Date Complete],qcomplete as [QComplete] from dbo.batch_header  order by qid desc";
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    formatting();
                }
                sql = "exec usp_get_department_work_availability 'Punching'";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    lblHours.Text = cmd.ExecuteScalar().ToString();
                }
                conn.Close();
            }


            //start a timer
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; //every 60 seconds
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //this is the code that will fire every 60seconds
            //dbo.bending_stock_items
            bendingAlert();
        }

        private void bendingAlert()
        {
            string sql = "select [description],requested_amount from dbo.bending_stock_items where request_yn = -1";
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    //read from the dt 
                    if (dt.Rows.Count > 1)
                    {
                        tempLabel.Invoke((Action)delegate//only way to change controls while there is multiple threads running ???
                        {
                            tempLabel.Text = "**MULTIPLE LOW STOCK ITEMS**";
                            tempLabel.Visible = true;
                            tempLabel.BackColor = Color.PaleVioletRed;
                        });
                    }
                    else if (dt.Rows.Count == 1)
                    {
                        tempLabel.Invoke((Action)delegate
                        {
                            tempLabel.Text = "Bending Dpt Requested: " + dt.Rows[0][0].ToString() + " x " + dt.Rows[0][1].ToString();
                            tempLabel.Visible = true;
                            tempLabel.BackColor = Color.PaleVioletRed;
                        });
                    }
                    else
                        tempLabel.Invoke((Action)delegate
                        {
                            tempLabel.Visible = false;
                        });
                }
                conn.Close();
            }
        }

        private void formatting()
        {
            try
            {
                dataGridView1.Columns.Remove("Complete");
            }
            catch
            {
            }
            //also add the button here too
            DataGridViewButtonColumn completeButtonColumn = new DataGridViewButtonColumn();
            completeButtonColumn.Name = "Complete";
            completeButtonColumn.Text = "Complete";
            completeButtonColumn.UseColumnTextForButtonValue = true;
            int columnIndex = (dataGridView1.Columns.Count);
            if (dataGridView1.Columns["Complete"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, completeButtonColumn);
            }
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int index = dataGridView1.Columns["QComplete"].Index;
                if (Convert.ToInt32(row.Cells[index].Value) == -1)
                    row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.Columns["Complete"].Index;
            if (e.ColumnIndex == index)
            {
                MessageBox.Show("Complete clicked");
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int index = dataGridView1.Columns["QComplete"].Index;
                if (Convert.ToInt32(row.Cells[index].Value) == -1)
                    row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
            }
        }

        private void txtDoorLookup_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                doorLookup(); //this can fire from two places so its easier to just stick it in its own void
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            doorLookup();
        }

        private void doorLookup()
        {
            string sql = "SELECT dbo.batch_header.qid FROM dbo.batch_programs INNER JOIN dbo.batch_header ON dbo.batch_programs.header_id = dbo.batch_header.id " +
                "where door_id = " + txtDoorLookup.Text + " GROUP BY dbo.batch_header.qid, dbo.batch_programs.door_id ";

            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    var getdata = cmd.ExecuteScalar();
                    if (getdata != null)
                        MessageBox.Show("Door found on batch " + getdata.ToString());
                    else
                        MessageBox.Show("Door not found on any batch");

                    txtDoorLookup.Text = "";

                }
                conn.Close();
            }
        }

        private void txtDoorLookup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                applyFilter(); //this can fire from two places so its easier to just stick it in its own void
            }
        }

        private void applyFilter()
        {
            try
            {
                dataGridView1.Columns.Remove("Complete");
            }
            catch
            {
            }
            if (txtBatchID.TextLength > 0)
            {
                dataGridView1.DataSource = null;
                //filter batch id
                string sql = "select id as [Batch ID],qname as [Description],machine as [Machine],datecomplete as [Date Complete],qcomplete as [QComplete] from dbo.batch_header where id like '%" + txtBatchID.Text + "%'  order by qid desc";
                using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        formatting();
                    }
                }
            }
            else
            {
                string sql = "select id as [Batch ID],qname as [Description],machine as [Machine],datecomplete as [Date Complete],qcomplete as [QComplete] from dbo.batch_header  order by qid desc";
                using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        formatting();
                    }
                }
            }

        }

        private void txtBatchID_TextChanged(object sender, EventArgs e)
        {
            applyFilter();
        }



        private void markAsComplete(int batch_id)
        {
            //this is the big one


            string sql = "";

            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                }
            }
        }
    }
}
