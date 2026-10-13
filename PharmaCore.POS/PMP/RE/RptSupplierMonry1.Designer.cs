namespace PharmaCore.POS.RE
{
    partial class RptSupplierMonry1
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptSupplierMonry1));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.Name1 = new DevExpress.XtraReports.UI.XRLabel();
            this.Logo1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.اسمالمستخدم1 = new DevExpress.XtraReports.UI.XRLabel();
            this.Text6 = new DevExpress.XtraReports.UI.XRLabel();
            this.تاريخالفاتورة1 = new DevExpress.XtraReports.UI.XRLabel();
            this.Text4 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.Phone21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrBarCode1 = new DevExpress.XtraReports.UI.XRBarCode();
            this.Address1 = new DevExpress.XtraReports.UI.XRLabel();
            this.Phone11 = new DevExpress.XtraReports.UI.XRLabel();
            this.Description1 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.ID = new DevExpress.XtraReports.Parameters.Parameter();
            this.Total = new DevExpress.XtraReports.UI.CalculatedField();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 20F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 20F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 0F;
            this.Detail.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail.Name = "Detail";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Name1,
            this.Logo1,
            this.اسمالمستخدم1,
            this.Text6,
            this.تاريخالفاتورة1,
            this.Text4});
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 344.0868F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // Name1
            // 
            this.Name1.BackColor = System.Drawing.Color.Transparent;
            this.Name1.BorderColor = System.Drawing.Color.Black;
            this.Name1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.Name1.BorderWidth = 1F;
            this.Name1.CanGrow = false;
            this.Name1.Dpi = 254F;
            this.Name1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Name]")});
            this.Name1.Font = new System.Drawing.Font("Arial", 10F);
            this.Name1.ForeColor = System.Drawing.Color.Black;
            this.Name1.LocationFloat = new DevExpress.Utils.PointFloat(36.95414F, 164.0417F);
            this.Name1.Name = "Name1";
            this.Name1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Name1.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.Name1.SizeF = new System.Drawing.SizeF(626.0037F, 45.86119F);
            this.Name1.StylePriority.UseBorders = false;
            this.Name1.StylePriority.UseTextAlignment = false;
            this.Name1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Logo1
            // 
            this.Logo1.BackColor = System.Drawing.Color.Transparent;
            this.Logo1.BorderColor = System.Drawing.Color.Black;
            this.Logo1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Logo1.BorderWidth = 1F;
            this.Logo1.Dpi = 254F;
            this.Logo1.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Logo1.ForeColor = System.Drawing.Color.Black;
            this.Logo1.LocationFloat = new DevExpress.Utils.PointFloat(170.0558F, 0F);
            this.Logo1.Name = "Logo1";
            this.Logo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Logo1.SizeF = new System.Drawing.SizeF(379.2362F, 164.0417F);
            this.Logo1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // اسمالمستخدم1
            // 
            this.اسمالمستخدم1.BackColor = System.Drawing.Color.Transparent;
            this.اسمالمستخدم1.BorderColor = System.Drawing.Color.Black;
            this.اسمالمستخدم1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.اسمالمستخدم1.BorderWidth = 1F;
            this.اسمالمستخدم1.CanGrow = false;
            this.اسمالمستخدم1.Dpi = 254F;
            this.اسمالمستخدم1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[اسم المورد]")});
            this.اسمالمستخدم1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.اسمالمستخدم1.ForeColor = System.Drawing.Color.Black;
            this.اسمالمستخدم1.LocationFloat = new DevExpress.Utils.PointFloat(36.95414F, 227.5417F);
            this.اسمالمستخدم1.Name = "اسمالمستخدم1";
            this.اسمالمستخدم1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.اسمالمستخدم1.SizeF = new System.Drawing.SizeF(444.5F, 42.3334F);
            this.اسمالمستخدم1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // Text6
            // 
            this.Text6.BackColor = System.Drawing.Color.Transparent;
            this.Text6.BorderColor = System.Drawing.Color.Black;
            this.Text6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Text6.BorderWidth = 1F;
            this.Text6.CanGrow = false;
            this.Text6.Dpi = 254F;
            this.Text6.Font = new System.Drawing.Font("Arial", 10F);
            this.Text6.ForeColor = System.Drawing.Color.Black;
            this.Text6.LocationFloat = new DevExpress.Utils.PointFloat(557.1245F, 287.0729F);
            this.Text6.Name = "Text6";
            this.Text6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Text6.SizeF = new System.Drawing.SizeF(105.8334F, 42.33334F);
            this.Text6.Text = "التاريخ";
            this.Text6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // تاريخالفاتورة1
            // 
            this.تاريخالفاتورة1.BackColor = System.Drawing.Color.Transparent;
            this.تاريخالفاتورة1.BorderColor = System.Drawing.Color.Black;
            this.تاريخالفاتورة1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.تاريخالفاتورة1.BorderWidth = 1F;
            this.تاريخالفاتورة1.CanGrow = false;
            this.تاريخالفاتورة1.Dpi = 254F;
            this.تاريخالفاتورة1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Today()")});
            this.تاريخالفاتورة1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.تاريخالفاتورة1.ForeColor = System.Drawing.Color.Black;
            this.تاريخالفاتورة1.LocationFloat = new DevExpress.Utils.PointFloat(36.95414F, 287.0729F);
            this.تاريخالفاتورة1.Name = "تاريخالفاتورة1";
            this.تاريخالفاتورة1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.تاريخالفاتورة1.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.تاريخالفاتورة1.SizeF = new System.Drawing.SizeF(512.3379F, 42.33334F);
            this.تاريخالفاتورة1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.تاريخالفاتورة1.TextFormatString = "{0:dddd dd/  MMMM (MM) / yyyy }";
            // 
            // Text4
            // 
            this.Text4.BackColor = System.Drawing.Color.Transparent;
            this.Text4.BorderColor = System.Drawing.Color.Black;
            this.Text4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Text4.BorderWidth = 1F;
            this.Text4.CanGrow = false;
            this.Text4.Dpi = 254F;
            this.Text4.Font = new System.Drawing.Font("Arial", 10F);
            this.Text4.ForeColor = System.Drawing.Color.Black;
            this.Text4.LocationFloat = new DevExpress.Utils.PointFloat(481.4541F, 227.5417F);
            this.Text4.Name = "Text4";
            this.Text4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Text4.SizeF = new System.Drawing.SizeF(181.5042F, 42.33334F);
            this.Text4.Text = "المورد";
            this.Text4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Phone21,
            this.xrTable3,
            this.xrBarCode1,
            this.Address1,
            this.Phone11,
            this.Description1});
            this.ReportFooter.Dpi = 254F;
            this.ReportFooter.HeightF = 465.4316F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // Phone21
            // 
            this.Phone21.BackColor = System.Drawing.Color.Transparent;
            this.Phone21.BorderColor = System.Drawing.Color.Black;
            this.Phone21.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Phone21.BorderWidth = 1F;
            this.Phone21.CanGrow = false;
            this.Phone21.Dpi = 254F;
            this.Phone21.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Phone2]")});
            this.Phone21.Font = new System.Drawing.Font("Arial", 10F);
            this.Phone21.ForeColor = System.Drawing.Color.Black;
            this.Phone21.LocationFloat = new DevExpress.Utils.PointFloat(0F, 278.7649F);
            this.Phone21.Name = "Phone21";
            this.Phone21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Phone21.SizeF = new System.Drawing.SizeF(332.4976F, 63.5F);
            this.Phone21.StylePriority.UseTextAlignment = false;
            this.Phone21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Dpi = 254F;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(699.9999F, 63.5F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell21});
            this.xrTableRow5.Dpi = 254F;
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell19.Dpi = 254F;
            this.xrTableCell19.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Total]")});
            this.xrTableCell19.Multiline = true;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.Text = "xrTableCell19";
            this.xrTableCell19.TextFormatString = "{0:C2}";
            this.xrTableCell19.Weight = 3.2248204428972223D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell21.Dpi = 254F;
            this.xrTableCell21.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell21.Multiline = true;
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.Text = "إجمالي المبالغ المتبقية للمورد :";
            this.xrTableCell21.Weight = 2.7751792957564092D;
            // 
            // xrBarCode1
            // 
            this.xrBarCode1.Alignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrBarCode1.AutoModule = true;
            this.xrBarCode1.BackColor = System.Drawing.Color.Empty;
            this.xrBarCode1.Dpi = 254F;
            this.xrBarCode1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[رقم الفاتورة]")});
            this.xrBarCode1.LocationFloat = new DevExpress.Utils.PointFloat(7.466431F, 76.90559F);
            this.xrBarCode1.Module = 5.08F;
            this.xrBarCode1.Name = "xrBarCode1";
            this.xrBarCode1.Padding = new DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254F);
            this.xrBarCode1.SizeF = new System.Drawing.SizeF(686.3943F, 106.6094F);
            this.xrBarCode1.StylePriority.UseBackColor = false;
            this.xrBarCode1.StylePriority.UseTextAlignment = false;
            this.xrBarCode1.Symbology = code128Generator1;
            this.xrBarCode1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Address1
            // 
            this.Address1.BackColor = System.Drawing.Color.Transparent;
            this.Address1.BorderColor = System.Drawing.Color.Black;
            this.Address1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Address1.BorderWidth = 1F;
            this.Address1.CanGrow = false;
            this.Address1.Dpi = 254F;
            this.Address1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Address]")});
            this.Address1.Font = new System.Drawing.Font("Arial", 10F);
            this.Address1.ForeColor = System.Drawing.Color.Black;
            this.Address1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 194.0982F);
            this.Address1.Name = "Address1";
            this.Address1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Address1.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.Address1.SizeF = new System.Drawing.SizeF(693.8608F, 84.66663F);
            this.Address1.StylePriority.UseTextAlignment = false;
            this.Address1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Phone11
            // 
            this.Phone11.BackColor = System.Drawing.Color.Transparent;
            this.Phone11.BorderColor = System.Drawing.Color.Black;
            this.Phone11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Phone11.BorderWidth = 1F;
            this.Phone11.CanGrow = false;
            this.Phone11.Dpi = 254F;
            this.Phone11.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Phone1]")});
            this.Phone11.Font = new System.Drawing.Font("Arial", 10F);
            this.Phone11.ForeColor = System.Drawing.Color.Black;
            this.Phone11.LocationFloat = new DevExpress.Utils.PointFloat(345.2161F, 278.7649F);
            this.Phone11.Name = "Phone11";
            this.Phone11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Phone11.SizeF = new System.Drawing.SizeF(341.5547F, 63.5F);
            this.Phone11.StylePriority.UseTextAlignment = false;
            this.Phone11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Description1
            // 
            this.Description1.BackColor = System.Drawing.Color.Transparent;
            this.Description1.BorderColor = System.Drawing.Color.Black;
            this.Description1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Description1.BorderWidth = 1F;
            this.Description1.CanGrow = false;
            this.Description1.Dpi = 254F;
            this.Description1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Description]")});
            this.Description1.Font = new System.Drawing.Font("Arial", 10F);
            this.Description1.ForeColor = System.Drawing.Color.Black;
            this.Description1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 342.265F);
            this.Description1.Name = "Description1";
            this.Description1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.Description1.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.Description1.SizeF = new System.Drawing.SizeF(693.8607F, 123.1666F);
            this.Description1.StylePriority.UseTextAlignment = false;
            this.Description1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1});
            this.DetailReport.DataSource = this.sqlDataSource1;
            this.DetailReport.Dpi = 254F;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail1.Dpi = 254F;
            this.Detail1.HeightF = 65.26389F;
            this.Detail1.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail1.Name = "Detail1";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Dpi = 254F;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(700F, 65.26389F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
            this.xrTableRow1.Dpi = 254F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Dpi = 254F;
            this.xrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SupplierMoneyReport].[تاريخ الاستحقاق]")});
            this.xrTableCell1.Font = new System.Drawing.Font("Arial", 9F);
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "تاريخ الاستحقاق";
            this.xrTableCell1.Weight = 1.32711187511547D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Dpi = 254F;
            this.xrTableCell2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SupplierMoneyReport].[تاريخ الفاتورة]")});
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "تاريخ الفاتورة";
            this.xrTableCell2.Weight = 1.4318918765783881D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Dpi = 254F;
            this.xrTableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SupplierMoneyReport].[السعر]")});
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "المبلغ المتبقي";
            this.xrTableCell3.Weight = 1.3846286807256158D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Dpi = 254F;
            this.xrTableCell4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SupplierMoneyReport].[رقم الفاتورة]")});
            this.xrTableCell4.Multiline = true;
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "المسلسل";
            this.xrTableCell4.Weight = 0.84923499868709529D;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.HeightF = 65.26389F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Dpi = 254F;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable2.SizeF = new System.Drawing.SizeF(700F, 65.26389F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell13,
            this.xrTableCell11});
            this.xrTableRow3.Dpi = 254F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Dpi = 254F;
            this.xrTableCell9.Font = new System.Drawing.Font("Arial", 9F);
            this.xrTableCell9.Multiline = true;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.Text = "تاريخ الاستحقاق";
            this.xrTableCell9.Weight = 1.32711187511547D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Dpi = 254F;
            this.xrTableCell10.Multiline = true;
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.Text = "تاريخ الفاتورة";
            this.xrTableCell10.Weight = 1.4318918765783881D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Dpi = 254F;
            this.xrTableCell13.Multiline = true;
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Text = "المبلغ المتبقي";
            this.xrTableCell13.Weight = 1.3846291160691071D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Dpi = 254F;
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Text = "المسلسل";
            this.xrTableCell11.Weight = 0.84923456334360425D;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "desktop-o2ht1re\\sqlexpress.pharmacyDB.dbo";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "SupplierMoneyReport";
            storedProcQuery1.StoredProcName = "SupplierMoneyReport";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // ID
            // 
            this.ID.Description = "Parameter1";
            this.ID.Name = "ID";
            this.ID.Type = typeof(double);
            this.ID.ValueInfo = "0";
            // 
            // Total
            // 
            this.Total.DataMember = "SupplierMoneyReport";
            this.Total.Expression = "Sum([السعر])";
            this.Total.Name = "Total";
            // 
            // RptSupplierMonry1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.ReportFooter,
            this.DetailReport});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.Total});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "SupplierMoneyReport";
            this.DataSource = this.sqlDataSource1;
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(48, 48, 20, 20);
            this.PageHeight = 2794;
            this.PageWidth = 796;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ID});
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.RollPaper = true;
            this.SnapGridSize = 25F;
            this.Version = "21.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        public DevExpress.XtraReports.UI.XRLabel Name1;
        public DevExpress.XtraReports.UI.XRPictureBox Logo1;
        private DevExpress.XtraReports.UI.XRLabel اسمالمستخدم1;
        private DevExpress.XtraReports.UI.XRLabel Text6;
        private DevExpress.XtraReports.UI.XRLabel تاريخالفاتورة1;
        private DevExpress.XtraReports.UI.XRLabel Text4;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell19;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell21;
        private DevExpress.XtraReports.UI.XRBarCode xrBarCode1;
        public DevExpress.XtraReports.UI.XRLabel Address1;
        public DevExpress.XtraReports.UI.XRLabel Phone11;
        public DevExpress.XtraReports.UI.XRLabel Description1;
        public DevExpress.XtraReports.UI.XRLabel Phone21;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.Parameters.Parameter ID;
        private DevExpress.XtraReports.UI.CalculatedField Total;
    }
}
