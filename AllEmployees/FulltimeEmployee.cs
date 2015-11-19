using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    public class FulltimeEmployee : Employee
    {
        string dateOfHire;
        string dateOfTermination;
        decimal salary;
        public FulltimeEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "FT" ? 1 : 0;

            if (ValidateFulltime(employeeData[index], employeeData[index+1], employeeData[index+2], employeeData[index+3], employeeData[index+4], employeeData[index+5], Convert.ToDecimal(employeeData[index+6])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = employeeData[index + 3];
                dateOfHire = employeeData[index + 4];
                dateOfTermination = employeeData[index + 5];
                salary = Convert.ToDecimal(employeeData[index + 6]);
            }
        }

        public FulltimeEmployee(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string dateOfHire, string dateOfTermination, decimal salary)
        {
            if (ValidateFulltime(name, lastName, socialInsuranceNumber, dateOfBirth, dateOfHire, dateOfTermination, salary))
            {
                this.firstName = name;
                this.lastName = lastName;
                this.socialInsuranceNumber = socialInsuranceNumber;
                this.dateOfBirth = dateOfBirth;
                this.dateOfHire = dateOfHire;
                this.dateOfTermination = dateOfTermination;
                this.salary = salary;
            }
        }
        


        private bool ValidateFulltime(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string dateOfHire, string dateOfTermination, decimal salary)
        {
            bool[] valid = new bool[4] { false, false, false, false };
            bool allValid = false;

            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            valid[1] = ValidateDate(dateOfHire);
            valid[2] = ValidateDate(dateOfTermination);
            valid[3] = ValidateMoney(salary);
            if( valid[0] & valid[1] & valid[2] & valid[3])
            {
                allValid = true;
            }
            return allValid;
        }


        public override string ToString()
        {
            return "FT|"+firstName+"|"+lastName+"|"+SocialInsuranceNumber+"|"+dateOfBirth+"|"+dateOfHire+"|"+dateOfTermination+"|"+salary;
        }

        

    }
}
