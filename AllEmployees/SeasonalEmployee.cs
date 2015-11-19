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
        

        private bool ValidateSeason(string newSeason)
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

            return valid;
        }

    }
}
