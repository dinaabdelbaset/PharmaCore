namespace PharmaCore.POS.PL
{
    partial class Frm_Store
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            groupBox3 = new GroupBox();
            btnDelete = new Button();
            btn_edit = new Button();
            groupBox1 = new GroupBox();
            btnSave = new Button();
            btnAdd = new Button();
            btnNew = new Button();
            label2 = new Label();
            label1 = new Label();
            txtID = new DevExpress.XtraEditors.TextEdit();
            txtName = new DevExpress.XtraEditors.TextEdit();
            DgvSearch = new DataGridView();
            label6 = new Label();
            txtTotal = new DevExpress.XtraEditors.TextEdit();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtID.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DgvSearch).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTotal.Properties).BeginInit();
            SuspendLayout();
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox3.Controls.Add(btnDelete);
            groupBox3.Controls.Add(btn_edit);
            groupBox3.Font = new Font("Tahoma", 11.25F, FontStyle.Bold);
            groupBox3.ForeColor = Color.FromArgb(0, 100, 100);
            groupBox3.Location = new Point(202, 393);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(340, 89);
            groupBox3.TabIndex = 39;
            groupBox3.TabStop = false;
            groupBox3.Text = "العمليات المتاحة";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(180, 40, 40);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(200, 27);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(116, 38);
            btnDelete.TabIndex = 295;
            btnDelete.Text = "مسح";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btn_edit
            // 
            btn_edit.BackColor = Color.FromArgb(0, 140, 140);
            btn_edit.FlatStyle = FlatStyle.Flat;
            btn_edit.ForeColor = Color.White;
            btn_edit.Location = new Point(48, 27);
            btn_edit.Name = "btn_edit";
            btn_edit.Size = new Size(116, 38);
            btn_edit.TabIndex = 296;
            btn_edit.Text = "تعديل";
            btn_edit.UseVisualStyleBackColor = true;
            btn_edit.Click += btn_edit_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(btnNew);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtID);
            groupBox1.Controls.Add(txtName);
            groupBox1.Font = new Font("Tahoma", 11.25F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(0, 100, 100);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(711, 152);
            groupBox1.TabIndex = 37;
            groupBox1.TabStop = false;
            groupBox1.Text = "بيانات المخزن";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 100, 180);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(145, 92);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(119, 38);
            btnSave.TabIndex = 294;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(0, 140, 100);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(422, 92);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(119, 38);
            btnAdd.TabIndex = 297;
            btnAdd.Text = "إضافة ";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnNew
            // 
            btnNew.BackColor = Color.DarkOrange;
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.ForeColor = Color.White;
            btnNew.Location = new Point(284, 92);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(119, 38);
            btnNew.TabIndex = 293;
            btnNew.Text = "جديد";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Black;
            label2.Location = new Point(258, 45);
            label2.Name = "label2";
            label2.Size = new Size(98, 18);
            label2.TabIndex = 2;
            label2.Text = "اسم المخزن:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Black;
            label1.Location = new Point(602, 45);
            label1.Name = "label1";
            label1.Size = new Size(93, 18);
            label1.TabIndex = 0;
            label1.Text = "رقم المخزن:";
            // 
            // txtID
            // 
            txtID.EditValue = "";
            txtID.Location = new Point(454, 42);
            txtID.Name = "txtID";
            txtID.Properties.Appearance.BorderColor = Color.Gray;
            txtID.Properties.Appearance.Font = new Font("Tahoma", 11F);
            txtID.Properties.Appearance.Options.UseBorderColor = true;
            txtID.Properties.Appearance.Options.UseFont = true;
            txtID.Properties.Appearance.Options.UseTextOptions = true;
            txtID.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtID.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtID.Properties.AppearanceDisabled.BackColor = Color.Silver;
            txtID.Properties.AppearanceDisabled.BorderColor = Color.Black;
            txtID.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtID.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtID.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            txtID.Properties.AppearanceDisabled.Options.UseFont = true;
            txtID.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtID.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtID.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtID.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtID.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtID.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtID.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtID.Properties.AppearanceFocused.Options.UseFont = true;
            txtID.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtID.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtID.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            txtID.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtID.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtID.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtID.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtID.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtID.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtID.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtID.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtID.Properties.Name = "textEdit1";
            txtID.RightToLeft = RightToLeft.No;
            txtID.Size = new Size(142, 24);
            txtID.TabIndex = 290;
            // 
            // txtName
            // 
            txtName.EditValue = "";
            txtName.Location = new Point(49, 42);
            txtName.Name = "txtName";
            txtName.Properties.Appearance.BorderColor = Color.Gray;
            txtName.Properties.Appearance.Font = new Font("Tahoma", 11F);
            txtName.Properties.Appearance.Options.UseBorderColor = true;
            txtName.Properties.Appearance.Options.UseFont = true;
            txtName.Properties.Appearance.Options.UseTextOptions = true;
            txtName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtName.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtName.Properties.AppearanceDisabled.BackColor = Color.Silver;
            txtName.Properties.AppearanceDisabled.BorderColor = Color.Black;
            txtName.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtName.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtName.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            txtName.Properties.AppearanceDisabled.Options.UseFont = true;
            txtName.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtName.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtName.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtName.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtName.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtName.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtName.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtName.Properties.AppearanceFocused.Options.UseFont = true;
            txtName.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtName.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtName.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            txtName.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtName.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtName.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtName.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtName.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtName.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtName.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtName.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtName.Properties.Name = "textEdit1";
            txtName.RightToLeft = RightToLeft.No;
            txtName.Size = new Size(203, 24);
            txtName.TabIndex = 291;
            // 
            // DgvSearch
            // 
            DgvSearch.AllowUserToAddRows = false;
            DgvSearch.AllowUserToDeleteRows = false;
            DgvSearch.AllowUserToResizeColumns = false;
            DgvSearch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DgvSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DgvSearch.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DgvSearch.BackgroundColor = Color.White;
            DgvSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Beige;
            dataGridViewCellStyle1.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle1.SelectionBackColor = Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = Color.Blue;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DgvSearch.DefaultCellStyle = dataGridViewCellStyle1;
            DgvSearch.Location = new Point(18, 183);
            DgvSearch.Name = "DgvSearch";
            DgvSearch.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DgvSearch.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DgvSearch.RowTemplate.DefaultCellStyle.BackColor = Color.White;
            DgvSearch.RowTemplate.DefaultCellStyle.ForeColor = Color.Blue;
            DgvSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 128);
            DgvSearch.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Blue;
            DgvSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvSearch.Size = new Size(711, 204);
            DgvSearch.TabIndex = 40;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.ForeColor = Color.Black;
            label6.Location = new Point(580, 405);
            label6.Name = "label6";
            label6.RightToLeft = RightToLeft.Yes;
            label6.Size = new Size(115, 18);
            label6.TabIndex = 159;
            label6.Text = "اجمالي المخازن :";
            // 
            // txtTotal
            // 
            txtTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtTotal.EditValue = "";
            txtTotal.Enabled = false;
            txtTotal.Location = new Point(583, 426);
            txtTotal.Name = "txtTotal";
            txtTotal.Properties.Appearance.BorderColor = Color.Gray;
            txtTotal.Properties.Appearance.Font = new Font("Tahoma", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotal.Properties.Appearance.Options.UseBorderColor = true;
            txtTotal.Properties.Appearance.Options.UseFont = true;
            txtTotal.Properties.Appearance.Options.UseTextOptions = true;
            txtTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtTotal.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtTotal.Properties.AppearanceDisabled.BackColor = Color.LightGray;
            txtTotal.Properties.AppearanceDisabled.BorderColor = Color.Black;
            txtTotal.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtTotal.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtTotal.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            txtTotal.Properties.AppearanceDisabled.Options.UseFont = true;
            txtTotal.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtTotal.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtTotal.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtTotal.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtTotal.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtTotal.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtTotal.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtTotal.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtTotal.Properties.AppearanceFocused.Options.UseFont = true;
            txtTotal.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtTotal.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtTotal.Properties.AppearanceReadOnly.BackColor = Color.LightGray;
            txtTotal.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtTotal.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtTotal.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtTotal.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtTotal.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtTotal.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtTotal.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtTotal.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtTotal.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtTotal.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtTotal.Properties.Name = "textEdit1";
            txtTotal.RightToLeft = RightToLeft.No;
            txtTotal.Size = new Size(130, 36);
            txtTotal.TabIndex = 292;
            // 
            // Frm_Store
            // 
            Appearance.BackColor = Color.FromArgb(240, 240, 240);
            Appearance.Options.UseBackColor = true;
            Appearance.Options.UseFont = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(741, 494);
            Controls.Add(txtTotal);
            Controls.Add(label6);
            Controls.Add(DgvSearch);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Font = new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            MinimizeBox = false;
            Name = "Frm_Store";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "إدارة المخازن";
            Load += Frm_Store_Load;
            groupBox3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtID.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)DgvSearch).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTotal.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btnAdd;
    }
}