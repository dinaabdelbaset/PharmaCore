using DevExpress.XtraBars;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using PharmaCore.POS.BL;
using PharmaCore.POS.PL;
using PharmaCore.POS.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PharmaCore.POS
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();

            // 1. Create a dynamic gradient background panel
            Panel gradientPanel = new Panel();
            gradientPanel.Name = "gradientPanel";
            gradientPanel.Dock = DockStyle.Fill;
            gradientPanel.Paint += (s, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    gradientPanel.ClientRectangle,
                    Color.FromArgb(215, 228, 248),  // Light blue-indigo
                    Color.FromArgb(243, 229, 245),  // Soft pastel purple
                    45F))
                {
                    e.Graphics.FillRectangle(brush, gradientPanel.ClientRectangle);
                }
            };
            this.Controls.Add(gradientPanel);
            gradientPanel.SendToBack();

            // 2. Move dashboard groupboxes and other controls to the gradientPanel to support transparency
            var controlsToMove = new System.Collections.Generic.List<Control>();
            foreach (Control c in this.Controls)
            {
                if (!(c is DevExpress.XtraBars.Ribbon.RibbonControl) && 
                    !(c is DevExpress.XtraBars.Ribbon.RibbonStatusBar) && 
                    c != gradientPanel)
                {
                    controlsToMove.Add(c);
                }
            }
            foreach (var c in controlsToMove)
            {
                this.Controls.Remove(c);
                gradientPanel.Controls.Add(c);
            }

            PharmaCore.POS.Helpers.ThemeHelper.ApplyModernTheme(this);
            PharmaCore.POS.Helpers.ThemeHelper.StyleDataGridView(DgvSearch);
            PharmaCore.POS.Helpers.ThemeHelper.StyleDataGridView(DgvCust);
            InitializeAiAssistant();
            InitializeUiCustomizer();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            barStaticItem18.Caption = DateTime.Now.ToShortDateString();
            barStaticItem17.Caption = string.IsNullOrWhiteSpace(CurrentEmployeeSession.Role)
                ? CurrentEmployeeSession.FullName
                : $"{CurrentEmployeeSession.FullName}  —  {CurrentEmployeeSession.Role}";
            barState.Caption = Properties.Settings.Default.StaName;
            await LoadDashboardStatsAsync();
        }

        private class KpiMetricDto
        {
            public decimal Value { get; set; }
            public double ChangePercent { get; set; }
        }

        private class ExpiryRiskDto
        {
            public string Barcode { get; set; }
            public string ProductName { get; set; }
            public string BranchName { get; set; }
            public decimal StockQuantity { get; set; }
            public DateTime ExpiryDate { get; set; }
            public int DaysRemaining { get; set; }
        }

        private class StockRiskDto
        {
            public string Barcode { get; set; }
            public string ProductName { get; set; }
            public string BranchName { get; set; }
            public decimal StockQuantity { get; set; }
            public decimal MinQtyAlert { get; set; }
        }

        private class MonthlyRevenueDto
        {
            public string Month { get; set; }
            public decimal Revenue { get; set; }
            public int Orders { get; set; }
        }

        private class TopProductDto
        {
            public string Name { get; set; }
            public decimal Quantity { get; set; }
            public decimal Revenue { get; set; }
        }

        private class PharmacyHealthDto
        {
            public string InventoryStatus { get; set; }
            public int LowStockCount { get; set; }
            public int ExpiredCount { get; set; }
            public double SalesTrendPercent { get; set; }
            public int StagnantCount { get; set; }
            public int OverdueDebtsCount { get; set; }
        }

        private class DashboardStatsDto
        {
            public KpiMetricDto TotalRevenue { get; set; }
            public KpiMetricDto SalesCount { get; set; }
            public int ActiveCashiers { get; set; }
            public List<ExpiryRiskDto> ExpiryRisks { get; set; }
            public List<StockRiskDto> StockRisks { get; set; }
            public List<MonthlyRevenueDto> RevenueChart { get; set; }
            public List<TopProductDto> TopProducts { get; set; }
            public PharmacyHealthDto PharmacyHealth { get; set; }
        }

        private class ProductListResponse
        {
            public int Total { get; set; }
            public int Page { get; set; }
            public int Size { get; set; }
        }

        private async System.Threading.Tasks.Task LoadDashboardStatsAsync()
        {
            string logPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PharmaCorePOS", "dashboard.log");
            try
            {
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now} | INFO: Starting LoadDashboardStatsAsync..." + Environment.NewLine);
                using (var http = PharmaCore.POS.Services.ConnectionManager.GetHttpClient())
                {
                    var token = Properties.Settings.Default.ApiToken;
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now} | INFO: Using BaseUrl={http.BaseAddress}, TokenLength={token?.Length ?? 0}" + Environment.NewLine);
                    
                    var response = await http.GetAsync("/api/dashboard/stats");
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now} | INFO: /api/dashboard/stats returned StatusCode={response.StatusCode}" + Environment.NewLine);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var stats = Newtonsoft.Json.JsonConvert.DeserializeObject<DashboardStatsDto>(json);
                        if (stats != null)
                        {
                            decimal totalRev = stats.TotalRevenue?.Value ?? 0;
                            int salesCount = (int)(stats.SalesCount?.Value ?? 0);
                            int expiryCount = stats.ExpiryRisks?.Count ?? 0;
                            int stockCount = stats.StockRisks?.Count ?? 0;

                            labToSelas.Text = Math.Round(totalRev, 2).ToString();
                            labToBuy.Text = salesCount.ToString();
                            labExpire.Text = expiryCount.ToString();
                            labQtyMin.Text = stockCount.ToString();
                            
                            // Net Margin or simple indicator
                            labRb7.Text = Math.Round(totalRev, 2).ToString();

                            // Out of stock or high risk count:
                            int outOfStock = 0;
                            if (stats.StockRisks != null)
                            {
                                foreach (var r in stats.StockRisks)
                                {
                                    if (r.StockQuantity <= 0)
                                        outOfStock++;
                                }
                            }
                            labEndQty.Text = outOfStock.ToString();

                            // Bind SaleChart (Monthly Sales)
                            if (stats.RevenueChart != null && stats.RevenueChart.Count > 0 && SaleChart != null)
                            {
                                SaleChart.Series.Clear();
                                var saleSeries = new DevExpress.XtraCharts.Series("المبيعات الشهرية", DevExpress.XtraCharts.ViewType.Bar);
                                foreach (var item in stats.RevenueChart)
                                {
                                    saleSeries.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.Month, (double)item.Revenue));
                                }
                                SaleChart.Series.Add(saleSeries);
                            }

                            // Bind TopChart (Top Products by Revenue)
                            if (stats.TopProducts != null && stats.TopProducts.Count > 0 && TopChart != null)
                            {
                                TopChart.Series.Clear();
                                var topSeries = new DevExpress.XtraCharts.Series("الأكثر مبيعاً", DevExpress.XtraCharts.ViewType.Pie);
                                foreach (var item in stats.TopProducts.Take(5))
                                {
                                    topSeries.Points.Add(new DevExpress.XtraCharts.SeriesPoint(item.Name, (double)item.Revenue));
                                }
                                TopChart.Series.Add(topSeries);
                            }

                            if (stats.PharmacyHealth != null)
                            {
                                RenderPharmacyHealth(stats.PharmacyHealth);
                            }
                        }
                    }
                    else
                    {
                        var errText = await response.Content.ReadAsStringAsync();
                        System.IO.File.AppendAllText(logPath, $"{DateTime.Now} | ERROR: API returned error content: {errText}" + Environment.NewLine);
                    }

                    var prodResponse = await http.GetAsync("/api/products?page=1&size=1");
                    System.IO.File.AppendAllText(logPath, $"{DateTime.Now} | INFO: /api/products returned StatusCode={prodResponse.StatusCode}" + Environment.NewLine);
                    if (prodResponse.IsSuccessStatusCode)
                    {
                        var prodJson = await prodResponse.Content.ReadAsStringAsync();
                        var prodObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductListResponse>(prodJson);
                        if (prodObj != null)
                        {
                            labTotal.Text = prodObj.Total.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(logPath, $"{DateTime.Now} | EXCEPTION: {ex}" + Environment.NewLine);
                System.Diagnostics.Debug.WriteLine($"Error loading dashboard stats: {ex.Message}");
            }
        }

        private void RenderPharmacyHealth(PharmacyHealthDto health)
        {
            if (groupBox5.InvokeRequired)
            {
                groupBox5.Invoke(new Action(() => RenderPharmacyHealth(health)));
                return;
            }

            groupBox5.Controls.Clear();
            groupBox5.Tag = "🏥 مؤشرات صحة الصيدلية (Pharmacy Health)";
            groupBox5.Text = "";
            groupBox5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            
            // Acrylic Light theme colors
            groupBox5.ForeColor = Color.FromArgb(30, 41, 59);
            groupBox5.BackColor = Color.Transparent;

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 1;
            layout.RowCount = 6;
            layout.Padding = new Padding(15);
            
            for (int i = 0; i < 6; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6f));
            }

            groupBox5.Controls.Add(layout);

            Panel CreateHealthCard(string title, string desc, string status)
            {
                Panel pnl = new Panel();
                pnl.Dock = DockStyle.Fill;
                pnl.Padding = new Padding(10, 5, 10, 5);
                pnl.Margin = new Padding(0, 5, 0, 5);
                pnl.BackColor = Color.White;

                Label lblDot = new Label();
                lblDot.Text = status == "success" ? "🟢" : (status == "warning" ? "🟡" : "🔴");
                lblDot.Font = new Font("Segoe UI", 14F);
                lblDot.AutoSize = true;
                lblDot.Location = new Point(10, 10);
                pnl.Controls.Add(lblDot);

                Label lblTitle = new Label();
                lblTitle.Text = title;
                lblTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                lblTitle.ForeColor = Color.FromArgb(100, 116, 139);
                lblTitle.Location = new Point(45, 6);
                lblTitle.AutoSize = true;
                pnl.Controls.Add(lblTitle);

                Label lblDesc = new Label();
                lblDesc.Text = desc;
                lblDesc.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                lblDesc.ForeColor = Color.FromArgb(30, 41, 59);
                lblDesc.Location = new Point(45, 26);
                lblDesc.AutoSize = true;
                pnl.Controls.Add(lblDesc);

                return pnl;
            }

            // 1. Inventory Health
            string invDesc = health.LowStockCount == 0 ? "المخزون: جيد" : $"يوجد {health.LowStockCount} منتج أوشك على النفاد";
            string invStatus = health.LowStockCount == 0 ? "success" : "warning";
            layout.Controls.Add(CreateHealthCard("📦 المخزون", invDesc, invStatus), 0, 0);

            // 2. Expiry Health
            string expDesc = health.ExpiredCount == 0 ? "صلاحية المنتجات: سليمة" : $"يوجد {health.ExpiredCount} منتج منتهي الصلاحية";
            string expStatus = health.ExpiredCount == 0 ? "success" : "danger";
            layout.Controls.Add(CreateHealthCard("📅 الصلاحية", expDesc, expStatus), 0, 1);

            // 3. Sales Trend Today vs Yesterday
            string trendSign = health.SalesTrendPercent >= 0 ? "+" : "";
            string salesDesc = health.SalesTrendPercent == 0 
                ? "المبيعات مستقرة اليوم" 
                : $"المبيعات اليوم {(health.SalesTrendPercent >= 0 ? "أعلى" : "أقل")} من أمس بـ {trendSign}{health.SalesTrendPercent:F1}%";
            string salesStatus = health.SalesTrendPercent >= 0 ? "success" : "warning";
            layout.Controls.Add(CreateHealthCard("📈 المبيعات اليومية", salesDesc, salesStatus), 0, 2);

            // 4. Stagnant Items (90 days)
            string stagnantDesc = health.StagnantCount == 0 ? "لا توجد أدوية راكدة" : $"يوجد {health.StagnantCount} منتج راكد منذ 90 يوماً";
            string stagnantStatus = health.StagnantCount == 0 ? "success" : "warning";
            layout.Controls.Add(CreateHealthCard("⏳ المنتجات الراكدة", stagnantDesc, stagnantStatus), 0, 3);

            // 5. Overdue Debts
            string debtDesc = health.OverdueDebtsCount == 0 ? "لا توجد مديونيات متأخرة" : $"يوجد عميل لديه مديونية معلقة";
            string debtStatus = health.OverdueDebtsCount == 0 ? "success" : "danger";
            layout.Controls.Add(CreateHealthCard("💳 مديونيات العملاء", debtDesc, debtStatus), 0, 4);

            // 6. Overall Status
            string generalDesc = (health.ExpiredCount == 0 && health.LowStockCount == 0) ? "الصيدلية تعمل بشكل ممتاز" : "يرجى اتخاذ إجراء بشأن التنبيهات";
            string generalStatus = (health.ExpiredCount == 0 && health.LowStockCount == 0) ? "success" : "warning";
            layout.Controls.Add(CreateHealthCard("🏥 حالة الصيدلية العامة", generalDesc, generalStatus), 0, 5);
        }


        private void GrafCatagorias(int Type)
        {
            string date1;
            string date2;
            date1 = SaleDtpDate1.Value.ToString("yyyy-MM-dd");
            date2 = SaleDtpDate2.Value.ToString("yyyy-MM-dd");

            DataTable tbl = new DataTable();

            if (SaleDay.Checked == true)
            {
                tbl.Clear();
                tbl = db.readData(" select day(CONVERT(date, [Date] ,105)) as Day , sum (TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Date is not null group by day(CONVERT(date, [Date] ,105)) order by day(CONVERT(date, [Date] ,105)) ", "");

                if (tbl.Rows.Count >= 1)
                {
                    getChart(Type, tbl, "day");
                }
            }
            else if (SaleMonth.Checked == true)
            {
                tbl.Clear();
                tbl = db.readData(" select month(CONVERT(date, [Date] ,105)) as Month , sum (TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Date is not null group by month(CONVERT(date, [Date] ,105)) order by month(CONVERT(date, [Date] ,105)) ", "");

                if (tbl.Rows.Count >= 1)
                {
                    getChart(Type, tbl, "Month");
                }
            }
            else if (SaleYear.Checked == true)
            {
                tbl.Clear();
                tbl = db.readData(" select year(CONVERT(date, [Date] ,105)) as year , sum (TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Date is not null group by year(CONVERT(date, [Date] ,105)) order by year(CONVERT(date, [Date] ,105)) ", "");

                if (tbl.Rows.Count >= 1)
                {
                    getChart(Type, tbl, "year");
                }
            }

            else
            {
                MessageBox.Show(" لا توجد بيانات لعرضها ");
            }
        }

        private void getChart(int Type, DataTable table, string interval)
        {


            // Removed LiveCharts code. Will implement DevExpress chart series binding later.
        }

        private void TopUser(int Type)
        {
            try
            {


                string date1;
                string date2;
                date1 = TopDtpDate1.Value.ToString("yyyy-MM-dd");
                date2 = TopDtpDate2.Value.ToString("yyyy-MM-dd");

                DataTable tblTop = new DataTable();

                tblTop.Clear();
                tblTop = db.readData(" select top 3 Us_Name as 'المستخدم', COUNT (CASE  WHEN TotalNeed != 0 THEN 1 END) as 'عدد عمليات البيع' , sum(TotalNeed) as 'إجمالي البيع' from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' group by Us_Name order by sum(TotalNeed) desc ", "");

                DgvSearch.DataSource = tblTop;

                if (DgvSearch.Rows.Count >= 1)
                {
                    if (DgvSearch.Rows.Count < 3)
                    {

                        MessageBox.Show(" انت تحتاج على الأقل لإضافة 3 مستخدمين لبدأ الرسم البياني ");
                        return;
                    }
                    DgvSearch.Rows[0].DefaultCellStyle.BackColor = System.Drawing.Color.LightPink;
                    DgvSearch.Rows[1].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                    DgvSearch.Rows[2].DefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                    DgvSearch.ClearSelection();

                    DataTable tblT1 = new DataTable();
                    DataTable tblT2 = new DataTable();
                    DataTable tblT3 = new DataTable();


                    string t1 = "";
                    string t2 = "";
                    string t3 = "";

                    if (TopDay.Checked == true)
                    {
                        tblT1.Clear();
                        tblT1 = db.readData(" select Month(Date) as Day , sum(TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Us_Name = N'" + DgvSearch.Rows[0].Cells[0].Value + "'  and Date is not null group by Month(Date) order by Month(Date) ", "");
                        t1 = (DgvSearch.Rows[0].Cells[0].Value).ToString();

                        tblT2.Clear();
                        tblT2 = db.readData(" select Month(Date) as Day , sum(TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Us_Name = N'" + DgvSearch.Rows[1].Cells[0].Value + "'  and Date is not null group by Month(Date) order by Month(Date) ", "");
                        t2 = (DgvSearch.Rows[1].Cells[0].Value).ToString();

                        tblT3.Clear();
                        tblT3 = db.readData(" select Month(Date) as Day , sum(TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Us_Name = N'" + DgvSearch.Rows[2].Cells[0].Value + "'  and Date is not null group by Month(Date) order by Month(Date) ", "");
                        t3 = (DgvSearch.Rows[2].Cells[0].Value).ToString();
                    }
                    else if (TopMonth.Checked == true)
                    {
                        tblT1.Clear();
                        tblT1 = db.readData(" select day(Date) as Month , sum(TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Us_Name = N'" + DgvSearch.Rows[0].Cells[0].Value + "'  and Date is not null group by day(Date) order by day(Date) ", "");
                        t1 = (DgvSearch.Rows[0].Cells[0].Value).ToString();

                        tblT2.Clear();
                        tblT2 = db.readData(" select day(Date) as Month , sum(TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Us_Name = N'" + DgvSearch.Rows[1].Cells[0].Value + "'  and Date is not null group by day(Date) order by day(Date) ", "");
                        t2 = (DgvSearch.Rows[1].Cells[0].Value).ToString();

                        tblT3.Clear();
                        tblT3 = db.readData(" select day(Date) as Month , sum(TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Us_Name = N'" + DgvSearch.Rows[2].Cells[0].Value + "'  and Date is not null group by day(Date) order by day(Date) ", "");
                        t3 = (DgvSearch.Rows[2].Cells[0].Value).ToString();
                    }
                    else if (TopYear.Checked == true)
                    {
                        tblT1.Clear();
                        tblT1 = db.readData(" select year(Date) as year , sum(TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Us_Name = N'" + DgvSearch.Rows[0].Cells[0].Value + "'  and Date is not null group by year(Date) order by year(Date) ", "");
                        t1 = (DgvSearch.Rows[0].Cells[0].Value).ToString();

                        tblT2.Clear();
                        tblT2 = db.readData(" select year(Date) as year , sum(TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Us_Name = N'" + DgvSearch.Rows[1].Cells[0].Value + "'  and Date is not null group by year(Date) order by year(Date) ", "");
                        t2 = (DgvSearch.Rows[1].Cells[0].Value).ToString();

                        tblT3.Clear();
                        tblT3 = db.readData(" select year(Date) as year , sum(TotalNeed) as Total from Sales where Convert(date, Date ,105 ) between '" + date1 + "' and '" + date2 + "' and Us_Name = N'" + DgvSearch.Rows[2].Cells[0].Value + "'  and Date is not null group by year(Date) order by year(Date) ", "");
                        t3 = (DgvSearch.Rows[2].Cells[0].Value).ToString();
                    }


                    ChartTopUser(Type, tblT1, tblT2, tblT3, t1, t2, t3);
                }
                else
                {
                    MessageBox.Show(" لا توجد بيانات لعرضها ");
                }
            }
            catch { }
        }


        private void ChartTopUser(int Type, DataTable table1, DataTable table2, DataTable table3, string lablel1, string lablel2, string lablel3)
        {
            // Removed LiveCharts code. Will implement DevExpress chart series binding later.
        }

        private void Cust()
        {
            DataTable tblCust = new DataTable();

            tblCust.Clear();
            tblCust = db.readData(" select top 5 Cust_Name as 'اسم العميل' , COUNT (CASE  WHEN TotalNeed != 0 THEN 1 END) as 'عدد مرات الشراء' , sum(TotalNeed) as 'إجمالي المبلغ' from Sales group by Cust_Name order by sum(TotalNeed) desc ", "");

            DgvCust.DataSource = tblCust;

            DgvCust.ClearSelection();

        }


        private void Pro()
        {
            DataTable tblPro = new DataTable();

            tblPro.Clear();
            tblPro = db.readData(" select COUNT (CASE  WHEN Qty != 0 THEN 1 END) as Total ,sum(Sale_PriceTax * Qty ) as ToSelas , sum(PH_Price * Qty ) as ToBuy from Products ", "");

            decimal Total = Convert.ToDecimal(tblPro.Rows[0][0]);
            decimal ToSelas = Convert.ToDecimal(tblPro.Rows[0][1]);
            decimal ToBuy = Convert.ToDecimal(tblPro.Rows[0][2]);

            labTotal.Text = Math.Round(Total, 2).ToString();
            labToSelas.Text = Math.Round(ToSelas, 2).ToString();
            labToBuy.Text = Math.Round(ToBuy, 2).ToString();
            labRb7.Text = Math.Round(ToSelas - ToBuy, 2).ToString();
        }

        private void Expire()
        {
            string now;
            string now2;
            now = DateTime.Now.ToString("yyyy-MM-dd");
            now2 = DateTime.Now.AddMonths(2).ToString("yyyy-MM-dd");

            DataTable tblExpire = new DataTable();
            DataTable tblMinQty = new DataTable();
            DataTable tblMinEnd = new DataTable();
            DataTable tblN89 = new DataTable();

            tblExpire.Clear();
            tblExpire = db.readData(" select count ( Pro_ID ) as expire FROM [dbo].[Products] where Convert(date, Expier ,105 ) between '" + now + "' and '" + now2 + "' ", "");

            tblMinQty.Clear();
            tblMinQty = db.readData(" select count ( Pro_ID ) as QtyReauerd FROM [dbo].[Products] where MinQty >= 1 and Qty <= MinQty ", "");

            tblMinEnd.Clear();
            tblMinEnd = db.readData(" select count ( Pro_ID ) as ProEnd FROM [dbo].[Products] where Qty <= 0 ", "");

            tblN89.Clear();
            tblN89 = db.readData(" select count ( N89_ID ) as ProN89 FROM [dbo].[N89_Table] ", "");

            decimal Expire = Convert.ToDecimal(tblExpire.Rows[0][0]);
            decimal QtyReauerd = Convert.ToDecimal(tblMinQty.Rows[0][0]);
            decimal ProEnd = Convert.ToDecimal(tblMinEnd.Rows[0][0]);
            decimal ProN89 = Convert.ToDecimal(tblN89.Rows[0][0]);

            labExpire.Text = Math.Round(Expire, 2).ToString();
            labQtyMin.Text = Math.Round(QtyReauerd, 2).ToString();
            labEndQty.Text = Math.Round(ProEnd, 2).ToString();
            labProN89.Text = Math.Round(ProN89, 2).ToString();
        }

        private bool checkUser(string permissionKey)
        {
            if (CurrentEmployeeSession.HasPermission(permissionKey)) return true;
            
            string diag = $"User: {CurrentEmployeeSession.FullName}\nRole: {CurrentEmployeeSession.Role}\nIsSystemRole: {CurrentEmployeeSession.IsSystemRole}";
            MessageBox.Show($"انت لا تملك صلاحيه الدخول لهذه الشاشة\n\n{diag}", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            AuditLogger.Log("PermissionDenied", "Auth", "Form1",
                $"محاولة دخول بدون صلاحية: {permissionKey}", severity: 1);
            return false;
        }
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("customers.view");
            if (check == true)
            {
                Frm_Customer frm = new Frm_Customer();
                frm.Show();
            }
        }

