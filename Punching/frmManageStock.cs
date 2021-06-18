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
    public partial class frmManageStock : Form
    {
        public frmManageStock()
        {
            InitializeComponent();
            //unbent stock
            string sql = "select id as [ID], [description] as [Description], amount_in_stock as [Unbent Stock] from dbo.unbent_stock";
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dgvUnbentStock.DataSource = dt;
                }
                sql = "SELECT dbo.bending_stock_items.id as [ID], dbo.bending_stock_items.[description] as [Description], dbo.bending_stock_items.current_stock AS [Bent Stock] " +
                    "FROM dbo.bending_stock_items WHERE(((dbo.bending_stock_items.visible_in_punching) = -1))";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dgvBentStock.DataSource = dt;
                }
                //fill comboboxes too~
                sql = "SELECT [description] FROM dbo.sheet_stock where [description] <> ''";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader DR = cmd.ExecuteReader();
                    while (DR.Read())
                    {
                        cmbSheetType.Items.Add(DR[0]);
                    }
                    DR.Close();
                }
                sql = "SELECT [description] FROM dbo.unbent_stock where [description] <> ''";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader DR = cmd.ExecuteReader();
                    while (DR.Read())
                    {
                        cmbSheetTypeGroupBox.Items.Add(DR[0]);
                    }
                    DR.Close();
                }

                foreach (DataGridViewColumn col in dgvBentStock.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                foreach (DataGridViewColumn col in dgvUnbentStock.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgvBentStock.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvUnbentStock.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


                radioYes.Checked = true;

                conn.Close();
            }
        }

        private void radioYes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioYes.Checked == true)
            {
                txtReason.Visible = false;
                lblReason.Visible = false;

                txtQtyGroupBox.Visible = true;
                cmbSheetTypeGroupBox.Visible = true;
                lblSheetTypeGroupBox.Visible = true;
                lblQtyGroupBox.Visible = true;
            }
            if (radioNo.Checked == true)
            {
                txtReason.Visible = true;
                lblReason.Visible = true;
                txtQtyGroupBox.Visible = false;
                cmbSheetTypeGroupBox.Visible = false;
                lblSheetTypeGroupBox.Visible = false;
                lblQtyGroupBox.Visible = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //null chekcks 

            //these two are universal
            if (String.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Please enter Quantity in Sheets before updating!", "Missing Data", MessageBoxButtons.OK);
                return;
            }
            if (cmbSheetType.Text == "")
            {
                MessageBox.Show("Please select a sheet type from the drop down menu!", "Missing Data", MessageBoxButtons.OK);
                return;
            }

            //radio button checks
            if (radioYes.Checked == true)
            {
                if (String.IsNullOrEmpty(txtQtyGroupBox.Text))
                {
                    MessageBox.Show("Please make sure you enter a quantity of tophats and flat hats before continuing!", "Missing Data", MessageBoxButtons.OK);
                    return;
                }
                if (cmbSheetTypeGroupBox.Text == "")
                {
                    MessageBox.Show("Please make sure you select a top/flat hat from the drop down list!", "Missing Data", MessageBoxButtons.OK);
                    return;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(txtReason.Text))
                {
                    MessageBox.Show("Please give a reason for sheet deduction!", "Missing Data", MessageBoxButtons.OK);
                    return;
                }
            }

            //user check
            DialogResult result = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //first thing to get is the stockcode of the combo boxes
                string sql = "";
                using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
                {
                    
                    int cmbSheetTypeStockCode  = 0; //bent?
                    int cmbSheetTypeGroupBoxStockCode = 0; //unbent
                    sql = "SELECT [description] FROM dbo.sheet_stock where [description] = '" + cmbSheetType.Text + "' ";
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmbSheetTypeStockCode = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sql = "UPDATE dbo.stock SET quantity_sheets = quantity_sheets - " + txtQty.Text + " WHERE stock_code = " + cmbSheetTypeStockCode;
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    //if tophats-flathats are selected  then use the combo box as the reason
                    if (radioYes.Checked == true)
                    {
                        //
                        sql = "SELECT [description] FROM dbo.unbent_stock where [description] = '" + cmbSheetTypeGroupBox.Text + "'";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmbSheetTypeGroupBoxStockCode = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                            sql = "INSERT INTO dbo_manual_sheet_update_log (sheet_qty, date_created, sheet_type, reason) VALUES (" + txtQty.Text + ", GETDATE(), '" + cmbSheetType.Text + "','" + cmbSheetTypeGroupBox.Text + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        sql = "UPDATE dbo.unbent_stock SET amount_in_stock = amount_in_stock + " + txtQtyGroupBox.Text + " where id = " + cmbSheetTypeGroupBoxStockCode.ToString();
                    }
                    else
                    {
                        sql = "INSERT INTO dbo.manual_sheet_update_log (sheet_qty,date_created,sheet_type,reason) VALUES (" + txtQty + ", GETDATE(),'" + cmbSheetType.Text + "','" + txtReason.Text + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            }
        }
    }
}
