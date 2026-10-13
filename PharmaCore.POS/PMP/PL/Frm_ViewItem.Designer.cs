namespace PharmaCore.POS.PL
{
    partial class Frm_ViewItem
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
            DataPro = new DataGridView();
            label6 = new Label();
            label3 = new Label();
            label9 = new Label();
            label1 = new Label();
            label16 = new Label();
            button3 = new Button();
            btn_edit = new Button();
            button5 = new Button();
            btnSearch = new Button();
            simpleButton2 = new Button();
            txtTotal = new DevExpress.XtraEditors.TextEdit();
            textActive = new DevExpress.XtraEditors.TextEdit();
            txtName = new DevExpress.XtraEditors.TextEdit();
            txtBarcode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)DataPro).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTotal.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textActive.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBarcode.Properties).BeginInit();
            SuspendLayout();
            // 
            // DataPro
            // 
            DataPro.AllowUserToAddRows = false;
            DataPro.AllowUserToDeleteRows = false;
            DataPro.AllowUserToResizeColumns = false;
            DataPro.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DataPro.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DataPro.BackgroundColor = Color.White;
            DataPro.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.Beige;
            dataGridViewCellStyle1.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle1.SelectionBackColor = Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = Color.Blue;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DataPro.DefaultCellStyle = dataGridViewCellStyle1;
            DataPro.Location = new Point(30, 163);
            DataPro.Name = "DataPro";
            DataPro.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DataPro.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DataPro.RowTemplate.DefaultCellStyle.BackColor = Color.White;
            DataPro.RowTemplate.DefaultCellStyle.ForeColor = Color.Blue;
            DataPro.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 128);
            DataPro.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Blue;
            DataPro.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataPro.Size = new Size(1224, 466);
            DataPro.TabIndex = 211;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.Black;
            label6.Location = new Point(909, 667);
            label6.Name = "label6";
            label6.RightToLeft = RightToLeft.Yes;
            label6.Size = new Size(122, 18);
            label6.TabIndex = 215;
            label6.Text = "اجمالي المنتجات :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(233, 109);
            label3.Name = "label3";
            label3.Size = new Size(88, 18);
            label3.TabIndex = 216;
            label3.Text = "بحث بالاسم:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.Black;
            label9.Location = new Point(812, 109);
            label9.Name = "label9";
            label9.Size = new Size(98, 18);
            label9.TabIndex = 219;
            label9.Text = "المادة الفعالة :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(92, 34);
            label1.Name = "label1";
            label1.Size = new Size(93, 18);
            label1.TabIndex = 224;
            label1.Text = "بحث بالباركود:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.BorderStyle = BorderStyle.Fixed3D;
            label16.ForeColor = Color.Red;
            label16.Location = new Point(1110, 9);
            label16.Name = "label16";
            label16.Size = new Size(144, 20);
            label16.TabIndex = 230;
            label16.Text = "إمكانية البحث بالنجمة";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            button3.Location = new Point(548, 99);
            button3.Name = "button3";
            button3.Size = new Size(135, 38);
            button3.TabIndex = 287;
            button3.Text = "بحث";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // btn_edit
            // 
            btn_edit.Location = new Point(0, 0);
            btn_edit.Name = "btn_edit";
            btn_edit.Size = new Size(75, 23);
            btn_edit.TabIndex = 298;
            // 
            // button5
            // 
            button5.Location = new Point(1064, 99);
            button5.Name = "button5";
            button5.Size = new Size(110, 38);
            button5.TabIndex = 289;
            button5.Text = "بحث";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(830, 24);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(183, 38);
            btnSearch.TabIndex = 290;
            btnSearch.Text = "منتجات انتهت كميتها";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // simpleButton2
            // 
            simpleButton2.Location = new Point(578, 647);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(183, 38);
            simpleButton2.TabIndex = 293;
            simpleButton2.Text = "مسح من البرنامج بشكل نهائي ";
            simpleButton2.UseVisualStyleBackColor = true;
            simpleButton2.Click += simpleButton2_Click;
            // 
            // txtTotal
            // 
            txtTotal.EditValue = "";
            txtTotal.Enabled = false;
            txtTotal.Location = new Point(1048, 655);
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
            txtTotal.Size = new Size(161, 36);
            txtTotal.TabIndex = 294;
            // 
            // textActive
            // 
            textActive.EditValue = "";
            textActive.Location = new Point(916, 106);
            textActive.Name = "textActive";
            textActive.Properties.Appearance.BorderColor = Color.Gray;
            textActive.Properties.Appearance.Font = new Font("Tahoma", 11F);
            textActive.Properties.Appearance.Options.UseBorderColor = true;
            textActive.Properties.Appearance.Options.UseFont = true;
            textActive.Properties.Appearance.Options.UseTextOptions = true;
            textActive.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            textActive.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            textActive.Properties.AppearanceDisabled.BackColor = Color.Silver;
            textActive.Properties.AppearanceDisabled.BorderColor = Color.Black;
            textActive.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            textActive.Properties.AppearanceDisabled.Options.UseBackColor = true;
            textActive.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            textActive.Properties.AppearanceDisabled.Options.UseFont = true;
            textActive.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            textActive.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            textActive.Properties.AppearanceFocused.BackColor = Color.Cyan;
            textActive.Properties.AppearanceFocused.BorderColor = Color.Teal;
            textActive.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            textActive.Properties.AppearanceFocused.Options.UseBackColor = true;
            textActive.Properties.AppearanceFocused.Options.UseBorderColor = true;
            textActive.Properties.AppearanceFocused.Options.UseFont = true;
            textActive.Properties.AppearanceFocused.Options.UseTextOptions = true;
            textActive.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            textActive.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            textActive.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            textActive.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            textActive.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            textActive.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            textActive.Properties.AppearanceReadOnly.Options.UseFont = true;
            textActive.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            textActive.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            textActive.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            textActive.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            textActive.Properties.Name = "textEdit1";
            textActive.RightToLeft = RightToLeft.No;
            textActive.Size = new Size(142, 24);
            textActive.TabIndex = 295;
            textActive.KeyPress += textActive_KeyPress;
            // 
            // txtName
            // 
            txtName.EditValue = "";
            txtName.Location = new Point(344, 106);
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
            txtName.Size = new Size(198, 24);
            txtName.TabIndex = 296;
            txtName.KeyPress += txtName_KeyPress;
            // 
            // txtBarcode
            // 
            txtBarcode.EditValue = "";
            txtBarcode.Location = new Point(39, 55);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Properties.Appearance.BorderColor = Color.Gray;
            txtBarcode.Properties.Appearance.Font = new Font("Tahoma", 11F);
            txtBarcode.Properties.Appearance.Options.UseBorderColor = true;
            txtBarcode.Properties.Appearance.Options.UseFont = true;
            txtBarcode.Properties.Appearance.Options.UseTextOptions = true;
            txtBarcode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtBarcode.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtBarcode.Properties.AppearanceDisabled.BackColor = Color.Silver;
            txtBarcode.Properties.AppearanceDisabled.BorderColor = Color.Black;
            txtBarcode.Properties.AppearanceDisabled.Font = new Font("Tahoma", 11F);
            txtBarcode.Properties.AppearanceDisabled.Options.UseBackColor = true;
            txtBarcode.Properties.AppearanceDisabled.Options.UseBorderColor = true;
            txtBarcode.Properties.AppearanceDisabled.Options.UseFont = true;
            txtBarcode.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            txtBarcode.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtBarcode.Properties.AppearanceFocused.BackColor = Color.Cyan;
            txtBarcode.Properties.AppearanceFocused.BorderColor = Color.Teal;
            txtBarcode.Properties.AppearanceFocused.Font = new Font("Tahoma", 11F);
            txtBarcode.Properties.AppearanceFocused.Options.UseBackColor = true;
            txtBarcode.Properties.AppearanceFocused.Options.UseBorderColor = true;
            txtBarcode.Properties.AppearanceFocused.Options.UseFont = true;
            txtBarcode.Properties.AppearanceFocused.Options.UseTextOptions = true;
            txtBarcode.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtBarcode.Properties.AppearanceReadOnly.BackColor = Color.Silver;
            txtBarcode.Properties.AppearanceReadOnly.BorderColor = Color.Black;
            txtBarcode.Properties.AppearanceReadOnly.Font = new Font("Tahoma", 11F);
            txtBarcode.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            txtBarcode.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            txtBarcode.Properties.AppearanceReadOnly.Options.UseFont = true;
            txtBarcode.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            txtBarcode.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            txtBarcode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            txtBarcode.Properties.ContextImageOptions.Alignment = DevExpress.XtraEditors.ContextImageAlignment.Far;
            txtBarcode.Properties.Name = "textEdit1";
            txtBarcode.RightToLeft = RightToLeft.No;
            txtBarcode.Size = new Size(203, 24);
            txtBarcode.TabIndex = 297;
            txtBarcode.KeyPress += txtBarcode_KeyPress;
            // 
            // Frm_ViewItem
            // 
            Appearance.Options.UseFont = true;
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1285, 706);
            Controls.Add(txtBarcode);
            Controls.Add(txtName);
            Controls.Add(textActive);
            Controls.Add(txtTotal);
            Controls.Add(simpleButton2);
            Controls.Add(btnSearch);
            Controls.Add(button5);
            Controls.Add(btn_edit);
            Controls.Add(button3);
            Controls.Add(label16);
            Controls.Add(label1);
            Controls.Add(label9);
            Controls.Add(label3);
            Controls.Add(label6);
            Controls.Add(DataPro);
            Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            KeyPreview = true;
            Margin = new Padding(4);
            Name = "Frm_ViewItem";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "عرض المنتجات";
            WindowState = FormWindowState.Maximized;
            Load += Frm_ViewItem_Load;
            ((System.ComponentModel.ISupportInitialize)DataPro).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTotal.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textActive.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBarcode.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DataPro;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button simpleButton2;
        private DevExpress.XtraEditors.TextEdit txtTotal;
        private DevExpress.XtraEditors.TextEdit textActive;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtBarcode;
    }
}