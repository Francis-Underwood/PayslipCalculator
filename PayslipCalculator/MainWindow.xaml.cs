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

namespace PayslipCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            int counter = 30;
            int[] ages = new int[counter + 1];
            for (int i=0; i <= counter; i++)
            {
                ages[i] = i;
            }
            this.DataContext = ages;
        }

        private void CalculateBtn_Clicked(object sender, RoutedEventArgs e)
        {
            
            
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
            // validation
            bool valid = true;
            // marital status
            var checkedButton = this.maritalStatus.Children.OfType<RadioButton>()
                                      .FirstOrDefault(r => (true == r.IsChecked));

            if (null == checkedButton)
            {
                valid = false;
            }

            // hours of work

            return valid;
        }


    }
}
