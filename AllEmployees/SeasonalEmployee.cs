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
        seasons season;
        double piecePay;
    }
}
