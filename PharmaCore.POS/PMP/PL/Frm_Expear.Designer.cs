namespace PharmaCore.POS.PL
{
    partial class Frm_Expear
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.cbxDate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.search = new System.Windows.Forms.Button();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalQty = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalBuy = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalSale = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBuy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSale.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(279, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 19);
            this.label9.TabIndex = 81;
            this.label9.Text = "تنتهي صلاحيته بعد :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(796, 477);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 18);
            this.label6.TabIndex = 77;
            this.label6.Text = "إجمالي البيع:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(514, 475);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 18);
            this.label5.TabIndex = 75;
            this.label5.Text = "إجمالي الشراء :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(278, 475);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 18);
            this.label4.TabIndex = 73;
            this.label4.Text = "إجمالي الكمية:";
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvSearch.Location = new System.Drawing.Point(38, 84);
            this.DgvSearch.Name = "DgvSearch";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvSearch.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvSearch.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvSearch.Size = new System.Drawing.Size(1005, 366);
            this.DgvSearch.TabIndex = 72;
            // 
            // cbxDate
            // 
            this.cbxDate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxDate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxDate.FormattingEnabled = true;
            this.cbxDate.Items.AddRange(new object[] {
            "تم الإنتهاء",
            "شهر",
            "شهرين",
            "ثلاثة شهور",
            "اربعة شهور",
            "خمس شهور ",
            "ستة شهور ",
            "سنة"});
            this.cbxDate.Location = new System.Drawing.Point(444, 27);
            this.cbxDate.Name = "cbxDate";
            this.cbxDate.Size = new System.Drawing.Size(188, 26);
            this.cbxDate.TabIndex = 84;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(35, 474);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 85;
            this.label1.Text = "إجمالي الأصناف :";
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(660, 20);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(113, 38);
            this.search.TabIndex = 277;
            this.search.Text = "بحث";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(157, 465);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotal.Properties.Appearance.Options.UseFont = true;
            this.txtTotal.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotal.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotal.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotal.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotal.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotal.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotal.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotal.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotal.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotal.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotal.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotal.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotal.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotal.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotal.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotal.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotal.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotal.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotal.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotal.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotal.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotal.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotal.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotal.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotal.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotal.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotal.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotal.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotal.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotal.Properties.Name = "textEdit1";
            this.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotal.Size = new System.Drawing.Size(111, 36);
            this.txtTotal.TabIndex = 278;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.EditValue = "";
            this.txtTotalQty.Enabled = false;
            this.txtTotalQty.Location = new System.Drawing.Point(387, 465);
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
            this.txtTotalQty.Size = new System.Drawing.Size(111, 36);
            this.txtTotalQty.TabIndex = 279;
            // 
            // txtTotalBuy
            // 
            this.txtTotalBuy.EditValue = "";
            this.txtTotalBuy.Enabled = false;
            this.txtTotalBuy.Location = new System.Drawing.Point(629, 465);
            this.txtTotalBuy.Name = "txtTotalBuy";
            this.txtTotalBuy.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalBuy.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBuy.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotalBuy.Properties.Appearance.Options.UseFont = true;
            this.txtTotalBuy.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalBuy.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalBuy.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalBuy.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalBuy.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotalBuy.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalBuy.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalBuy.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotalBuy.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotalBuy.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalBuy.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalBuy.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalBuy.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotalBuy.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotalBuy.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalBuy.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotalBuy.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotalBuy.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotalBuy.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotalBuy.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalBuy.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalBuy.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotalBuy.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalBuy.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotalBuy.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotalBuy.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotalBuy.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotalBuy.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalBuy.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalBuy.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotalBuy.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotalBuy.Properties.Name = "textEdit1";
            this.txtTotalBuy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotalBuy.Size = new System.Drawing.Size(150, 36);
            this.txtTotalBuy.TabIndex = 280;
            // 
            // txtTotalSale
            // 
            this.txtTotalSale.EditValue = "";
            this.txtTotalSale.Enabled = false;
            this.txtTotalSale.Location = new System.Drawing.Point(893, 465);
            this.txtTotalSale.Name = "txtTotalSale";
            this.txtTotalSale.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtTotalSale.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalSale.Properties.Appearance.Options.UseBorderColor = true;
            this.txtTotalSale.Properties.Appearance.Options.UseFont = true;
            this.txtTotalSale.Properties.Appearance.Options.UseTextOptions = true;
            this.txtTotalSale.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalSale.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalSale.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalSale.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtTotalSale.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalSale.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalSale.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtTotalSale.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtTotalSale.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalSale.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalSale.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalSale.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtTotalSale.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtTotalSale.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalSale.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtTotalSale.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtTotalSale.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtTotalSale.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtTotalSale.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalSale.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtTotalSale.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtTotalSale.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtTotalSale.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtTotalSale.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtTotalSale.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtTotalSale.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtTotalSale.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtTotalSale.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtTotalSale.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTotalSale.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtTotalSale.Properties.Name = "textEdit1";
            this.txtTotalSale.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTotalSale.Size = new System.Drawing.Size(150, 36);
            this.txtTotalSale.TabIndex = 281;
            // 
            // Frm_Expear
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1071, 545);
            this.Controls.Add(this.txtTotalSale);
            this.Controls.Add(this.txtTotalBuy);
            this.Controls.Add(this.txtTotalQty);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.search);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxDate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DgvSearch);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_Expear";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تتبع تواريخ الإنتهاء";
            this.Load += new System.EventHandler(this.Frm_Expear_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBuy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSale.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.ComboBox cbxDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button search;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.TextEdit txtTotalQty;
        private DevExpress.XtraEditors.TextEdit txtTotalBuy;
        private DevExpress.XtraEditors.TextEdit txtTotalSale;
    }
}