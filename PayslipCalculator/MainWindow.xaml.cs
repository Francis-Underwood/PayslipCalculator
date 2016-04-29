using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

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

        public MainWindow()
        {
            InitializeComponent();
            int counter = 30;
            int[] ages = new int[counter + 1];
            for (int i = 0; i <= counter; i++)
            {
                ages[i] = i;
            }
            this.DataContext = ages;
            //this.calBtn.IsEnabled = false;
        }

        private void CalculateBtn_Clicked(object sender, RoutedEventArgs e)
        {
            this.ValidateForm();
            foreach (Object obj in this.errorMessages)
            {
                Console.WriteLine("{0}", obj);
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
                this.errorMessages.Add("Please fill in the contractor's ird number.");
            }
            else
            {
                if (this.ird.Text.Length < 8 || this.ird.Text.Length > 9)
                {
                    valid = false;
                    this.errorMessages.Add("Please fill in 8~9 digits as the contractor's ird number.");
                }
                else
                {
                    if (!int.TryParse(this.ird.Text, out this.irdNumer))
                    {
                        valid = false;
                        this.errorMessages.Add("Please fill in 8~9 digits only as the contractor's ird number.");
                    }
                }
            }

            // first name
            if (string.IsNullOrWhiteSpace(this.fName.Text))
            {
                valid = false;
                this.errorMessages.Add("Please fill in the contractor's first name.");
            }

            // last name
            if (string.IsNullOrWhiteSpace(this.lName.Text))
            {
                valid = false;
                this.errorMessages.Add("Please fill in the contractor's last name.");
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

            // hours of work
            if (string.IsNullOrWhiteSpace(this.workedHours.Text))
            {
                valid = false;
                this.errorMessages.Add("Please fill in worked hours.");
            }
            else
            {
                if (!int.TryParse(this.workedHours.Text, out this.hoursOfWork))
                {
                    valid = false;
                    this.errorMessages.Add("Please fill in integer only as the worked hours.");
                }
                else
                {
                    if (this.hoursOfWork > 24 * 7)
                    {
                        valid = false;
                        this.errorMessages.Add("Don't be ridiculous, there can't be more than " + (24*7) + " hours in a week.");
                    }
                }
            }

            return valid;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Console.WriteLine("Lisa Ann");
        }


    }
}
