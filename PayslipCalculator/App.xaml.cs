using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PayslipCalculator.BusinessDomain;

namespace PayslipCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public int hoursOfWork;
        public Contractor aContractor;
        public Payment aPayment;
    }
}
