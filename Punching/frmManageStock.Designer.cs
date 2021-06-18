namespace Punching
{
    partial class frmManageStock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageStock));
            this.dgvUnbentStock = new System.Windows.Forms.DataGridView();
            this.dgvBentStock = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.cmbSheetType = new System.Windows.Forms.ComboBox();
            this.lblSheetType = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioNo = new System.Windows.Forms.RadioButton();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.RichTextBox();
            this.lblSheetTypeGroupBox = new System.Windows.Forms.Label();
            this.cmbSheetTypeGroupBox = new System.Windows.Forms.ComboBox();
            this.radioYes = new System.Windows.Forms.RadioButton();
            this.lblQtyGroupBox = new System.Windows.Forms.Label();
            this.txtQtyGroupBox = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnbentStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBentStock)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUnbentStock
            // 
            this.dgvUnbentStock.AllowUserToAddRows = false;
            this.dgvUnbentStock.AllowUserToDeleteRows = false;
            this.dgvUnbentStock.AllowUserToResizeColumns = false;
            this.dgvUnbentStock.AllowUserToResizeRows = false;
            this.dgvUnbentStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnbentStock.Location = new System.Drawing.Point(302, 32);
            this.dgvUnbentStock.Name = "dgvUnbentStock";
            this.dgvUnbentStock.RowHeadersVisible = false;
            this.dgvUnbentStock.Size = new System.Drawing.Size(337, 591);
            this.dgvUnbentStock.TabIndex = 0;
            // 
            // dgvBentStock
            // 
            this.dgvBentStock.AllowUserToAddRows = false;
            this.dgvBentStock.AllowUserToDeleteRows = false;
            this.dgvBentStock.AllowUserToResizeColumns = false;
            this.dgvBentStock.AllowUserToResizeRows = false;
            this.dgvBentStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBentStock.Location = new System.Drawing.Point(661, 32);
            this.dgvBentStock.Name = "dgvBentStock";
            this.dgvBentStock.RowHeadersVisible = false;
            this.dgvBentStock.Size = new System.Drawing.Size(337, 591);
            this.dgvBentStock.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(302, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Unbent Stock:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(661, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(337, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bent Stock";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(12, 239);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(83, 20);
            this.txtQty.TabIndex = 4;
            // 
            // lblQty
            // 
            this.lblQty.Location = new System.Drawing.Point(12, 201);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(83, 35);
            this.lblQty.TabIndex = 5;
            this.lblQty.Text = "Quantity in Sheets";
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // cmbSheetType
            // 
            this.cmbSheetType.FormattingEnabled = true;
            this.cmbSheetType.Location = new System.Drawing.Point(107, 238);
            this.cmbSheetType.Name = "cmbSheetType";
            this.cmbSheetType.Size = new System.Drawing.Size(189, 21);
            this.cmbSheetType.TabIndex = 6;
            // 
            // lblSheetType
            // 
            this.lblSheetType.Location = new System.Drawing.Point(107, 200);
            this.lblSheetType.Name = "lblSheetType";
            this.lblSheetType.Size = new System.Drawing.Size(189, 35);
            this.lblSheetType.TabIndex = 7;
            this.lblSheetType.Text = "Sheet Type";
            this.lblSheetType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioNo);
            this.groupBox1.Controls.Add(this.lblReason);
            this.groupBox1.Controls.Add(this.txtReason);
            this.groupBox1.Controls.Add(this.lblSheetTypeGroupBox);
            this.groupBox1.Controls.Add(this.cmbSheetTypeGroupBox);
            this.groupBox1.Controls.Add(this.radioYes);
            this.groupBox1.Controls.Add(this.lblQtyGroupBox);
            this.groupBox1.Controls.Add(this.txtQtyGroupBox);
            this.groupBox1.Location = new System.Drawing.Point(15, 277);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 239);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TopHat / FlatHat?";
            // 
            // radioNo
            // 
            this.radioNo.AutoSize = true;
            this.radioNo.Location = new System.Drawing.Point(15, 137);
            this.radioNo.Name = "radioNo";
            this.radioNo.Size = new System.Drawing.Size(39, 17);
            this.radioNo.TabIndex = 1;
            this.radioNo.TabStop = true;
            this.radioNo.Text = "No";
            this.radioNo.UseVisualStyleBackColor = true;
            // 
            // lblReason
            // 
            this.lblReason.Location = new System.Drawing.Point(21, 145);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(251, 28);
            this.lblReason.TabIndex = 14;
            this.lblReason.Text = "Reason:";
            this.lblReason.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(24, 176);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(251, 45);
            this.txtReason.TabIndex = 13;
            this.txtReason.Text = "";
            // 
            // lblSheetTypeGroupBox
            // 
            this.lblSheetTypeGroupBox.Location = new System.Drawing.Point(101, 58);
            this.lblSheetTypeGroupBox.Name = "lblSheetTypeGroupBox";
            this.lblSheetTypeGroupBox.Size = new System.Drawing.Size(174, 35);
            this.lblSheetTypeGroupBox.TabIndex = 12;
            this.lblSheetTypeGroupBox.Text = "Sheet Type";
            this.lblSheetTypeGroupBox.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cmbSheetTypeGroupBox
            // 
            this.cmbSheetTypeGroupBox.FormattingEnabled = true;
            this.cmbSheetTypeGroupBox.Location = new System.Drawing.Point(101, 96);
            this.cmbSheetTypeGroupBox.Name = "cmbSheetTypeGroupBox";
            this.cmbSheetTypeGroupBox.Size = new System.Drawing.Size(174, 21);
            this.cmbSheetTypeGroupBox.TabIndex = 11;
            // 
            // radioYes
            // 
            this.radioYes.AutoSize = true;
            this.radioYes.Location = new System.Drawing.Point(15, 29);
            this.radioYes.Name = "radioYes";
            this.radioYes.Size = new System.Drawing.Size(43, 17);
            this.radioYes.TabIndex = 0;
            this.radioYes.TabStop = true;
            this.radioYes.Text = "Yes";
            this.radioYes.UseVisualStyleBackColor = true;
            this.radioYes.CheckedChanged += new System.EventHandler(this.radioYes_CheckedChanged);
            // 
            // lblQtyGroupBox
            // 
            this.lblQtyGroupBox.Location = new System.Drawing.Point(24, 59);
            this.lblQtyGroupBox.Name = "lblQtyGroupBox";
            this.lblQtyGroupBox.Size = new System.Drawing.Size(65, 35);
            this.lblQtyGroupBox.TabIndex = 10;
            this.lblQtyGroupBox.Text = "Quantity in Sheets";
            this.lblQtyGroupBox.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // txtQtyGroupBox
            // 
            this.txtQtyGroupBox.Location = new System.Drawing.Point(24, 97);
            this.txtQtyGroupBox.Name = "txtQtyGroupBox";
            this.txtQtyGroupBox.Size = new System.Drawing.Size(65, 20);
            this.txtQtyGroupBox.TabIndex = 9;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(212, 522);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "✔ Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(12, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(275, 165);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // frmManageStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 635);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblSheetType);
            this.Controls.Add(this.cmbSheetType);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvBentStock);
            this.Controls.Add(this.dgvUnbentStock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Stock";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnbentStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBentStock)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUnbentStock;
        private System.Windows.Forms.DataGridView dgvBentStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.ComboBox cmbSheetType;
        private System.Windows.Forms.Label lblSheetType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioNo;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.RichTextBox txtReason;
        private System.Windows.Forms.Label lblSheetTypeGroupBox;
        private System.Windows.Forms.ComboBox cmbSheetTypeGroupBox;
        private System.Windows.Forms.RadioButton radioYes;
        private System.Windows.Forms.Label lblQtyGroupBox;
        private System.Windows.Forms.TextBox txtQtyGroupBox;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}