// //         private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("suppliers.view");
// //             if (check == true)
// //             {
// //                 Frm_Suppliers frm = new Frm_Suppliers();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("accounting.type");
// //             if (check == true)
// //             {
// //                 Frm_DeservedType frm = new Frm_DeservedType();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("accounting.entry");
            if (check == true)
            {
                Frm_Deserved frm = new Frm_Deserved();
                frm.Show();
            }
        }

// //         private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("accounting.report");
// //             if (check == true)
// //             {
// //                 Frm_DeservedReport frm = new Frm_DeservedReport();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("products.add");
            if (check == true)
            {
                Frm_Products frm = new Frm_Products();
                frm.Show();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("purchases.create");
            if (check == true)
            {
                Frm_Buy frm = new Frm_Buy();
                frm.Show();
            }
        }

// //         private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("suppliers.money");
// //             if (check == true)
// //             {
// //                 Frm_SupplierMoney frm = new Frm_SupplierMoney();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("suppliers.report");
// //             if (check == true)
// //             {
// //                 //Frm_SupplierReport frm = new Frm_SupplierReport();
// //                 //frm.Show();
// //             }
// //         }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("products.view");
            if (check == true)
            {
                Frm_ViewItem frm = new Frm_ViewItem();
                frm.Show();
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("purchases.report");
            if (check == true)
            {
                Frm_BuyReport frm = new Frm_BuyReport();
                frm.Show();
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("sales.create");
            if (check == true)
            {
                Frm_Sales frm = new Frm_Sales();
                frm.Show();
            }
        }

// //         private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("customers.money");
// //             if (check == true)
// //             {
// //                 Frm_CustomerMoney frm = new Frm_CustomerMoney();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("customers.report");
// //             if (check == true)
// //             {
// //                 Frm_CustomerReport frm = new Frm_CustomerReport();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("sales.user_report");
            if (check == true)
            {
                Frm_SalesReport frm = new Frm_SalesReport();
                frm.Show();
            }
        }

