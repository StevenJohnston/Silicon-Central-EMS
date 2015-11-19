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
            if (ValidateEmployee(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3]))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                SocialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = employeeData[index + 3];
            }
            dateOfHire = employeeData[index + 4];
            dateOfTermination = employeeData[index + 5];
            salary = Convert.ToDecimal(employeeData[index + 6]);
        }
        public override string ToString()
        {
            return "FT|"+firstName+"|"+lastName+"|"+SocialInsuranceNumber+"|"+dateOfBirth+"|"+dateOfHire+"|"+dateOfTermination+"|"+salary;
        }

    }
}
