namespace PharmaCore.POS.RE
{
    partial class RptUses
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
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabUses = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabUses});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 111.419F;
            this.Detail.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail.Name = "Detail";
            // 
            // xrLabUses
            // 
            this.xrLabUses.AutoWidth = true;
            this.xrLabUses.BackColor = System.Drawing.Color.Transparent;
            this.xrLabUses.BorderColor = System.Drawing.Color.Black;
            this.xrLabUses.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabUses.BorderWidth = 1F;
            this.xrLabUses.CanShrink = true;
            this.xrLabUses.Dpi = 254F;
            this.xrLabUses.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabUses.ForeColor = System.Drawing.Color.Black;
            this.xrLabUses.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabUses.Name = "xrLabUses";
            this.xrLabUses.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.xrLabUses.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
            this.xrLabUses.SizeF = new System.Drawing.SizeF(340F, 111.419F);
            this.xrLabUses.StylePriority.UseFont = false;
            this.xrLabUses.StylePriority.UsePadding = false;
            this.xrLabUses.StylePriority.UseTextAlignment = false;
            this.xrLabUses.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // RptUses
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 170;
            this.PageWidth = 340;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.RollPaper = true;
            this.SnapGridSize = 25F;
            this.Version = "21.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        public DevExpress.XtraReports.UI.XRLabel xrLabUses;
    }
}
