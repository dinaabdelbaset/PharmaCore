namespace PharmaCore.POS.PL
{
    partial class Frm_Buy
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
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxStore = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblItemsCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.DgvBuy = new System.Windows.Forms.DataGridView();
            this.I = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvExpier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPriceZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBouns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxItems = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxSupplier = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.DtpTime = new System.Windows.Forms.DateTimePicker();
            this.label20 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtExpier = new System.Windows.Forms.DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnSupplierbrowse = new System.Windows.Forms.Button();
            this.simpleButton3 = new System.Windows.Forms.Button();
            this.simpleButton2 = new System.Windows.Forms.Button();
            this.simpleButton1 = new System.Windows.Forms.Button();
            this.simpleButton4 = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnItems = new System.Windows.Forms.Button();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.DtpAagel = new System.Windows.Forms.DateTimePicker();
            this.txtbarcode = new DevExpress.XtraEditors.TextEdit();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.rbtnAagel = new System.Windows.Forms.RadioButton();
            this.rbtnCash = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.DgvBuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(899, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(357, 74);
            this.label8.TabIndex = 53;
            this.label8.Text = "إذا تم تغيير اسم المنتج او الصلاحية او سعر البيع \r\nسوف يتم ادراجه كمنتج جديد ببار" +
    "كود جديد\r\nلكن بقية بيانات المنتج سوف يتم نسخها من القديم\r\nفلذلك ادخل منتج قديم م" +
    "شابه ليتم نسخه واضافه الجديد";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(777, 633);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 18);
            this.label11.TabIndex = 84;
            this.label11.Text = "اختر مخزن:";
            // 
            // cbxStore
            // 
            this.cbxStore.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStore.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStore.FormattingEnabled = true;
            this.cbxStore.Location = new System.Drawing.Point(711, 658);
            this.cbxStore.Name = "cbxStore";
            this.cbxStore.Size = new System.Drawing.Size(206, 26);
            this.cbxStore.TabIndex = 83;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(257, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 18);
            this.label15.TabIndex = 81;
            this.label15.Text = "تاريخ الاستحقاق:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(933, 619);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 18);
            this.label14.TabIndex = 78;
            this.label14.Text = "الإجمالي:";
            // 
            // lblItemsCount
            // 
            this.lblItemsCount.AutoSize = true;
            this.lblItemsCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblItemsCount.Location = new System.Drawing.Point(159, 621);
            this.lblItemsCount.Name = "lblItemsCount";
            this.lblItemsCount.Size = new System.Drawing.Size(25, 20);
            this.lblItemsCount.TabIndex = 76;
            this.lblItemsCount.Text = "...";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Location = new System.Drawing.Point(67, 621);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 20);
            this.label13.TabIndex = 75;
            this.label13.Text = "عدد الاصناف";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUsername.ForeColor = System.Drawing.Color.Red;
            this.lblUsername.Location = new System.Drawing.Point(182, 655);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(25, 20);
            this.lblUsername.TabIndex = 74;
            this.lblUsername.Text = "...";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(67, 655);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 20);
            this.label10.TabIndex = 73;
            this.label10.Text = "اسم المستخدم";
            // 
            // DgvBuy
            // 
            this.DgvBuy.AllowUserToAddRows = false;
            this.DgvBuy.AllowUserToDeleteRows = false;
            this.DgvBuy.AllowUserToResizeColumns = false;
            this.DgvBuy.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvBuy.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvBuy.BackgroundColor = System.Drawing.Color.White;
            this.DgvBuy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvBuy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.I,
            this.dgvID,
            this.dgvName,
            this.dgvExpier,
            this.dgvStore,
            this.dgvUnit,
            this.dgvQty,
            this.dgvPrice,
            this.dgvPriceZ,
            this.dgvBouns,
            this.dgvTotal});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvBuy.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvBuy.Location = new System.Drawing.Point(79, 162);
            this.DgvBuy.Name = "DgvBuy";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvBuy.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DgvBuy.RowHeadersVisible = false;
            this.DgvBuy.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvBuy.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvBuy.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvBuy.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvBuy.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvBuy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvBuy.Size = new System.Drawing.Size(1174, 392);
            this.DgvBuy.TabIndex = 72;
            this.DgvBuy.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvBuy_CellValueChanged);
            this.DgvBuy.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgvBuy_DataError);
            // 
            // I
            // 
            this.I.FillWeight = 13F;
            this.I.HeaderText = "NO.";
            this.I.Name = "I";
            this.I.ReadOnly = true;
            // 
            // dgvID
            // 
            this.dgvID.FillWeight = 35F;
            this.dgvID.HeaderText = "رقم المنتج";
            this.dgvID.Name = "dgvID";
            this.dgvID.ReadOnly = true;
            // 
            // dgvName
            // 
            this.dgvName.FillWeight = 70F;
            this.dgvName.HeaderText = "اسم المنتج";
            this.dgvName.Name = "dgvName";
            this.dgvName.ReadOnly = true;
            // 
            // dgvExpier
            // 
            this.dgvExpier.FillWeight = 35F;
            this.dgvExpier.HeaderText = "الصلاحية";
            this.dgvExpier.Name = "dgvExpier";
            // 
            // dgvStore
            // 
            this.dgvStore.FillWeight = 30F;
            this.dgvStore.HeaderText = "الرصيد";
            this.dgvStore.Name = "dgvStore";
            // 
            // dgvUnit
            // 
            this.dgvUnit.FillWeight = 30F;
            this.dgvUnit.HeaderText = "الوحدة";
            this.dgvUnit.Name = "dgvUnit";
            this.dgvUnit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgvQty
            // 
            this.dgvQty.FillWeight = 25F;
            this.dgvQty.HeaderText = "الكمية";
            this.dgvQty.Name = "dgvQty";
            // 
            // dgvPrice
            // 
            this.dgvPrice.FillWeight = 35F;
            this.dgvPrice.HeaderText = "سعر شراء الصيدلي";
            this.dgvPrice.Name = "dgvPrice";
            // 
            // dgvPriceZ
            // 
            this.dgvPriceZ.FillWeight = 35F;
            this.dgvPriceZ.HeaderText = "سعر بيع الجمهور";
            this.dgvPriceZ.Name = "dgvPriceZ";
            // 
            // dgvBouns
            // 
            this.dgvBouns.FillWeight = 25F;
            this.dgvBouns.HeaderText = "البونص";
            this.dgvBouns.Name = "dgvBouns";
            // 
            // dgvTotal
            // 
            this.dgvTotal.FillWeight = 35F;
            this.dgvTotal.HeaderText = "إجمالي الصنف";
            this.dgvTotal.Name = "dgvTotal";
            this.dgvTotal.ReadOnly = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(14, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 20);
            this.label6.TabIndex = 68;
            this.label6.Text = "Delete";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(416, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 18);
            this.label3.TabIndex = 63;
            this.label3.Text = "اختر منتج:";
            // 
            // cbxItems
            // 
            this.cbxItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxItems.FormattingEnabled = true;
            this.cbxItems.Location = new System.Drawing.Point(489, 115);
            this.cbxItems.Name = "cbxItems";
            this.cbxItems.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbxItems.Size = new System.Drawing.Size(253, 26);
            this.cbxItems.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(434, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 18);
            this.label5.TabIndex = 60;
            this.label5.Text = "المورد:";
            // 
            // cbxSupplier
            // 
            this.cbxSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSupplier.FormattingEnabled = true;
            this.cbxSupplier.Location = new System.Drawing.Point(489, 30);
            this.cbxSupplier.Name = "cbxSupplier";
            this.cbxSupplier.Size = new System.Drawing.Size(195, 26);
            this.cbxSupplier.TabIndex = 59;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(76, 118);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 18);
            this.label12.TabIndex = 58;
            this.label12.Text = "رقم الباركود:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(76, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 18);
            this.label16.TabIndex = 55;
            this.label16.Text = "رقم الفاتورة:";
            // 
            // DtpTime
            // 
            this.DtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DtpTime.Location = new System.Drawing.Point(797, 26);
            this.DtpTime.Name = "DtpTime";
            this.DtpTime.Size = new System.Drawing.Size(111, 26);
            this.DtpTime.TabIndex = 85;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(42, 537);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(34, 20);
            this.label20.TabIndex = 146;
            this.label20.Text = "F11";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(1007, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 149;
            this.label2.Text = "لفتح شاشة البحث F1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(50, 118);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(26, 20);
            this.label17.TabIndex = 148;
            this.label17.Text = "F3";
            // 
            // txtExpier
            // 
            this.txtExpier.CustomFormat = "MM/yyyy";
            this.txtExpier.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtExpier.Location = new System.Drawing.Point(298, 600);
            this.txtExpier.Name = "txtExpier";
            this.txtExpier.ShowUpDown = true;
            this.txtExpier.Size = new System.Drawing.Size(145, 26);
            this.txtExpier.TabIndex = 150;
            this.txtExpier.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(777, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(157, 50);
            this.pictureBox1.TabIndex = 86;
            this.pictureBox1.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(710, 588);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(34, 20);
            this.label21.TabIndex = 188;
            this.label21.Text = "F12";
            // 
            // btnSupplierbrowse
            // 
            this.btnSupplierbrowse.Location = new System.Drawing.Point(710, 24);
            this.btnSupplierbrowse.Name = "btnSupplierbrowse";
            this.btnSupplierbrowse.Size = new System.Drawing.Size(162, 38);
            this.btnSupplierbrowse.TabIndex = 286;
            this.btnSupplierbrowse.Text = "إضافة مورد جديد";
            this.btnSupplierbrowse.UseVisualStyleBackColor = true;
            this.btnSupplierbrowse.Click += new System.EventHandler(this.btnSupplierbrowse_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(532, 67);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(152, 38);
            this.simpleButton3.TabIndex = 287;
            this.simpleButton3.Text = "إضافة منتج جديد";
            this.simpleButton3.UseVisualStyleBackColor = true;
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(89, 560);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(183, 38);
            this.simpleButton2.TabIndex = 288;
            this.simpleButton2.Text = "تعديل بيانات المنتج";
            this.simpleButton2.UseVisualStyleBackColor = true;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(496, 570);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(183, 38);
            this.simpleButton1.TabIndex = 289;
            this.simpleButton1.Text = "حفظ و طباعة الفاتورة";
            this.simpleButton1.UseVisualStyleBackColor = true;
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(496, 623);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(183, 38);
            this.simpleButton4.TabIndex = 290;
            this.simpleButton4.Text = "طباعة الفاتورة السابقة";
            this.simpleButton4.UseVisualStyleBackColor = true;
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(12, 300);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(86, 38);
            this.btnDeleteItem.TabIndex = 291;
            this.btnDeleteItem.Text = "Delete";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // btnItems
            // 
            this.btnItems.Location = new System.Drawing.Point(748, 77);
            this.btnItems.Name = "btnItems";
            this.btnItems.Size = new System.Drawing.Size(86, 38);
            this.btnItems.TabIndex = 292;
            this.btnItems.Text = "F2";
            this.btnItems.UseVisualStyleBackColor = true;
            this.btnItems.Click += new System.EventHandler(this.btnItems_Click);
            // 
            // DtpDate
            // 
            this.DtpDate.CustomFormat = "yyyy-MM-dd";
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDate.Location = new System.Drawing.Point(831, 115);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.RightToLeftLayout = true;
            this.DtpDate.Size = new System.Drawing.Size(138, 26);
            this.DtpDate.TabIndex = 293;
            this.DtpDate.Visible = false;
            // 
            // DtpAagel
            // 
            this.DtpAagel.CustomFormat = "yyyy-MM-dd";
            this.DtpAagel.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpAagel.Location = new System.Drawing.Point(377, 73);
            this.DtpAagel.Name = "DtpAagel";
            this.DtpAagel.RightToLeftLayout = true;
            this.DtpAagel.Size = new System.Drawing.Size(138, 26);
            this.DtpAagel.TabIndex = 294;
            this.DtpAagel.Visible = false;
            // 
            // txtbarcode
            // 
            this.txtbarcode.EditValue = "";
            this.txtbarcode.Location = new System.Drawing.Point(171, 115);
            this.txtbarcode.Name = "txtbarcode";
            this.txtbarcode.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtbarcode.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtbarcode.Properties.Appearance.Options.UseBorderColor = true;
            this.txtbarcode.Properties.Appearance.Options.UseFont = true;
            this.txtbarcode.Properties.Appearance.Options.UseTextOptions = true;
            this.txtbarcode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtbarcode.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtbarcode.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Silver;
            this.txtbarcode.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtbarcode.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtbarcode.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtbarcode.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtbarcode.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtbarcode.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtbarcode.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtbarcode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtbarcode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtbarcode.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtbarcode.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtbarcode.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtbarcode.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtbarcode.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtbarcode.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtbarcode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Silver;
            this.txtbarcode.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtbarcode.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtbarcode.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtbarcode.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtbarcode.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtbarcode.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtbarcode.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtbarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtbarcode.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtbarcode.Properties.Name = "textEdit1";
            this.txtbarcode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtbarcode.Size = new System.Drawing.Size(220, 24);
            this.txtbarcode.TabIndex = 295;
            this.txtbarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbarcode_KeyPress);
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(171, 30);
            this.txtID.Name = "txtID";
            this.txtID.Properties.Appearance.BorderColor = System.Drawing.Color.Gray;
            this.txtID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.Appearance.Options.UseBorderColor = true;
            this.txtID.Properties.Appearance.Options.UseFont = true;
            this.txtID.Properties.Appearance.Options.UseTextOptions = true;
            this.txtID.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtID.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightGray;
            this.txtID.Properties.AppearanceDisabled.BorderColor = System.Drawing.Color.Black;
            this.txtID.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtID.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceDisabled.Options.UseFont = true;
            this.txtID.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtID.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtID.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Cyan;
            this.txtID.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.Teal;
            this.txtID.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtID.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceFocused.Options.UseFont = true;
            this.txtID.Properties.AppearanceFocused.Options.UseTextOptions = true;
            this.txtID.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightGray;
            this.txtID.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Black;
            this.txtID.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtID.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtID.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtID.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.txtID.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.txtID.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtID.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.txtID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtID.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            this.txtID.Properties.Name = "textEdit1";
            this.txtID.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtID.Size = new System.Drawing.Size(157, 24);
            this.txtID.TabIndex = 296;
            // 
            // txtTotal
            // 
            this.txtTotal.EditValue = "";
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(1063, 607);
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
            this.txtTotal.Size = new System.Drawing.Size(161, 36);
            this.txtTotal.TabIndex = 297;
            // 
            // rbtnAagel
            // 
            this.rbtnAagel.AutoSize = true;
            this.rbtnAagel.Location = new System.Drawing.Point(159, 75);
            this.rbtnAagel.Name = "rbtnAagel";
            this.rbtnAagel.Size = new System.Drawing.Size(78, 22);
            this.rbtnAagel.TabIndex = 298;
            this.rbtnAagel.TabStop = true;
            this.rbtnAagel.Text = "دفع آجل";
            this.rbtnAagel.UseVisualStyleBackColor = true;
            // 
            // rbtnCash
            // 
            this.rbtnCash.AutoSize = true;
            this.rbtnCash.Location = new System.Drawing.Point(23, 75);
            this.rbtnCash.Name = "rbtnCash";
            this.rbtnCash.Size = new System.Drawing.Size(130, 22);
            this.rbtnCash.TabIndex = 299;
            this.rbtnCash.TabStop = true;
            this.rbtnCash.Text = "دفع الفاتورة كاش";
            this.rbtnCash.UseVisualStyleBackColor = true;
            // 
            // Frm_Buy
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1270, 696);
            this.Controls.Add(this.rbtnCash);
            this.Controls.Add(this.rbtnAagel);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtbarcode);
            this.Controls.Add(this.DtpAagel);
            this.Controls.Add(this.DtpDate);
            this.Controls.Add(this.btnItems);
            this.Controls.Add(this.btnDeleteItem);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.btnSupplierbrowse);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtExpier);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DtpTime);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbxStore);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblItemsCount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.DgvBuy);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxItems);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxSupplier);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label16);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_Buy";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "كاشير الشراء";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Buy_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_Buy_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DgvBuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtbarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxStore;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblItemsCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.DataGridView DgvBuy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxItems;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cbxSupplier;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DateTimePicker DtpTime;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridViewTextBoxColumn I;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvExpier;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStore;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPriceZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBouns;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTotal;
        private System.Windows.Forms.DateTimePicker txtExpier;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnSupplierbrowse;
        private System.Windows.Forms.Button simpleButton3;
        private System.Windows.Forms.Button simpleButton2;
        private System.Windows.Forms.Button simpleButton1;
        private System.Windows.Forms.Button simpleButton4;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnItems;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private System.Windows.Forms.DateTimePicker DtpAagel;
        private DevExpress.XtraEditors.TextEdit txtbarcode;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private System.Windows.Forms.RadioButton rbtnAagel;
        private System.Windows.Forms.RadioButton rbtnCash;
    }
}