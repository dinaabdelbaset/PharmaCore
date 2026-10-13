using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace PharmaCore.POS.RE
{
    public partial class RptTaxesReportsammury1 : DevExpress.XtraReports.UI.XtraReport
    {
        public RptTaxesReportsammury1()
        {
            InitializeComponent();

            //if ( ( TotalSalesBill + TotalBuysTaxReport ) - (TotalBuysBill + TotalSalesTaxReport) < 0) 
            //    then
            //    "المبلغ المستحق لك " & 
            //    abs((TotalSalesBill + TotalBuysTaxReport) - (TotalBuysBill + TotalSalesTaxReport))

            
            //else if (( TotalSalesBill + TotalBuysTaxReport ) -  (TotalBuysBill + TotalSalesTaxReport) < 0) 
            //    then
            //     "لا يوجد اى مبالغ مستحقة"
             
            //    else
            //    "المبلغ المستحق عليك " & 
            //        totext((TotalSalesBill + TotalBuysTaxReport ) - (TotalBuysBill + TotalSalesTaxReport) , 1 ,"")





            //   if (( ? ToSaBi+ ? ToBuTaxRe) - (? ToBuBi+ ? ToSaTaxRe) < 0)
             
            //                 "المبلغ المستحق لك " &
            //         abs((? ToSaBi+ ? ToBuTaxRe) - (? ToBuBi+ ? ToSaTaxRe))
             
             
             
            //  else if (( ? ToSaBi+ ? ToBuTaxRe) -  (? ToBuBi+ ? ToSaTaxRe) < 0)
            //                 then "لا يوجد اى مبالغ مستحقة"
             
             
            //  else
            //                 "المبلغ المستحق عليك " &
            //         totext((? ToSaBi+ ? ToBuTaxRe) - (? ToBuBi+ ? ToSaTaxRe), 1, "")
             

        }

    }
}
