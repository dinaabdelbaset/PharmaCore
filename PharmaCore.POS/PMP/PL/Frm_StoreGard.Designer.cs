namespace PharmaCore.POS.PL
{
    partial class Frm_StoreGard
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxStore = new System.Windows.Forms.ComboBox();
            this.rbtnOneStore = new System.Windows.Forms.RadioButton();
            this.rbtnAllStore = new System.Windows.Forms.RadioButton();
            this.txtBarcode = new DevExpress.XtraEditors.TextEdit();
            this.txtRb7h = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalBuy = new DevExpress.XtraEditors.TextEdit();
            this.txtTotalSale = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRb7h.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBuy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSale.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(741, 456);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 18);
            this.label3.TabIndex = 53;
            this.label3.Text = "مبلغ البيع :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(385, 456);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 18);
            this.label2.TabIndex = 51;
            this.label2.Text = "مبلغ الشراء :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 454);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 49;
            this.label1.Text = "الارباح المتوقعة:";
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
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvSearch.Location = new System.Drawing.Point(19, 129);
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
            this.DgvSearch.Size = new System.Drawing.Size(982, 298);
            this.DgvSearch.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(667, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 18);
            this.label4.TabIndex = 47;
            this.label4.Text = "بحث بالباركود:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnAllStore);
            this.groupBox1.Controls.Add(this.cbxStore);
            this.groupBox1.Controls.Add(this.rbtnOneStore);
            this.groupBox1.Location = new System.Drawing.Point(19, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 80);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            // 
            // cbxStore
            // 
            this.cbxStore.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStore.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStore.FormattingEnabled = true;
            this.cbxStore.Location = new System.Drawing.Point(9, 34);
            this.cbxStore.Name = "cbxStore";
            this.cbxStore.Size = new System.Drawing.Size(187, 26);
            this.cbxStore.TabIndex = 9;
            // 
            // rbtnOneStore
            // 
            this.rbtnOneStore.AutoSize = true;
            this.rbtnOneStore.Location = new System.Drawing.Point(214, 38);
            this.rbtnOneStore.Name = "rbtnOneStore";
            this.rbtnOneStore.Size = new System.Drawing.Size(98, 22);
            this.rbtnOneStore.TabIndex = 282;
            this.rbtnOneStore.Text = "مخزن محدد";
            this.rbtnOneStore.UseVisualStyleBackColor = true;
            // 
            // rbtnAllStore
            // 
            this.rbtnAllStore.AutoSize = true;
            this.rbtnAllStore.Checked = true;
            this.rbtnAllStore.Location = new System.Drawing.Point(318, 38);
            this.rbtnAllStore.Name = "rbtnAllStore";
            this.rbtnAllStore.Size = new System.Drawing.Size(95, 22);
            this.rbtnAllStore.TabIndex = 283;
            this.rbtnAllStore.TabStop = true;
            this.rbtnAllStore.Text = "كل المخازن";
            this.rbtnAllStore.UseVisualStyleBackColor = true;
            // 
            // txtBarcode
            // 
            this.txtBarcode.EditValue = "";
            this.txtBarcode.Location = new System.Drawing.Point(782, 84);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtBarcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBarcode.Properties.Appearance.Options.UseBorderColor = true;
            this.txtBarcode.Properties.Appearance.Options.UseFont = true;
            this.txtBarcode.Properties.Appearance.Options.UseTextOptions = true;
            this.txtBarcode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtBarcode.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBarcode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtBarcode.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtBarcode.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBarcode.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtBarcode.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtBarcode.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtBarcode.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtBarcode.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBarcode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtBarcode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtBarcode.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBarcode.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtBarcode.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtBarcode.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtBarcode.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtBarcode.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBarcode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtBarcode.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtBarcode.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtBarcode.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtBarcode.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtBarcode.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtBarcode.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtBarcode.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtBarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBarcode.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtBarcode.Properties.Name = "textEdit1";
            this.txtBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtBarcode.Size = new System.Drawing.Size(187, 24);
            this.txtBarcode.TabIndex = 290;
            // 
            // txtRb7h
            // 
            this.txtRb7h.EditValue = "";
            this.txtRb7h.Enabled = false;
            this.txtRb7h.Location = new System.Drawing.Point(132, 444);
            this.txtRb7h.Name = "txtRb7h";
            this.txtRb7h.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtRb7h.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRb7h.Properties.Appearance.Options.UseBorderColor = true;
            this.txtRb7h.Properties.Appearance.Options.UseFont = true;
            this.txtRb7h.Properties.Appearance.Options.UseTextOptions = true;
            this.txtRb7h.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtRb7h.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtRb7h.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtRb7h.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtRb7h.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtRb7h.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtRb7h.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtRb7h.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtRb7h.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtRb7h.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtRb7h.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtRb7h.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtRb7h.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtRb7h.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtRb7h.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtRb7h.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtRb7h.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtRb7h.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtRb7h.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtRb7h.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtRb7h.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtRb7h.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtRb7h.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtRb7h.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtRb7h.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtRb7h.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtRb7h.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtRb7h.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtRb7h.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtRb7h.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtRb7h.Properties.Name = "textEdit1";
            this.txtRb7h.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtRb7h.Size = new System.Drawing.Size(161, 36);
            this.txtRb7h.TabIndex = 291;
            // 
            // txtTotalBuy
            // 
            this.txtTotalBuy.EditValue = "";
            this.txtTotalBuy.Enabled = false;
            this.txtTotalBuy.Location = new System.Drawing.Point(481, 444);
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
            this.txtTotalBuy.Size = new System.Drawing.Size(161, 36);
            this.txtTotalBuy.TabIndex = 292;
            // 
            // txtTotalSale
            // 
            this.txtTotalSale.EditValue = "";
            this.txtTotalSale.Enabled = false;
            this.txtTotalSale.Location = new System.Drawing.Point(824, 442);
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
            this.txtTotalSale.Size = new System.Drawing.Size(161, 36);
            this.txtTotalSale.TabIndex = 293;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(485, 23);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(124, 38);
            this.btnSearch.TabIndex = 294;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Frm_StoreGard
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1015, 496);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtTotalSale);
            this.Controls.Add(this.txtTotalBuy);
            this.Controls.Add(this.txtRb7h);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DgvSearch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_StoreGard";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "جرد المخازن";
            this.Load += new System.EventHandler(this.Frm_StoreGard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRb7h.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalBuy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalSale.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxStore;
        private System.Windows.Forms.RadioButton rbtnAllStore;
        private System.Windows.Forms.RadioButton rbtnOneStore;
        private DevExpress.XtraEditors.TextEdit txtBarcode;
        private DevExpress.XtraEditors.TextEdit txtRb7h;
        private DevExpress.XtraEditors.TextEdit txtTotalBuy;
        private DevExpress.XtraEditors.TextEdit txtTotalSale;
        private System.Windows.Forms.Button btnSearch;
    }
}