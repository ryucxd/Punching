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
    public partial class frmBendingStock : Form
    {
        public frmBendingStock()
        {
            InitializeComponent();
            string sql = "select id,[description] as [Description],requested_amount as [Requested Amount] FROM dbo.bending_stock_items WHERE dbo.bending_stock_items.request_yn = -1";
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
                completeButtonColumn.Text = "✔";
                completeButtonColumn.UseColumnTextForButtonValue = true;
                int columnIndex = (dataGridView1.Columns.Count);
                if (dataGridView1.Columns["Complete"] == null)
                {
                    dataGridView1.Columns.Insert(columnIndex, completeButtonColumn);
                }
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                conn.Close();
                dataGridView1.ReadOnly = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.Columns["Complete"].Index;
            if (e.ColumnIndex == index)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            //marks all green entries as complete
            DialogResult result = MessageBox.Show("This will mark all green rows as complete, are you sure?", "Mark as Complete!", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //loop through each row and mark the green 
                using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
                {
                    conn.Open();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.DefaultCellStyle.BackColor == Color.DarkSeaGreen)
                        {
                            int index = 0;
                            if (dataGridView1.Columns.Contains("id"))
                            {
                                index = dataGridView1.Columns["id"].Index;
                            }
                                string sql = "UPDATE dbo.bending_stock_items SET request_yn = NULL,requested_amount = NULL WHERE id = " + row.Cells[index].Value.ToString();   //
                            using (SqlCommand cmd = new SqlCommand(sql, conn))
                            {
                                MessageBox.Show(sql);
                            }
                        }
                    }
                    conn.Close();
                }
            }
        }
    }
}
