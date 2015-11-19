using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    public class ContractEmployee : Employee
    {
        string contractStartDate;
        string contractStopDate;
        decimal fixedContractAmount;

        public ContractEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "CT" ? 1 : 0;

            if (ValidateContract(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], employeeData[index + 5], Convert.ToDecimal(employeeData[index + 6])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = employeeData[index + 3];
                contractStartDate = employeeData[index + 4];
                contractStopDate = employeeData[index + 5];
                fixedContractAmount = Convert.ToDecimal(employeeData[index + 6]);
            }
        }

        private bool ValidateContract(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string contractStartDate, string contractStopDate, decimal fixedContractAmount)
        {
            bool allValid = false;
            bool[] valid = new bool[5];
            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            valid[1] = ValidateDate(contractStartDate);
            valid[2] = ValidateDate(contractStopDate);
            valid[3] = ValidateMoney(fixedContractAmount);
            if (valid[0] & valid[1] & valid[2] & valid[3])
            {
                allValid = true;
            }
            return allValid;
        }
    }
}
