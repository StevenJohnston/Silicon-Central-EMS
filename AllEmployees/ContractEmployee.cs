using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    public class ContractEmployee : Employee
    {
        DateTime contractStartDate;
        DateTime contractStopDate;
        decimal fixedContractAmount;

        public ContractEmployee()
        {

        }

        public ContractEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "CT" ? 1 : 0;

            if (ValidateContract(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], employeeData[index + 5], Convert.ToDecimal(employeeData[index + 6])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                contractStartDate = Convert.ToDateTime(employeeData[index + 4]);
                contractStopDate = Convert.ToDateTime(employeeData[index + 5]);
                fixedContractAmount = Convert.ToDecimal(employeeData[index + 6]);
            }
        }

        private bool ValidateContract(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string contractStartDate, string contractStopDate, decimal fixedContractAmount)
        {
            bool allValid = false;
            bool[] valid = new bool[5];
            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            if (valid[0])
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
            }
            valid[1] = ValidateDate(contractStartDate, dateType.CONTRACT_START);
            valid[2] = ValidateDate(contractStopDate, dateType.CONTRACT_END);
            valid[3] = ValidateMoney(fixedContractAmount);
            if (valid[0] & valid[1] & valid[2] & valid[3])
            {
                allValid = true;
            }
            return allValid;
        }

        protected virtual bool ValidateDate(string date, dateType type)
        {
            bool valid = false;
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd" };
            DateTime dateValue;

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                switch (type)
                {
                    case dateType.BIRTH:
                        if (dateValue <= DateTime.Now)
                        {
                            valid = true;
                            dateOfBirth = dateValue;
                        }
                        break;
                    case dateType.CONTRACT_START:
                        if (dateValue >= dateOfBirth && dateValue <= DateTime.Now)
                        {
                            valid = true;
                            contractStartDate = dateValue;
                        }
                        break;
                    case dateType.CONTRACT_END:
                        if (dateValue >= contractStartDate && dateValue <= DateTime.Now)
                        {
                            valid = true;
                            contractStopDate = dateValue;
                        }
                        break;
                }
            }
            return valid;
        }

    }
}
