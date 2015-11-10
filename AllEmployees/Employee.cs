using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    public class Employee
    {
        protected string firstName;
        protected string lastName;
        protected string socialInsuranceNumber;
        protected string dateOfBirth;

        string ValidateDateOfBirth(string dateOfBirth)
        {
            int year = 0;
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            string newTemp = dateOfBirth.ToLower();
            string formats = "yyyy/mm/dd";
            DateTime dateValue;

            newTemp = newTemp.Remove(4, newTemp.Length - 4);
            if (DateTime.TryParseExact(newTemp, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                //convert string to int
                year = Convert.ToInt32(newTemp);
                //check year range
                if ((year >= 1900) && (year <= 2001))
                {

                }
                else
                {
                    //put error code
                }

            }
            else
            {
                //put error code
            }
            //did not return anything because error code has not been created
            return "";

        }
    }
}
