using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    
    public class SeasonalEmployee : Employee
    {
        /*public enum seasons
        {
            winter,
            spring,
            summer,
            fall,
        }
        seasons season { get; set; }*/
        string season;
        Decimal piecePay { get; set; }

        public bool VariablesLogString(string[] employeeData)
        {
            bool success = true;
            int index = employeeData[0] == "SN" ? 1 : 0;
            string toLog = "Trying to create Seasonal Employee with:\n||\tFirst Name: " + employeeData[index] +
                        "||Last Name: " + employeeData[index + 1] +
                        "||SIN: " + employeeData[index + 2] +
                        "||Date of Birth: " + employeeData[index + 3] +
                        "||Season: " + employeeData[index + 4] +
                        "||Piece Pay: " + employeeData[index + 5];
            AddToLogString(toLog);
            return success;
        }

        public bool SuccessLogString()
        {
            bool success = true;
            if (IsValid)
            {
                AddToLogString("\t-->Creating Seasonal Employee was successful.");
            }
            else
            {
                AddToLogString("\t-->Creating Seasonal Employee failed.");
            }
            Supporting.Logging.LogString(logString);
            return success;
        }

        public SeasonalEmployee()
        {

        }

        public SeasonalEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "SN" ? 1 : 0;
            VariablesLogString(employeeData);
            if (ValidateSeasonal(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], Convert.ToDecimal(employeeData[index + 5])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                season = employeeData[index + 4];
                piecePay = Convert.ToDecimal(employeeData[index + 5]);
                IsValid = true;
            }
            SuccessLogString();
        }

        private bool ValidateSeasonal(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string season, decimal piecePay)
        {
            bool[] valid = new bool[3] { false, false, false };
            bool allValid = false;

            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            valid[1] = ValidateSeason(season);
            valid[2] = ValidateMoney(piecePay);
            if (valid[0] & valid[1] & valid[2] & valid[3])
            {
                allValid = true;
            }
            return allValid;
        }

        public bool ValidateSeason(string newSeason)
        {
            bool valid = false;

            if (newSeason.ToUpper() == "WINTER")
            {
                valid = true;
            }
            else if (newSeason.ToUpper() == "SPRING")
            {
                valid = true;
            }
            else if (newSeason.ToUpper() == "SUMMER")
            {
                valid = true;
            }
            else if (newSeason.ToUpper() == "FALL")
            {
                valid = true;
            }
            else
            {
                AddToLogString("\tSeason Error: Invalid season.\n\t\tTried: " + newSeason);
            }

            return valid;
        }

    }
}
