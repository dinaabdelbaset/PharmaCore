namespace PharmaCore.POS.PL
{
    partial class Frm_StoretransfireReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxStoreTo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxStoreFrom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtnOneStoreFrom = new System.Windows.Forms.RadioButton();
            this.rbtnAllStoreFrom = new System.Windows.Forms.RadioButton();
            this.DtpFrom = new System.Windows.Forms.DateTimePicker();
            this.DtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtTotalQty = new DevExpress.XtraEditors.TextEdit();
            this.rbtnAllStoreTo = new System.Windows.Forms.RadioButton();
            this.rbtnOneStoreTo = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalQty.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(821, 478);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 18);
            this.label4.TabIndex = 44;
            this.label4.Text = "اجمالي الكميات المحولة:";
            // 
            // DgvSearch
            // 
            this.DgvSearch.AllowUserToAddRows = false;
            this.DgvSearch.AllowUserToDeleteRows = false;
            this.DgvSearch.AllowUserToResizeColumns = false;
            this.DgvSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvSearch.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvSearch.BackgroundColor = System.Drawing.Color.White;
            this.DgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle5;
            this.DgvSearch.Location = new System.Drawing.Point(30, 178);
            this.DgvSearch.Name = "DgvSearch";
            this.DgvSearch.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DgvSearch.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvSearch.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvSearch.Size = new System.Drawing.Size(1119, 281);
            this.DgvSearch.TabIndex = 42;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnOneStoreTo);
            this.groupBox2.Controls.Add(this.rbtnAllStoreTo);
            this.groupBox2.Controls.Add(this.cbxStoreTo);
            this.groupBox2.Location = new System.Drawing.Point(401, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 80);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "المخازن المحول لها";
            // 
            // cbxStoreTo
            // 
            this.cbxStoreTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStoreTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStoreTo.FormattingEnabled = true;
            this.cbxStoreTo.Location = new System.Drawing.Point(6, 31);
            this.cbxStoreTo.Name = "cbxStoreTo";
            this.cbxStoreTo.Size = new System.Drawing.Size(152, 26);
            this.cbxStoreTo.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxStoreFrom);
            this.groupBox1.Controls.Add(this.rbtnAllStoreFrom);
            this.groupBox1.Controls.Add(this.rbtnOneStoreFrom);
            this.groupBox1.Location = new System.Drawing.Point(10, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 80);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "المخازن المحول منها";
            // 
            // cbxStoreFrom
            // 
            this.cbxStoreFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStoreFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStoreFrom.FormattingEnabled = true;
            this.cbxStoreFrom.Location = new System.Drawing.Point(6, 31);
            this.cbxStoreFrom.Name = "cbxStoreFrom";
            this.cbxStoreFrom.Size = new System.Drawing.Size(152, 26);
            this.cbxStoreFrom.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(515, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 23);
            this.label2.TabIndex = 37;
            this.label2.Text = "اجمالي المنتجات المحولة";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.DtpFrom);
            this.groupBox3.Controls.Add(this.DtpTo);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(792, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(381, 73);
            this.groupBox3.TabIndex = 174;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(156, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "الى:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(346, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "من:";
            // 
            // rbtnOneStoreFrom
            // 
            this.rbtnOneStoreFrom.AutoSize = true;
            this.rbtnOneStoreFrom.Location = new System.Drawing.Point(180, 32);
            this.rbtnOneStoreFrom.Name = "rbtnOneStoreFrom";
            this.rbtnOneStoreFrom.Size = new System.Drawing.Size(98, 22);
            this.rbtnOneStoreFrom.TabIndex = 283;
            this.rbtnOneStoreFrom.Text = "مخزن محدد";
            this.rbtnOneStoreFrom.UseVisualStyleBackColor = true;
            // 
            // rbtnAllStoreFrom
            // 
            this.rbtnAllStoreFrom.AutoSize = true;
            this.rbtnAllStoreFrom.Checked = true;
            this.rbtnAllStoreFrom.Location = new System.Drawing.Point(284, 32);
            this.rbtnAllStoreFrom.Name = "rbtnAllStoreFrom";
            this.rbtnAllStoreFrom.Size = new System.Drawing.Size(95, 22);
            this.rbtnAllStoreFrom.TabIndex = 284;
            this.rbtnAllStoreFrom.TabStop = true;
            this.rbtnAllStoreFrom.Text = "كل المخازن";
            this.rbtnAllStoreFrom.UseVisualStyleBackColor = true;
            // 
            // DtpFrom
            // 
            this.DtpFrom.CustomFormat = "yyyy-MM-dd";
            this.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpFrom.Location = new System.Drawing.Point(214, 29);
            this.DtpFrom.Name = "DtpFrom";
            this.DtpFrom.RightToLeftLayout = true;
            this.DtpFrom.Size = new System.Drawing.Size(126, 26);
            this.DtpFrom.TabIndex = 286;
            this.DtpFrom.Visible = false;
            // 
            // DtpTo
            // 
            this.DtpTo.CustomFormat = "yyyy-MM-dd";
            this.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpTo.Location = new System.Drawing.Point(24, 24);
            this.DtpTo.Name = "DtpTo";
            this.DtpTo.RightToLeftLayout = true;
            this.DtpTo.Size = new System.Drawing.Size(116, 26);
            this.DtpTo.TabIndex = 287;
            this.DtpTo.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(824, 40);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(114, 38);
            this.btnSearch.TabIndex = 288;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Location = new System.Drawing.Point(237, 468);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(220, 38);
            this.btnDeleteAll.TabIndex = 289;
            this.btnDeleteAll.Text = "مسح كل ما بين فترتي التاريخ ";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(60, 468);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(124, 38);
            this.btnDelete.TabIndex = 290;
            this.btnDelete.Text = "مسح البيانات";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.EditValue = "";
            this.txtTotalQty.Enabled = false;
            this.txtTotalQty.Location = new System.Drawing.Point(984, 466);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalQty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalQty.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotalQty.Properties.Appearance.Options.UseFont = true;
            this.txtTotalQty.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalQty.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalQty.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalQty.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotalQty.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalQty.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalQty.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotalQty.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotalQty.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalQty.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalQty.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalQty.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotalQty.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotalQty.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalQty.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotalQty.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotalQty.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotalQty.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotalQty.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalQty.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalQty.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotalQty.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalQty.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotalQty.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotalQty.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotalQty.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotalQty.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalQty.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalQty.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotalQty.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotalQty.Properties.Name = "textEdit1";
            this.txtTotalQty.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotalQty.Size = new System.Drawing.Size(161, 36);
            this.txtTotalQty.TabIndex = 291;
            // 
            // rbtnAllStoreTo
            // 
            this.rbtnAllStoreTo.AutoSize = true;
            this.rbtnAllStoreTo.Checked = true;
            this.rbtnAllStoreTo.Location = new System.Drawing.Point(279, 35);
            this.rbtnAllStoreTo.Name = "rbtnAllStoreTo";
            this.rbtnAllStoreTo.Size = new System.Drawing.Size(95, 22);
            this.rbtnAllStoreTo.TabIndex = 293;
            this.rbtnAllStoreTo.TabStop = true;
            this.rbtnAllStoreTo.Text = "كل المخازن";
            this.rbtnAllStoreTo.UseVisualStyleBackColor = true;
            // 
            // rbtnOneStoreTo
            // 
            this.rbtnOneStoreTo.AutoSize = true;
            this.rbtnOneStoreTo.Location = new System.Drawing.Point(169, 35);
            this.rbtnOneStoreTo.Name = "rbtnOneStoreTo";
            this.rbtnOneStoreTo.Size = new System.Drawing.Size(98, 22);
            this.rbtnOneStoreTo.TabIndex = 292;
            this.rbtnOneStoreTo.Text = "مخزن محدد";
            this.rbtnOneStoreTo.UseVisualStyleBackColor = true;
            // 
            // Frm_StoretransfireReport
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1185, 527);
            this.Controls.Add(this.txtTotalQty);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_StoretransfireReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقرير تحويل المنتجات بين المخازن";
            this.Load += new System.EventHandler(this.Frm_StoretransfireReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalQty.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxStoreTo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxStoreFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtnOneStoreFrom;
        private System.Windows.Forms.RadioButton rbtnAllStoreFrom;
        private System.Windows.Forms.DateTimePicker DtpFrom;
        private System.Windows.Forms.DateTimePicker DtpTo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnDelete;
        private DevExpress.XtraEditors.TextEdit txtTotalQty;
        private System.Windows.Forms.RadioButton rbtnAllStoreTo;
        private System.Windows.Forms.RadioButton rbtnOneStoreTo;
    }
}