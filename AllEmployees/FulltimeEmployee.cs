using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    public class FulltimeEmployee : Employee
    {
        DateTime dateOfHire;
        DateTime dateOfTermination;
        decimal salary;
        public FulltimeEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "FT" ? 1 : 0;

            if (ValidateFulltime(employeeData[index], employeeData[index+1], employeeData[index+2], employeeData[index+3], employeeData[index+4], employeeData[index+5], Convert.ToDecimal(employeeData[index+6])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                dateOfHire = Convert.ToDateTime(employeeData[index + 4]);
                dateOfTermination = Convert.ToDateTime(employeeData[index + 5]);
                salary = Convert.ToDecimal(employeeData[index + 6]);
            }
        }

        private bool ValidateFulltime(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string dateOfHire, string dateOfTermination, decimal salary)
        {
            bool[] valid = new bool[4] { false, false, false, false };
            bool allValid = false;

            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            if (valid[0])
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
            }
            valid[1] = ValidateDate(dateOfHire, dateType.HIRE);
            valid[2] = ValidateDate(dateOfTermination, dateType.TERMINATE);
            valid[3] = ValidateMoney(salary);
            if( valid[0] & valid[1] & valid[2] & valid[3])
            {
                allValid = true;
            }
            return allValid;
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
                    case dateType.HIRE:
                        if (dateValue >= dateOfBirth && dateValue <= DateTime.Now)
                        {
                            valid = true;
                            dateOfHire = dateValue;
                        }
                        break;
                    case dateType.TERMINATE:
                        if (dateValue >= dateOfHire && dateValue <= DateTime.Now)
                        {
                            valid = true;
                            dateOfTermination = dateValue;
                        }
                        break;
                }
            }
            return valid;
        }
        
        


        public override string ToString()
        {
            return "FT|"+firstName+"|"+lastName+"|"+SocialInsuranceNumber+"|"+dateOfBirth+"|"+dateOfHire+"|"+dateOfTermination+"|"+salary;
        }

        

    }
}
