using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    
    public class SeasonalEmployee : Employee
    {
        public enum seasons
        {
            winter,
            spring,
            summer,
            fall,
        }
        seasons season { get; set; }
        Decimal piecePay { get; set; }

        public SeasonalEmployee()
        {

        }

        public SeasonalEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "SS" ? 1 : 0;

            if (ValidateSeasonal(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], (seasons)Convert.ToInt32(employeeData[index + 4]), Convert.ToDecimal(employeeData[index + 5])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                season = (seasons)Convert.ToInt32(employeeData[index + 4]);
                piecePay = Convert.ToDecimal(employeeData[index + 5]);
            }
        }

        private bool ValidateSeasonal(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, seasons season, decimal piecePay)
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

        private bool ValidateSeason(seasons newSeason)
        {
            bool valid = false;

            if (newSeason >= seasons.winter && newSeason <= seasons.fall)
            {
                valid = true;
            } 

            return valid;
        }

    }
}
