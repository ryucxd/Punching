using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punching
{
    public partial class frmRemoveFromBatch : Form
    {
        public int _queueID { get; set; }
        public string _queueName { get; set; }
        public frmRemoveFromBatch(string qname)
        {
            InitializeComponent();
            _queueName = qname;
            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"\\fujitsu\fp\production.mdb"); //writes to the file on finn //\\designsvr1\DropBox\production.mdb   // @"\\designsvr1\DropBox\production.mdb");//  \\fujitsu\fp\production.mdb
            connection.Open();
            string sql = "SELECT QUEUEID FROM QUEUE WHERE QNAME = '" + qname + "'";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            int actualQID = 0;
            actualQID = Convert.ToInt32(cmd.ExecuteScalar());
            _queueID = actualQID;
            lblNcFile.Text = qname;
            lblqid.Text = actualQID.ToString() ;
            sql = "SELECT QUEUEID,NCFILE,NCFILE as [fullNC] FROM QUEUEPROG WHERE QUEUEID = " + actualQID;
            OleDbCommand command = new OleDbCommand(sql, connection);
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string new_nc = dt.Rows[i][1].ToString();
                new_nc = new_nc.Substring(3);
                new_nc = new_nc.Substring(0,new_nc.Length - 3);
                dt.Rows[i][1] = new_nc;
            }
            dataGridView1.DataSource = dt;
            //also add the button here too
            DataGridViewButtonColumn completeButtonColumn = new DataGridViewButtonColumn();
            completeButtonColumn.Name = "Remove";
            completeButtonColumn.Text = "✖";
            completeButtonColumn.UseColumnTextForButtonValue = true;
            int columnIndex = (dataGridView1.Columns.Count);
            if (dataGridView1.Columns["Remove"] == null)
            {
                dataGridView1.Columns.Insert(columnIndex, completeButtonColumn);
            }
            connection.Close();
            connection.Dispose(); //properly close my connections

            //here we trim the column to only show the program number
  
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.Columns["Remove"].Index;

            if (e.ColumnIndex == index)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.PaleVioletRed;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.Columns["fullNC"].Index;
            DialogResult result =  MessageBox.Show("This will remove every red line from the batch, are you sure?", "Remove From Batch", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.DefaultCellStyle.BackColor == Color.PaleVioletRed)
                    {
                        string temp = row.Cells[index].Value.ToString();
                        temp = temp.Substring(3);
                        string sql = "DELETE * FROM QUEUEPROG WHERE NCFILE LIKE '*" + temp  + "*' AND QUEUEID = " + _queueID;
                        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"\\fujitsu\fp\production.mdb"); //writes to the file on finn //\\designsvr1\DropBox\production.mdb   // @"\\designsvr1\DropBox\production.mdb");//  \\fujitsu\fp\production.mdb
                        connection.Open();
                        OleDbCommand command = new OleDbCommand(sql, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }

        }
    }
}
