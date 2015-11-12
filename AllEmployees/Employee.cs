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
        private string socialInsuranceNumber;
        protected string dateOfBirth;
        private bool isValid = true;

        public string SocialInsuranceNumber
        {
            get
            {
                return socialInsuranceNumber;
            }

            set
            {
                socialInsuranceNumber = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return isValid;
            }

            set
            {
                isValid = value;
            }
        }

        public Employee()
        {
            this.firstName = "";
            this.lastName = "";
            this.SocialInsuranceNumber = "";
            this.dateOfBirth = ""; 
        }

        public Employee(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public Employee(string firstName, string lastName, string socialInsuranceNumber, string dateOfBirth)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.SocialInsuranceNumber = socialInsuranceNumber;
            this.dateOfBirth = dateOfBirth;

        }
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
