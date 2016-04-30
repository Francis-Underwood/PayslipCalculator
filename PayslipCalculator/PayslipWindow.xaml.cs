using System;
using System.Windows;
using PayslipCalculator.BusinessDomain;

namespace PayslipCalculator
{
    /// <summary>
    /// Interaction logic for PayslipWindow.xaml
    /// </summary>
    public partial class PayslipWindow : Window
    {
        private App app;

        public PayslipWindow()
        {
            InitializeComponent();
            this.app = (App)(Application.Current);
            try
            {
                this.app.aPayment = new Payment(this.app.aContractor, this.app.hoursOfWork);
            }
            catch (Exception e)
            {
                this.ShowError(e.Message);
            }

            // context
            string fNameTemp;
            string lNameTemp;
            int irdTemp;
            this.app.aContractor.GetContractorInfo(out fNameTemp, out lNameTemp, out irdTemp);
            this.contractorInfoLbl.Content = string.Format("Payslip: {0} {1}, IRD: {2}", fNameTemp, lNameTemp, irdTemp);
            this.gstLbl.Content = string.Format("NZD {0:0.00}", this.app.aPayment.GetGST());
            this.incomeTaxLbl.Content = string.Format("NZD {0:0.00}", this.app.aPayment.GetIncomeTax());
            this.membershipFeeLbl.Content = string.Format("NZD {0:0.00}", this.app.aPayment.GetMembrshipFee());
            this.totalDeductionLbl.Content = string.Format("NZD {0:0.00}", this.app.aPayment.GetTotalDeductions());
            this.netPayLbl.Content = string.Format("NZD {0:0.00}", this.app.aPayment.GetNetPay());
        }

        private void ReturnBtn_Clicked(object sender, RoutedEventArgs evt)
        {
            this.Close();
        }

        private void ShowError(string errMsg)
        {
            var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                        errMsg,
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.No,
                        (Style)App.Current.Resources["TotaraMessageBoxStyle"]
                    );
        }

    }
}
