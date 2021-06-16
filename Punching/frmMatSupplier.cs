using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginClass;

namespace Punching
{
    public partial class frmMatSupplier : Form
    {
        public frmMatSupplier()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbMatieral.Text == "")
            {
                MessageBox.Show("Please ensure you select a material supplier before attempting to continue!", "Select Supplier", MessageBoxButtons.OK);
                return;
            }
            Login.material = cmbMatieral.Text;
        }
    }
}
