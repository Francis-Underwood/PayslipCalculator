using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections;
using PayslipCalculator.BusinessDomain;
using System.Text.RegularExpressions;

namespace PayslipCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ArrayList errorMessages = new ArrayList();
        private int irdNumer;
        private int hoursOfWork;
        private bool isMarrid;
        private App app;

        public MainWindow()
        {
            InitializeComponent();
            this.app = (App)(Application.Current);
            // init range of number of children
            int counter = 30;
            int[] numberOfChildre_Selections = new int[counter + 1];
            for (int i = 0; i <= counter; i++)
            {
                numberOfChildre_Selections[i] = i;
            }
            this.DataContext = numberOfChildre_Selections;
        }

        private void CalculateBtn_Clicked(object sender, RoutedEventArgs evt)
        {
            bool valid = this.ValidateForm();
            if (valid)
            {
                try
                {
                    this.app.aContractor = new Contractor(
                                                            this.fName.Text,
                                                            this.lName.Text,
                                                            this.isMarrid,
                                                            this.irdNumer,
                                                            (int)this.noChildren.SelectedValue
                                                        );
                }
                catch (Exception e)
                {
                    this.ShowError(e.Message);
                }

                this.app.hoursOfWork = this.hoursOfWork;

                // kill the current Payslip window
                foreach (Window winn in Application.Current.Windows)
                {
                    if (winn.GetType() == typeof(PayslipWindow))
                    {
                        winn.Close();
                    }
                }

                // open a new one
                PayslipWindow payslipWin = new PayslipWindow();
                
                // show the window
                payslipWin.Show();
            }
            else
            {
                string msg = "";
                foreach (Object obj in this.errorMessages)
                {
                    msg = msg + obj.ToString() + "\n";
                }
                this.ShowError(msg);
            }
        }

        private void digitOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private bool ValidateForm()
        {
            // reset
            this.errorMessages.Clear();
            this.irdNumer = 0;
            this.hoursOfWork = 0;

            // validation
            bool valid = true;

            // ird
            if (string.IsNullOrWhiteSpace(this.ird.Text))
            {
                valid = false;
                this.errorMessages.Add("Please fill in the contractor's IRD number.");
            }
            else
            {
                if (this.ird.Text.Length < 8 || this.ird.Text.Length > 9)
                {
                    valid = false;
                    this.errorMessages.Add("Please fill in 8~9 digits as the contractor's IRD number.");
                }
                else
                {
                    Regex regex = new Regex("[^0-9]+");
                    if (regex.IsMatch(this.ird.Text))
                    {
                        //Console.WriteLine("Tera");
                        valid = false;
                        this.errorMessages.Add("Please fill in 8~9 digits only as the contractor's IRD number.");
                    }
                    else
                    {
                        if (!int.TryParse(this.ird.Text, out this.irdNumer))
                        {
                            valid = false;
                            this.errorMessages.Add("Please fill in 8~9 digits only as the contractor's IRD number.");
                        }
                    }
                }
            }

            // first name
            if (string.IsNullOrWhiteSpace(this.fName.Text))
            {
                valid = false;
                this.errorMessages.Add("Please fill in the contractor's first name.");
            }
            else
            {
                Regex regex = new Regex("[^A-Za-z,.()]+");
                if (regex.IsMatch(this.fName.Text))
                {
                    valid = false;
                    this.errorMessages.Add("Please don't fill in strange symbols in the first name.");
                }
            }

            // last name
            if (string.IsNullOrWhiteSpace(this.lName.Text))
            {
                valid = false;
                this.errorMessages.Add("Please fill in the contractor's last name.");
            }
            else
            {
                Regex regex = new Regex("[^A-Za-z,.()]+");
                if (regex.IsMatch(this.lName.Text))
                {
                    valid = false;
                    this.errorMessages.Add("Please don't fill in strange symbols in the last name.");
                }
            }

            // no of children

            // marital status
            var checkedButton = this.maritalStatus.Children.OfType<RadioButton>()
                                  .FirstOrDefault(r => (true == r.IsChecked));

            if (null == checkedButton)
            {
                valid = false;
                this.errorMessages.Add("Please select the contractor's marital status.");
            }
            else
            {
                if ("Married" == checkedButton.Content.ToString())
                {
                    this.isMarrid = true;
                }
                else
                {
                    this.isMarrid = false;
                }
            }

            // hours of work
            if (string.IsNullOrWhiteSpace(this.workedHours.Text))
            {
                valid = false;
                this.errorMessages.Add("Please fill in hours of work.");
            }
            else
            {
                Regex regex = new Regex("[^0-9]+");
                if (regex.IsMatch(this.workedHours.Text))
                {
                    valid = false;
                    this.errorMessages.Add("Please fill in positive integer only as the hours of work.");
                }
                else
                {
                    if (!int.TryParse(this.workedHours.Text, out this.hoursOfWork))
                    {
                        valid = false;
                        this.errorMessages.Add("Please fill in positive integer only as the hours of work.");
                    }
                    else
                    {
                        if (this.hoursOfWork > 24 * 7)
                        {
                            valid = false;
                            this.errorMessages.Add("Don't be ridiculous, there can't be more than " + (24 * 7) + " hours in a week.");
                        }
                    }
                }
            }

            return valid;
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
