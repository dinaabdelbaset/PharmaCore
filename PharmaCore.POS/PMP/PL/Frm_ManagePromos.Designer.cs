namespace PharmaCore.POS.PL
{
    partial class Frm_ManagePromos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            _tabs = new TabControl();
            _tabPromos = new TabPage();
            splitPromos = new SplitContainer();
            _lstPromos = new ListBox();
            pnlRightPromo = new Panel();
            pnlPromoGrid = new Panel();
            _dgvRules = new DataGridView();
            colCondition = new DataGridViewComboBoxColumn();
            colCondValName = new DataGridViewTextBoxColumn();
            colCondValId = new DataGridViewTextBoxColumn();
            colPickCond = new DataGridViewButtonColumn();
            colBuyQty = new DataGridViewTextBoxColumn();
            colReward = new DataGridViewComboBoxColumn();
            colRewardQtyOrVal = new DataGridViewTextBoxColumn();
            colRewardProdName = new DataGridViewTextBoxColumn();
            colRewardProdId = new DataGridViewTextBoxColumn();
            colPickReward = new DataGridViewButtonColumn();
            pnlPromoBot = new Panel();
            _btnNewPromo = new Button();
            _btnSavePromo = new Button();
            pnlPromoTop = new TableLayoutPanel();
            label1 = new Label();
            _txtPromoName = new TextBox();
            label2 = new Label();
            _cmbPromoType = new ComboBox();
            label3 = new Label();
            _dtpStart = new DateTimePicker();
            label4 = new Label();
            _dtpEnd = new DateTimePicker();
            label5 = new Label();
            _txtPromoDesc = new TextBox();
            _chkActive = new CheckBox();
            _tabCollections = new TabPage();
            splitCols = new SplitContainer();
            _lstCollections = new ListBox();
            pnlRightCol = new Panel();
            pnlColGrid = new Panel();
            _dgvColItems = new DataGridView();
            colItemId = new DataGridViewTextBoxColumn();
            colItemCode = new DataGridViewTextBoxColumn();
            colItemName = new DataGridViewTextBoxColumn();
            colItemDel = new DataGridViewButtonColumn();
            pnlColBot = new Panel();
            _btnNewCol = new Button();
            _btnSaveCol = new Button();
            btnAddItems = new Button();
            pnlColTop = new FlowLayoutPanel();
            label6 = new Label();
            _txtColName = new TextBox();
            _tabs.SuspendLayout();
            _tabPromos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitPromos).BeginInit();
            splitPromos.Panel1.SuspendLayout();
            splitPromos.Panel2.SuspendLayout();
            splitPromos.SuspendLayout();
            pnlRightPromo.SuspendLayout();
            pnlPromoGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvRules).BeginInit();
            pnlPromoBot.SuspendLayout();
            pnlPromoTop.SuspendLayout();
            _tabCollections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitCols).BeginInit();
            splitCols.Panel1.SuspendLayout();
            splitCols.Panel2.SuspendLayout();
            splitCols.SuspendLayout();
            pnlRightCol.SuspendLayout();
            pnlColGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvColItems).BeginInit();
            pnlColBot.SuspendLayout();
            pnlColTop.SuspendLayout();
            SuspendLayout();
            // 
            // _tabs
            // 
            _tabs.Controls.Add(_tabPromos);
            _tabs.Controls.Add(_tabCollections);
            _tabs.Dock = DockStyle.Fill;
            _tabs.Font = new Font("Tahoma", 11F);
            _tabs.Location = new Point(0, 0);
            _tabs.Name = "_tabs";
            _tabs.RightToLeftLayout = true;
            _tabs.SelectedIndex = 0;
            _tabs.Size = new Size(1209, 800);
            _tabs.TabIndex = 0;
            // 
            // _tabPromos
            // 
            _tabPromos.Controls.Add(splitPromos);
            _tabPromos.Location = new Point(4, 27);
            _tabPromos.Name = "_tabPromos";
            _tabPromos.Padding = new Padding(3);
            _tabPromos.Size = new Size(1201, 769);
            _tabPromos.TabIndex = 0;
            _tabPromos.Text = "إدارة العروض";
            _tabPromos.UseVisualStyleBackColor = true;
            // 
            // splitPromos
            // 
            splitPromos.Dock = DockStyle.Fill;
            splitPromos.Location = new Point(3, 3);
            splitPromos.Name = "splitPromos";
            // 
            // splitPromos.Panel1
            // 
            splitPromos.Panel1.Controls.Add(_lstPromos);
            splitPromos.Panel1.RightToLeft = RightToLeft.Yes;
            // 
            // splitPromos.Panel2
            // 
            splitPromos.Panel2.Controls.Add(pnlRightPromo);
            splitPromos.Panel2.RightToLeft = RightToLeft.Yes;
            splitPromos.Size = new Size(1195, 763);
            splitPromos.SplitterDistance = 302;
            splitPromos.TabIndex = 0;
            // 
            // _lstPromos
            // 
            _lstPromos.Dock = DockStyle.Fill;
            _lstPromos.Font = new Font("Tahoma", 12F);
            _lstPromos.FormattingEnabled = true;
            _lstPromos.ItemHeight = 19;
            _lstPromos.Location = new Point(0, 0);
            _lstPromos.Name = "_lstPromos";
            _lstPromos.Size = new Size(302, 763);
            _lstPromos.TabIndex = 0;
            // 
            // pnlRightPromo
            // 
            pnlRightPromo.Controls.Add(pnlPromoGrid);
            pnlRightPromo.Controls.Add(pnlPromoBot);
            pnlRightPromo.Controls.Add(pnlPromoTop);
            pnlRightPromo.Dock = DockStyle.Fill;
            pnlRightPromo.Location = new Point(0, 0);
            pnlRightPromo.Name = "pnlRightPromo";
            pnlRightPromo.Padding = new Padding(10);
            pnlRightPromo.Size = new Size(889, 763);
            pnlRightPromo.TabIndex = 0;
            // 
            // pnlPromoGrid
            // 
            pnlPromoGrid.Controls.Add(_dgvRules);
            pnlPromoGrid.Dock = DockStyle.Fill;
            pnlPromoGrid.Location = new Point(10, 160);
            pnlPromoGrid.Name = "pnlPromoGrid";
            pnlPromoGrid.Padding = new Padding(0, 10, 0, 10);
            pnlPromoGrid.Size = new Size(869, 543);
            pnlPromoGrid.TabIndex = 1;
            // 
            // _dgvRules
            // 
            _dgvRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvRules.BackgroundColor = Color.White;
            _dgvRules.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvRules.Columns.AddRange(new DataGridViewColumn[] { colCondition, colCondValName, colCondValId, colPickCond, colBuyQty, colReward, colRewardQtyOrVal, colRewardProdName, colRewardProdId, colPickReward });
            _dgvRules.Dock = DockStyle.Fill;
            _dgvRules.Location = new Point(0, 10);
            _dgvRules.Name = "_dgvRules";
            _dgvRules.Size = new Size(869, 523);
            _dgvRules.TabIndex = 0;
            // 
            // colCondition
            // 
            colCondition.HeaderText = "شروط العرض";
            colCondition.Items.AddRange(new object[] { "منتج محدد", "قسم محدد", "براند محدد", "أي منتج", "مجموعة مخصصة" });
            colCondition.Name = "colCondition";
            // 
            // colCondValName
            // 
            colCondValName.HeaderText = "قيمة الشرط";
            colCondValName.Name = "colCondValName";
            colCondValName.ReadOnly = true;
            // 
            // colCondValId
            // 
            colCondValId.HeaderText = "colCondValId";
            colCondValId.Name = "colCondValId";
            colCondValId.Visible = false;
            // 
            // colPickCond
            // 
            colPickCond.HeaderText = "اختيار..";
            colPickCond.Name = "colPickCond";
            colPickCond.Text = "بحث";
            colPickCond.UseColumnTextForButtonValue = true;
            // 
            // colBuyQty
            // 
            colBuyQty.HeaderText = "اذا اشترى عدد";
            colBuyQty.Name = "colBuyQty";
            // 
            // colReward
            // 
            colReward.HeaderText = "نوع المكافأة";
            colReward.Items.AddRange(new object[] { "بونص مجاني", "خصم نسبة", "خصم مبلغ ثابت" });
            colReward.Name = "colReward";
            // 
            // colRewardQtyOrVal
            // 
            colRewardQtyOrVal.HeaderText = "مقدار الخصم/البونص";
            colRewardQtyOrVal.Name = "colRewardQtyOrVal";
            // 
            // colRewardProdName
            // 
            colRewardProdName.HeaderText = "المنتج المجاني";
            colRewardProdName.Name = "colRewardProdName";
            colRewardProdName.ReadOnly = true;
            // 
            // colRewardProdId
            // 
            colRewardProdId.HeaderText = "colRewardProdId";
            colRewardProdId.Name = "colRewardProdId";
            colRewardProdId.Visible = false;
            // 
            // colPickReward
            // 
            colPickReward.HeaderText = "اختيار مجاني";
            colPickReward.Name = "colPickReward";
            colPickReward.Text = "بحث";
            colPickReward.UseColumnTextForButtonValue = true;
            // 
            // pnlPromoBot
            // 
            pnlPromoBot.Controls.Add(_btnNewPromo);
            pnlPromoBot.Controls.Add(_btnSavePromo);
            pnlPromoBot.Dock = DockStyle.Bottom;
            pnlPromoBot.Location = new Point(10, 703);
            pnlPromoBot.Name = "pnlPromoBot";
            pnlPromoBot.Size = new Size(869, 50);
            pnlPromoBot.TabIndex = 2;
            // 
            // _btnNewPromo
            // 
            _btnNewPromo.BackColor = Color.Peru;
            _btnNewPromo.Dock = DockStyle.Right;
            _btnNewPromo.ForeColor = Color.White;
            _btnNewPromo.Location = new Point(769, 0);
            _btnNewPromo.Name = "_btnNewPromo";
            _btnNewPromo.Size = new Size(100, 50);
            _btnNewPromo.TabIndex = 1;
            _btnNewPromo.Text = "جديد";
            _btnNewPromo.UseVisualStyleBackColor = false;
            // 
            // _btnSavePromo
            // 
            _btnSavePromo.BackColor = Color.Teal;
            _btnSavePromo.Dock = DockStyle.Left;
            _btnSavePromo.ForeColor = Color.White;
            _btnSavePromo.Location = new Point(0, 0);
            _btnSavePromo.Name = "_btnSavePromo";
            _btnSavePromo.Size = new Size(150, 50);
            _btnSavePromo.TabIndex = 0;
            _btnSavePromo.Text = "حفظ العرض";
            _btnSavePromo.UseVisualStyleBackColor = false;
            // 
            // pnlPromoTop
            // 
            pnlPromoTop.ColumnCount = 4;
            pnlPromoTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            pnlPromoTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlPromoTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            pnlPromoTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlPromoTop.Controls.Add(label1, 0, 0);
            pnlPromoTop.Controls.Add(_txtPromoName, 1, 0);
            pnlPromoTop.Controls.Add(label2, 2, 0);
            pnlPromoTop.Controls.Add(_cmbPromoType, 3, 0);
            pnlPromoTop.Controls.Add(label3, 0, 1);
            pnlPromoTop.Controls.Add(_dtpStart, 1, 1);
            pnlPromoTop.Controls.Add(label4, 2, 1);
            pnlPromoTop.Controls.Add(_dtpEnd, 3, 1);
            pnlPromoTop.Controls.Add(label5, 0, 2);
            pnlPromoTop.Controls.Add(_txtPromoDesc, 1, 2);
            pnlPromoTop.Controls.Add(_chkActive, 3, 2);
            pnlPromoTop.Dock = DockStyle.Top;
            pnlPromoTop.Location = new Point(10, 10);
            pnlPromoTop.Name = "pnlPromoTop";
            pnlPromoTop.RowCount = 3;
            pnlPromoTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            pnlPromoTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            pnlPromoTop.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            pnlPromoTop.Size = new Size(869, 150);
            pnlPromoTop.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(781, 0);
            label1.Name = "label1";
            label1.Size = new Size(85, 18);
            label1.TabIndex = 0;
            label1.Text = "اسم العرض:";
            // 
            // _txtPromoName
            // 
            _txtPromoName.Dock = DockStyle.Fill;
            _txtPromoName.Location = new Point(438, 3);
            _txtPromoName.Name = "_txtPromoName";
            _txtPromoName.Size = new Size(328, 25);
            _txtPromoName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(390, 0);
            label2.Name = "label2";
            label2.Size = new Size(42, 18);
            label2.TabIndex = 2;
            label2.Text = "النوع:";
            // 
            // _cmbPromoType
            // 
            _cmbPromoType.Dock = DockStyle.Fill;
            _cmbPromoType.DropDownStyle = ComboBoxStyle.DropDownList;
            _cmbPromoType.FormattingEnabled = true;
            _cmbPromoType.Items.AddRange(new object[] { "اشتر X واحصل على Y مجاناً", "اشتر X واحصل على Y بخصم", "باقة مجمعة", "تخفيض براند" });
            _cmbPromoType.Location = new Point(3, 3);
            _cmbPromoType.Name = "_cmbPromoType";
            _cmbPromoType.Size = new Size(329, 26);
            _cmbPromoType.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(801, 40);
            label3.Name = "label3";
            label3.Size = new Size(65, 18);
            label3.TabIndex = 4;
            label3.Text = "من تاريخ:";
            // 
            // _dtpStart
            // 
            _dtpStart.Dock = DockStyle.Fill;
            _dtpStart.Location = new Point(438, 43);
            _dtpStart.Name = "_dtpStart";
            _dtpStart.RightToLeftLayout = true;
            _dtpStart.Size = new Size(328, 25);
            _dtpStart.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(363, 40);
            label4.Name = "label4";
            label4.Size = new Size(69, 18);
            label4.TabIndex = 6;
            label4.Text = "إلى تاريخ:";
            // 
            // _dtpEnd
            // 
            _dtpEnd.Dock = DockStyle.Fill;
            _dtpEnd.Location = new Point(3, 43);
            _dtpEnd.Name = "_dtpEnd";
            _dtpEnd.RightToLeftLayout = true;
            _dtpEnd.Size = new Size(329, 25);
            _dtpEnd.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(812, 80);
            label5.Name = "label5";
            label5.Size = new Size(54, 18);
            label5.TabIndex = 8;
            label5.Text = "الوصف:";
            // 
            // _txtPromoDesc
            // 
            _txtPromoDesc.Dock = DockStyle.Fill;
            _txtPromoDesc.Location = new Point(438, 83);
            _txtPromoDesc.Multiline = true;
            _txtPromoDesc.Name = "_txtPromoDesc";
            _txtPromoDesc.Size = new Size(328, 64);
            _txtPromoDesc.TabIndex = 9;
            // 
            // _chkActive
            // 
            _chkActive.AutoSize = true;
            _chkActive.Checked = true;
            _chkActive.CheckState = CheckState.Checked;
            _chkActive.Location = new Point(274, 83);
            _chkActive.Name = "_chkActive";
            _chkActive.Size = new Size(58, 22);
            _chkActive.TabIndex = 10;
            _chkActive.Text = "نشط";
            _chkActive.UseVisualStyleBackColor = true;
            // 
            // _tabCollections
            // 
            _tabCollections.Controls.Add(splitCols);
            _tabCollections.Location = new Point(4, 27);
            _tabCollections.Name = "_tabCollections";
            _tabCollections.Padding = new Padding(3);
            _tabCollections.Size = new Size(1201, 769);
            _tabCollections.TabIndex = 1;
            _tabCollections.Text = "القوائم المخصصة (Custom Groups)";
            _tabCollections.UseVisualStyleBackColor = true;
            // 
            // splitCols
            // 
            splitCols.Dock = DockStyle.Fill;
            splitCols.Location = new Point(3, 3);
            splitCols.Name = "splitCols";
            // 
            // splitCols.Panel1
            // 
            splitCols.Panel1.Controls.Add(_lstCollections);
            splitCols.Panel1.RightToLeft = RightToLeft.Yes;
            // 
            // splitCols.Panel2
            // 
            splitCols.Panel2.Controls.Add(pnlRightCol);
            splitCols.Panel2.RightToLeft = RightToLeft.Yes;
            splitCols.Size = new Size(1195, 763);
            splitCols.SplitterDistance = 302;
            splitCols.TabIndex = 0;
            // 
            // _lstCollections
            // 
            _lstCollections.Dock = DockStyle.Fill;
            _lstCollections.Font = new Font("Tahoma", 12F);
            _lstCollections.FormattingEnabled = true;
            _lstCollections.ItemHeight = 19;
            _lstCollections.Location = new Point(0, 0);
            _lstCollections.Name = "_lstCollections";
            _lstCollections.Size = new Size(302, 763);
            _lstCollections.TabIndex = 0;
            // 
            // pnlRightCol
            // 
            pnlRightCol.Controls.Add(pnlColGrid);
            pnlRightCol.Controls.Add(pnlColBot);
            pnlRightCol.Controls.Add(pnlColTop);
            pnlRightCol.Dock = DockStyle.Fill;
            pnlRightCol.Location = new Point(0, 0);
            pnlRightCol.Name = "pnlRightCol";
            pnlRightCol.Padding = new Padding(10);
            pnlRightCol.Size = new Size(889, 763);
            pnlRightCol.TabIndex = 0;
            // 
            // pnlColGrid
            // 
            pnlColGrid.Controls.Add(_dgvColItems);
            pnlColGrid.Dock = DockStyle.Fill;
            pnlColGrid.Location = new Point(10, 60);
            pnlColGrid.Name = "pnlColGrid";
            pnlColGrid.Padding = new Padding(0, 10, 0, 10);
            pnlColGrid.Size = new Size(869, 643);
            pnlColGrid.TabIndex = 1;
            // 
            // _dgvColItems
            // 
            _dgvColItems.AllowUserToAddRows = false;
            _dgvColItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvColItems.BackgroundColor = Color.White;
            _dgvColItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvColItems.Columns.AddRange(new DataGridViewColumn[] { colItemId, colItemCode, colItemName, colItemDel });
            _dgvColItems.Dock = DockStyle.Fill;
            _dgvColItems.Location = new Point(0, 10);
            _dgvColItems.Name = "_dgvColItems";
            _dgvColItems.Size = new Size(869, 623);
            _dgvColItems.TabIndex = 0;
            // 
            // colItemId
            // 
            colItemId.HeaderText = "colItemId";
            colItemId.Name = "colItemId";
            colItemId.Visible = false;
            // 
            // colItemCode
            // 
            colItemCode.HeaderText = "كود المنتج";
            colItemCode.Name = "colItemCode";
            colItemCode.ReadOnly = true;
            // 
            // colItemName
            // 
            colItemName.HeaderText = "الاسم";
            colItemName.Name = "colItemName";
            colItemName.ReadOnly = true;
            // 
            // colItemDel
            // 
            colItemDel.HeaderText = "حذف";
            colItemDel.Name = "colItemDel";
            colItemDel.Text = "حذف";
            colItemDel.UseColumnTextForButtonValue = true;
            // 
            // pnlColBot
            // 
            pnlColBot.Controls.Add(_btnNewCol);
            pnlColBot.Controls.Add(_btnSaveCol);
            pnlColBot.Controls.Add(btnAddItems);
            pnlColBot.Dock = DockStyle.Bottom;
            pnlColBot.Location = new Point(10, 703);
            pnlColBot.Name = "pnlColBot";
            pnlColBot.Size = new Size(869, 50);
            pnlColBot.TabIndex = 2;
            // 
            // _btnNewCol
            // 
            _btnNewCol.BackColor = Color.Peru;
            _btnNewCol.Dock = DockStyle.Left;
            _btnNewCol.ForeColor = Color.White;
            _btnNewCol.Location = new Point(150, 0);
            _btnNewCol.Name = "_btnNewCol";
            _btnNewCol.Size = new Size(100, 50);
            _btnNewCol.TabIndex = 2;
            _btnNewCol.Text = "جديد";
            _btnNewCol.UseVisualStyleBackColor = false;
            // 
            // _btnSaveCol
            // 
            _btnSaveCol.BackColor = Color.Teal;
            _btnSaveCol.Dock = DockStyle.Left;
            _btnSaveCol.ForeColor = Color.White;
            _btnSaveCol.Location = new Point(0, 0);
            _btnSaveCol.Name = "_btnSaveCol";
            _btnSaveCol.Size = new Size(150, 50);
            _btnSaveCol.TabIndex = 1;
            _btnSaveCol.Text = "حفظ المجموعة";
            _btnSaveCol.UseVisualStyleBackColor = false;
            // 
            // btnAddItems
            // 
            btnAddItems.BackColor = Color.SteelBlue;
            btnAddItems.Dock = DockStyle.Right;
            btnAddItems.ForeColor = Color.White;
            btnAddItems.Location = new Point(719, 0);
            btnAddItems.Name = "btnAddItems";
            btnAddItems.Size = new Size(150, 50);
            btnAddItems.TabIndex = 0;
            btnAddItems.Text = "إضافة منتجات F1";
            btnAddItems.UseVisualStyleBackColor = false;
            // 
            // pnlColTop
            // 
            pnlColTop.Controls.Add(label6);
            pnlColTop.Controls.Add(_txtColName);
            pnlColTop.Dock = DockStyle.Top;
            pnlColTop.Location = new Point(10, 10);
            pnlColTop.Name = "pnlColTop";
            pnlColTop.Size = new Size(869, 50);
            pnlColTop.TabIndex = 0;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(758, 0);
            label6.Name = "label6";
            label6.Padding = new Padding(0, 5, 0, 0);
            label6.Size = new Size(108, 23);
            label6.TabIndex = 0;
            label6.Text = "اسم المجموعة:";
            // 
            // _txtColName
            // 
            _txtColName.Location = new Point(352, 3);
            _txtColName.Name = "_txtColName";
            _txtColName.Size = new Size(400, 25);
            _txtColName.TabIndex = 1;
            // 
            // Frm_ManagePromos
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 244, 248);
            ClientSize = new Size(1209, 800);
            Controls.Add(_tabs);
            Font = new Font("Tahoma", 10F);
            Name = "Frm_ManagePromos";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "إدارة العروض والمجموعات المخصصة";
            _tabs.ResumeLayout(false);
            _tabPromos.ResumeLayout(false);
            splitPromos.Panel1.ResumeLayout(false);
            splitPromos.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitPromos).EndInit();
            splitPromos.ResumeLayout(false);
            pnlRightPromo.ResumeLayout(false);
            pnlPromoGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgvRules).EndInit();
            pnlPromoBot.ResumeLayout(false);
            pnlPromoTop.ResumeLayout(false);
            pnlPromoTop.PerformLayout();
            _tabCollections.ResumeLayout(false);
            splitCols.Panel1.ResumeLayout(false);
            splitCols.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitCols).EndInit();
            splitCols.ResumeLayout(false);
            pnlRightCol.ResumeLayout(false);
            pnlColGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dgvColItems).EndInit();
            pnlColBot.ResumeLayout(false);
            pnlColTop.ResumeLayout(false);
            pnlColTop.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tabs;
        private System.Windows.Forms.TabPage _tabPromos;
        private System.Windows.Forms.TabPage _tabCollections;
        private System.Windows.Forms.SplitContainer splitPromos;
        private System.Windows.Forms.ListBox _lstPromos;
        private System.Windows.Forms.Panel pnlRightPromo;
        private System.Windows.Forms.Panel pnlPromoGrid;
        private System.Windows.Forms.DataGridView _dgvRules;
        private System.Windows.Forms.Panel pnlPromoBot;
        private System.Windows.Forms.Button _btnSavePromo;
        private System.Windows.Forms.Button _btnNewPromo;
        private System.Windows.Forms.TableLayoutPanel pnlPromoTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _txtPromoName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cmbPromoType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker _dtpStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker _dtpEnd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _txtPromoDesc;
        private System.Windows.Forms.CheckBox _chkActive;
        // Colections controls
        private System.Windows.Forms.SplitContainer splitCols;
        private System.Windows.Forms.ListBox _lstCollections;
        private System.Windows.Forms.Panel pnlRightCol;
        private System.Windows.Forms.Panel pnlColGrid;
        private System.Windows.Forms.DataGridView _dgvColItems;
        private System.Windows.Forms.Panel pnlColBot;
        private System.Windows.Forms.Button btnAddItems;
        private System.Windows.Forms.Button _btnSaveCol;
        private System.Windows.Forms.Button _btnNewCol;
        private System.Windows.Forms.FlowLayoutPanel pnlColTop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox _txtColName;
        
        // Columns Promo Rules
        private System.Windows.Forms.DataGridViewComboBoxColumn colCondition;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCondValName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCondValId;
        private System.Windows.Forms.DataGridViewButtonColumn colPickCond;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyQty;
        private System.Windows.Forms.DataGridViewComboBoxColumn colReward;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRewardQtyOrVal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRewardProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRewardProdId;
        private System.Windows.Forms.DataGridViewButtonColumn colPickReward;

        // Columns Col Rules
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemName;
        private System.Windows.Forms.DataGridViewButtonColumn colItemDel;
    }
}