// //         private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("sales.profit");
// //             if (check == true)
// //             {
// //                 Frm_SalesRb7h frm = new Frm_SalesRb7h();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("returns.sale");
            if (check == true)
            {
                Frm_Return frm = new Frm_Return();
                frm.Show();
            }
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("returns.report");
            if (check == true)
            {
                Frm_ReturnReport frm = new Frm_ReturnReport();
                frm.Show();
            }
        }

// //         private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.add_stock");
// //             if (check == true)
// //             {
// //                 Frm_AddStock frm = new Frm_AddStock();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.stock_deposit");
// //             if (check == true)
// //             {
// //                 Frm_StockAddMoney frm = new Frm_StockAddMoney();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem33_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.bank_deposit");
// //             if (check == true)
// //             {
// //                 Frm_BankAddMoney frm = new Frm_BankAddMoney();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.stock_withdraw");
// //             if (check == true)
// //             {
// //                 Frm_StockPullMoney frm = new Frm_StockPullMoney();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.bank_withdraw");
// //             if (check == true)
// //             {
// //                 Frm_BankPullMoney frm = new Frm_BankPullMoney();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem36_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.transfer");
// //             if (check == true)
// //             {
// //                 Frm_StockTransfire frm = new Frm_StockTransfire();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.bank_stock_transfer");
// //             if (check == true)
// //             {
// //                 Frm_StockBankTransfire frm = new Frm_StockBankTransfire();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem38_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.balance");
// //             if (check == true)
// //             {
// //                 Frm_currentMoney frm = new Frm_currentMoney();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem40_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.report");
// //             if (check == true)
// //             {
// //                 Frm_StockAddMoneyReport frm = new Frm_StockAddMoneyReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem41_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.report");
// //             if (check == true)
// //             {
// //                 Frm_BankAddMoneyReport frm = new Frm_BankAddMoneyReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem42_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.report");
// //             if (check == true)
// //             {
// //                 Frm_StockPullMoneyReport frm = new Frm_StockPullMoneyReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem43_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.report");
// //             if (check == true)
// //             {
// //                 Frm_BankPullMoneyReport frm = new Frm_BankPullMoneyReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem44_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.report");
// //             if (check == true)
// //             {
// //                 Frm_StockTransfireReport frm = new Frm_StockTransfireReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem45_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("treasury.report");
// //             if (check == true)
// //             {
// //                 Frm_StockBankTransfireReport frm = new Frm_StockBankTransfireReport();
// //                 frm.Show();
// //             }
// //         }


        DDatabase db = new DDatabase(); // TODO: remove when dashboard is API-backed

        private void RunPostgresBackup(string backupFilePath)
        {
            try
            {
                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "pg_dump.exe",
                    Arguments = $"-h localhost -p 5432 -U postgres -F c -b -v -f \"{backupFilePath}\" PharmaCoreDB",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true
                };
                processInfo.EnvironmentVariables["PGPASSWORD"] = "12345";
                using (var process = System.Diagnostics.Process.Start(processInfo))
                {
                    string err = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("تم أخذ النسخة الاحتياطية بنجاح!", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AuditLogger.Log("Backup", "Backup", "Form1", $"أخذ نسخة احتياطية بنجاح: {backupFilePath}");
                    }
                    else
                    {
                        MessageBox.Show("فشل أخذ النسخة الاحتياطية: " + err, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء النسخ الاحتياطي: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunPostgresRestore(string backupFilePath)
        {
            try
            {
                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "pg_restore.exe",
                    Arguments = $"-h localhost -p 5432 -U postgres -d PharmaCoreDB -c --clean --if-exists -v \"{backupFilePath}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true
                };
                processInfo.EnvironmentVariables["PGPASSWORD"] = "12345";
                using (var process = System.Diagnostics.Process.Start(processInfo))
                {
                    string err = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("تم استرجاع النسخة الاحتياطية بنجاح!", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AuditLogger.Log("Restore", "Backup", "Form1", $"استرجاع نسخة احتياطية بنجاح: {backupFilePath}");
                    }
                    else
                    {
                        MessageBox.Show("فشل استرجاع النسخة الاحتياطية: " + err, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء استرجاع النسخة الاحتياطية: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("backup.create");
            if (check == true)
            {
                try
                {
                    string d = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    SaveFileDialog open = new SaveFileDialog();
                    open.Filter = "Backup Files (*.backup) | *.backup";
                    open.FileName = "Pharmacy_Backup_" + d;

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        RunPostgresBackup(open.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("backup.restore");
            if (check == true)
            {
                try
                {
                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "Backup Files (*.backup) | *.backup";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        RunPostgresRestore(open.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

// //         private void barButtonItem46_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("employees.add");
// //             if (check == true)
// //             {
// //                 // فتح شاشة الإدارة الجديدة بدل شاشة قديمة
// //                 using var frm = new PharmaCore.POS.PL.Frm_EmployeeManagement();
// //                 frm.ShowDialog(this);
// //             }
// //         }

// //         private void barButtonItem47_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             //bool check = checkUser("Borrow_Items", "User_Emp");
// //             //if (check == true)
// //             //{
// //             //    Frm_EmployeeBorrowItems frm = new Frm_EmployeeBorrowItems();
// //             //    frm.Show();
// //             //}
// //         }

// //         private void barButtonItem48_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("employees.salary");
// //             if (check == true)
// //             {
// //                 Frm_EmployeeSalary frm = new Frm_EmployeeSalary();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem49_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("employees.borrow_money");
// //             if (check == true)
// //             {
// //                 Frm_EmployeeBorrowMoney frm = new Frm_EmployeeBorrowMoney();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem50_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("employees.borrow_items_report");
// //             if (check == true)
// //             {
// //                 Frm_EmployeeBorrowItemsReport frm = new Frm_EmployeeBorrowItemsReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem52_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("employees.salary_report");
// //             if (check == true)
// //             {
// //                 Frm_EmployeeSalaryReport frm = new Frm_EmployeeSalaryReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem51_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("employees.borrow_money_report");
// //             if (check == true)
// //             {
// //                 Frm_EmployeeBorrowMoneyReport frm = new Frm_EmployeeBorrowMoneyReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem53_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("accounting.receipt");
// //             if (check == true)
// //             {
// //                 Frm_SanadKabd frm = new Frm_SanadKabd();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem54_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("accounting.payment");
// //             if (check == true)
// //             {
// //                 Frm_SanadSarf frm = new Frm_SanadSarf();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem55_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("accounting.voucher_report");
// //             if (check == true)
// //             {
// //                 Frm_SanadReport frm = new Frm_SanadReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem56_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("accounting.tax_report");
// //             if (check == true)
// //             {
// //                 Frm_TaxesReport frm = new Frm_TaxesReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem57_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("products.unit");
// //             if (check == true)
// //             {
// //                 Frm_Unit frm = new Frm_Unit();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem59_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("warehouse.add");
// //             if (check == true)
// //             {
// //                 Frm_Store frm = new Frm_Store();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem60_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("warehouse.inventory");
            if (check == true)
            {
                Frm_StoreGard frm = new Frm_StoreGard();
                frm.Show();
            }
        }

// //         private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("permissions.manage");
// //             if (check == true)
// //             {
// //                 Frm_Permission frm = new Frm_Permission();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("settings.view");
            if (check == true)
            {
                Frm_Setting frm = new Frm_Setting();
                frm.Show();
            }
        }

        private void barButtonItem61_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("warehouse.transfer");
            if (check == true)
            {
                Frm_Storetransfire frm = new Frm_Storetransfire();
                frm.Show();
            }
        }

// //         private void barButtonItem62_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("warehouse.transfer_report");
// //             if (check == true)
// //             {
// //                 Frm_StoretransfireReport frm = new Frm_StoretransfireReport();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem63_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool check = checkUser("warehouse.out");
            if (check == true)
            {
                Frm_ProductsOutStore frm = new Frm_ProductsOutStore();
                frm.Show();
            }
        }

// //         private void barButtonItem65_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("warehouse.out_report");
// //             if (check == true)
// //             {
// //                 Frm_ProductsOutStoreReport frm = new Frm_ProductsOutStoreReport();
// //                 frm.Show();
// //             }
// //         }

        private void barStaticItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/profile.php?id=100022090175179");
        }

        private void barButtonItem66_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool check = checkUser("products.barcode");
            if (check == true)
            {
                new PharmaCore.POS.Forms.Frm_ReportManager().ShowDialog();
            }
        }

// //         private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("reports.customers");
// //             if (check == true)
// //             {
// //                 Frm_CustomerReport frm = new Frm_CustomerReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("reports.suppliers");
// //             if (check == true)
// //             {
// //                 //Frm_SupplierReport frm = new Frm_SupplierReport();
// //                 //frm.Show();
// //             }
// //         }

// //         private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("reports.purchases");
// //             if (check == true)
// //             {
// //                 Frm_BuyReport frm = new Frm_BuyReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("reports.sales");
// //             if (check == true)
// //             {
// //                 Frm_SalesReport frm = new Frm_SalesReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("reports.returns");
// //             if (check == true)
// //             {
// //                 Frm_ReturnReport frm = new Frm_ReturnReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("reports.accounting");
// //             if (check == true)
// //             {
// //                 Frm_DeservedReport frm = new Frm_DeservedReport();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem67_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool check = checkUser("shifts.close");
            if (check == true)
            {
                Frm_Close frm = new Frm_Close();
                frm.Show();
            }
        }

        private void barButtonItem68_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool check = checkUser("shifts.open");
            if (check == true)
            {
                Frm_Open frm = new Frm_Open();
                frm.Show();
            }
        }

        private void barButtonItem72_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool check = checkUser("products.expire");
            if (check == true)
            {
                Frm_Expear frm = new Frm_Expear();
                frm.Show();
            }
        }

        private void barButtonItem71_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool check = checkUser("purchases.na89");
            if (check == true)
            {
                Frm_Na89 frm = new Frm_Na89();
                frm.Show();
            }
        }

// //         private void barButtonItem73_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("sales.insurance_company");
// //             if (check == true)
// //             {
// //                 Frm_Company frm = new Frm_Company();
// //                 frm.Show();
// //             }
// //         }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

// //         private void barButtonItem74_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("customers.points");
// //             if (check == true)
// //             {
// //                 Frm_Point frm = new Frm_Point();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem75_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("customers.points_report");
// //             if (check == true)
// //             {
// //                 Frm_PointReport frm = new Frm_PointReport();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem76_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("sales.insurance_report");
// //             if (check == true)
// //             {
// //                 Frm_CutTameenReport frm = new Frm_CutTameenReport();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem77_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool check = checkUser("sales.report");
            if (check == true)
            {
                Frm_CutSalesReport frm = new Frm_CutSalesReport();
                frm.Show();
            }
        }

// //         private void barButtonItem80_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("products.activate");
// //             if (check == true)
// //             {
// //                 Frm_ProductActive frm = new Frm_ProductActive();
// //                 frm.Show();
// //             }
// //         }

// //         private void barButtonItem78_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("returns.buy");
// //             if (check == true)
// //             {
// //                 Frm_ReturnBuy frm = new Frm_ReturnBuy();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem82_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool check = checkUser("products.required");
            if (check == true)
            {
                Frm_ProductsReauerd frm = new Frm_ProductsReauerd();
                frm.Show();
            }
        }

        private void TopRefesh_Click(object sender, EventArgs e)
        {
            try
            {
                TopUser(1);
            }
            catch { }
        }

        private void TopMax_Click(object sender, EventArgs e)
        {
            try
            {
                TopUser(2);
            }
            catch { }
        }

        private void SaleRefresh_Click(object sender, EventArgs e)
        {

        }
        private void SelaMax_Click(object sender, EventArgs e)
        {

        }

        private void CustRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cust();
            }
            catch { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool check = checkUser("products.financials");
            if (check == true)
            {
                // simpleButton2.BringToFront();
                // Pro(); // TODO: call API instead of dead SQL
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool check = checkUser("products.financials");
            if (check == true)
            {
                simpleButton1.BringToFront();
                labTotal.Text = ".................";
                labToSelas.Text = ".................";
                labToBuy.Text = ".................";
                labRb7.Text = ".................";
            }
        }

        private void ExpireRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {

        }

// //         private void barButtonItem69_ItemClick(object sender, ItemClickEventArgs e)
// //         {
// //             bool check = checkUser("reports.shifts");
// //             if (check == true)
// //             {
// //                 Frm_SheftReport frm = new Frm_SheftReport();
// //                 frm.Show();
// //             }
// //         }

        private void barButtonItem79_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool check = checkUser("reports.shifts");
            if (check == true)
            {
                Frm_BlackBox frm = new Frm_BlackBox();
                frm.Show();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AuditLogger.Log("Logout", "Auth", "Form1", "تسجيل خروج من البرنامج");
            AuditLogger.EndSession();
            System.Threading.Thread.Sleep(300); // give fire-and-forget a moment
            Application.Exit();
        }
    }
}
