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

        private bool ValidateContract(string name, string lastName, string businessNumber, string dateOfBirth, string contractStartDate, string contractStopDate, decimal fixedContractAmount)
        {
            bool allValid = false;
            bool[] valid = new bool[5];
            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            if (valid[0])
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
                valid[4] = ValidateBusinessNumber(businessNumber);
            }
            
            valid[1] = ValidateDate(contractStartDate, dateType.CONTRACT_START);
            valid[2] = ValidateDate(contractStopDate, dateType.CONTRACT_END);
            valid[3] = ValidateMoney(fixedContractAmount);
            if (valid[0] & valid[1] & valid[2] & valid[3] & valid[4])
            {
                allValid = true;
            }
            return allValid;
        }

        private bool ValidateBusinessNumber(string businessNumber)
        {

            int newSin = 0;
            Int32.TryParse(socialInsuranceNumber.Replace(" ", string.Empty), out newSin);

            int[] tempSin = new int[9];
            int[] sin = new int[9];
            int[] year = new int[4];
            int tempYear = dateOfBirth.Year;
            double totalSin = 0;
            bool validSin = false;
            int toAdd = 0;
            int theSin = newSin;
            for (int x = 8; x >= 0; x--) //splits the SIN into an array
            {
                sin[x] = newSin % 10;
                newSin /= 10;
            }
            for(int x = 4; x >= 0; x--)
            {
                year[x] = tempYear % 10;
                tempYear /= 10;
            }
            if (sin[0] == year[2] && sin[1] == year[3])
            {
                for (int x = 0; x < 8; x++)
                {
                    toAdd = sinCheck[x] * sin[x];
                    if (toAdd >= 10)
                    {
                        toAdd = (toAdd % 10) + (toAdd / 10);
                    }
                    totalSin += toAdd;
                }

                if ((Math.Ceiling(totalSin / 10) * 10 - totalSin) == sin[8])
                {
                    validSin = true;
                }
            }
            else if (theSin == 0)
            {
                validSin = true;
            }
            return validSin;
        
        }

        protected bool ValidateDate(string date, dateType type)
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
