using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayslipCalculator.BusinessDomain
{
    public class Contractor
    {
        private string firstName;
        private string lastName;
        private bool isMarried;
        private int IRD;
        private int noChildren;
        private int noDependents;

        public Contractor(string fName, string lName, bool isMarried, int ird, int noChild)
        {
            if (string.IsNullOrWhiteSpace(fName))
            {
                throw new Exception("first name is detected to be empty.");
            }
            else
            {
                this.firstName = fName;
            }

            if (string.IsNullOrWhiteSpace(lName))
            {
                throw new Exception("last name is detected to be empty.");
            }
            else
            {
                this.lastName = lName;
            }

            if (ird < 0)
            {
                throw new Exception("IRD is detected to be negative number.");
            }
            else
            {
                this.IRD = ird;
            }
            this.isMarried = isMarried;
            this.noChildren = noChild;
            this.GetNumberOfDependents();
        }

        public void GetContractorInfo(out string fName, out string lName, out bool isMarried, out int ird, out int noChild)
        {
            fName = this.firstName;
            lName = this.lastName;
            isMarried = this.isMarried;
            ird = this.IRD;
            noChild = this.noChildren;
        }

        public void GetContractorInfo(out string fName, out string lName, out int ird)
        {
            fName = this.firstName;
            lName = this.lastName;
            ird = this.IRD;
        }

        public int GetNumberOfDependents()
        {
            this.noDependents = this.noChildren;
            if (this.isMarried)
            {
                this.noDependents++;
            }
            if (this.noDependents > 4)
            {
                this.noDependents = 4;
            }
            return this.noDependents;
        }

    }
}
