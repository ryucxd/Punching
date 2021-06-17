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
    public partial class frmLateLoadReport : Form
    {
        public frmLateLoadReport(string LateLoad,DateTime date)
        {
            InitializeComponent();
            string sql = "";
            lblDate.Text = date.ToString("yyyy-MM-dd");
            if (LateLoad == "Late")
            {
                lblTitle.Text = "Late Punching Sheet";
               
                this.Text = "Late Punching";
                sql = "SELECT  dbo.door.id as [Door ID],batched as [Batched],finn as [Finn],rainer as [Rainer],order_date as [Booked In],program_date as [Date Programmed], " +
                    "dbo.SALES_LEDGER.[NAME] as [Customer],CAST(quantity_same as nvarchar) + ' x ' + door_type_description as [Door Type],infill_description as [Infil]," +
                    "CASE WHEN fast_track = -1 then 'FT' ELSE lead_time end as [Lead Time],[status_description] as [Status Description],urgent_flag,case when programed_by_id > 0 AND checked_by_id > 0 then -1 ELSE 0 END as [checked_and_programmed]" +
                    " FROM dbo.door_program RIGHT JOIN dbo.door LEFT JOIN SALES_LEDGER ON dbo.door.customer_acc_ref = SALES_LEDGER.ACCOUNT_REF " +
                    "LEFT JOIN dbo.door_type ON dbo.door.door_type_id = dbo.door_type.id ON dbo.door_program.door_id = dbo.door.id " +
                    "LEFT JOIN dbo.door_container ON dbo.door.order_id = dbo.door_container.order_id LEFT JOIN dbo.infill on dbo.door.infill_id = dbo.infill.id " +
                    "LEFT JOIN dbo.[status] on dbo.door.status_id = dbo.[status].id WHERE cast(dbo.door.date_punch as date) < cast('" + date.ToString("yyyyMMdd") + "' as date) And " +
                    "(dbo.door.complete_punch = 0 or dbo.door.complete_punch is null) And(dbo.door.status_id = 1 Or dbo.door.status_id = 2) And(dbo.door.test_identifier = 0 Or dbo.door.test_identifier Is Null) ORDER BY dbo.door.date_punch, dbo.door.id";
            }
            else
            {
                lblTitle.Text = "Punching Total Sheet";
                this.Text = "Punching Total";
                sql = "SELECT dbo.door.id as [Door ID],batched as [Batched],finn as [Finn],rainer as [Rainer],dbo.door_container.order_date as [Booked In],program_date as [Date Programmed], " +
                    "dbo.SALES_LEDGER.[NAME] as [Customer],CAST(quantity_same as nvarchar) + ' x ' + door_type_description as [Door Type],infill_description as [Infil], " +
                    "CASE WHEN fast_track = -1 then 'FT' ELSE lead_time end as [Lead Time],[status_description] as [Status Description],urgent_flag,case when programed_by_id > 0 AND checked_by_id > 0 then -1 ELSE 0 END as [checked_and_programmed] " +
                    "FROM dbo.door_container RIGHT JOIN dbo.door_program RIGHT JOIN dbo.door LEFT JOIN SALES_LEDGER ON dbo.door.customer_acc_ref = SALES_LEDGER.ACCOUNT_REF " +
                    "LEFT JOIN dbo.door_type ON dbo.door.door_type_id = dbo.door_type.id ON dbo.door_program.door_id = dbo.door.id ON dbo.door_container.order_id = dbo.door.order_id " +
                    "LEFT JOIN dbo.door_container AS door_container_1 ON dbo.door.order_id = door_container_1.order_id LEFT JOIN dbo.infill on dbo.door.infill_id = dbo.infill.id LEFT JOIN dbo.[status] on dbo.door.status_id = dbo.[status].id " +
                    "WHERE cast(dbo.door.date_punch as date) = CAST('" + date.ToString("yyyyMMdd") + "' as date) And(dbo.door.complete_punch = 0 or dbo.door.complete_punch is null) And(dbo.door.status_id = 1 Or dbo.door.status_id = 2) And " +
                    "(dbo.door.test_identifier = 0 Or dbo.door.test_identifier Is Null) ORDER BY dbo.door.id";
            }

            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    conn.Close();
                }
            }

        }

        private void frmLateLoadReport_Shown(object sender, EventArgs e)
        {
            int urgentIndex = dataGridView1.Columns["urgent_flag"].Index;
            int checkedAndProgrammed = dataGridView1.Columns["checked_and_programmed"].Index;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[urgentIndex].Visible = false;
            dataGridView1.Columns[checkedAndProgrammed].Visible = false;

            //loop for colours 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (Convert.ToString(row.Cells[urgentIndex].Value) == "-1")
                {
                    row.DefaultCellStyle.BackColor = Color.PaleVioletRed;
                }
                if (Convert.ToString(row.Cells[checkedAndProgrammed].Value) == "-1")
                {
                    row.Cells[0].Style.BackColor = Color.DarkSeaGreen;
                }

            }
        }
    }
}
