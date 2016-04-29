using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayslipCalculator.BusinessDomain
{
    public class Payment
    {
        private const double DEPENDENT_BENEFIT_RATE = 0.0875;
        private const double GST_RATE = 0.15;
        private const int HOURLY_RATE = 75;
        private const double INCOME_TAX_RATE = 0.27;
        private const double MEMBERSHIP_RATE = 0.03;

        private double grossPay;
        private int noOfHoursWorked;
        private Contractor theContractor;

        public Payment(Contractor theContractor, int noOfHoursWorked)
        {
            if (null == theContractor)
            {
                throw new Exception("theContractor is detected to be null.");
            }
            else
            {
                this.theContractor = theContractor;
            }

            this.noOfHoursWorked = noOfHoursWorked;

            //basePay = noOfHoursWorked * HOURLY_RATE;
            this.grossPay = this.noOfHoursWorked * HOURLY_RATE - this.GetMembrshipFee();
        }

        public double GetGST()
        {
            return this.grossPay * GST_RATE;
        }

        public double GetIncomeTax()
        {
            double incomeTaxBase = this.grossPay * INCOME_TAX_RATE;
            double dependentsBenifit = incomeTaxBase * theContractor.GetNumberOfDependents() * DEPENDENT_BENEFIT_RATE;
            double incomeTaxDeduction = incomeTaxBase - dependentsBenifit;
            return incomeTaxDeduction;
        }

        public double GetMembrshipFee()
        {
            return this.noOfHoursWorked * HOURLY_RATE * MEMBERSHIP_RATE;
        }

        public double GetTotalDeductions()
        {
            return this.GetGST() + this.GetIncomeTax();
        }

        public double GetNetPay()
        {
            return this.grossPay - this.GetTotalDeductions();
        }


    }
}
