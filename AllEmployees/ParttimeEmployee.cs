using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    public class ParttimeEmployee : Employee
    {
        string dateOfHire;
        string dateOfTermination;
        decimal hourlyRate;
            
        public ParttimeEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "PT" ? 1 : 0;

            if (ValidateParttime(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], employeeData[index + 5], Convert.ToDecimal(employeeData[index+6])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = employeeData[index + 3];
                dateOfHire = employeeData[index + 4];
                dateOfTermination = employeeData[index + 5];
                hourlyRate = Convert.ToDecimal(employeeData[index + 6]);
            }
        }

        private bool ValidateParttime(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string dateOfHire, string dateOfTermination, decimal hourlyRate)
        {
            bool allValid = false;
            bool[] valid = new bool[5];
            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            valid[1] = ValidateDate(dateOfHire);
            valid[2] = ValidateDate(dateOfTermination);
            valid[3] = ValidateMoney(hourlyRate);
            if (valid[0] & valid[1] & valid[2] & valid[3])
            {
                allValid = true;
            }
            return allValid;
        }

    }
}
