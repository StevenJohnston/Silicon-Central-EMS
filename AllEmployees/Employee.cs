using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AllEmployees
{
    public class Employee
    {
        public static int[] sinCheck = new int[9] { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
        public enum dateType{
            BIRTH,
            HIRE,
            TERMINATE,
            CONTRACT_START,
            CONTRACT_END
        }


        protected string firstName;
        protected string lastName;
        protected string socialInsuranceNumber;
        protected DateTime dateOfBirth;
        private bool isValid = false;
        protected string logString = "";

        public void AddToLogString(string toLog)
        {
            logString += "\n||" + toLog;
        }

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
            //this.dateOfBirth = ""; 
        }

        public Employee(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public Employee(string firstName, string lastName, string socialInsuranceNumber, string dateOfBirth)
        {
            if (ValidateEmployee(firstName, lastName, socialInsuranceNumber, dateOfBirth))
            {
                this.firstName = firstName;
                this.lastName = lastName;
                this.SocialInsuranceNumber = socialInsuranceNumber;
                //this.dateOfBirth = dateOfBirth;
            }
        }


        protected bool ValidateEmployee(string name, string lastName, string socialInsuranceNumber, string dateOfBirth)
        {
            bool[] valid = new bool[4] { false, false, false, false };
            bool allValid = false;
            valid[0] = ValidateName(name);
            valid[1] = ValidateName(lastName);
            valid[2] = ValidateSIN(socialInsuranceNumber);
            valid[3] = ValidateDate(dateOfBirth);
            if(valid[0] & valid[1] & valid[2] & valid[3])
            {
                allValid = true;
            }
            return allValid;
        }

        protected bool ValidateName(string name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z'-]*?$");
        }

        protected bool ValidateSIN(string socialInsuranceNumber)
        {
            //first digit can't be 0 or 8
            int newSin = 0;
            try {
                newSin = Convert.ToInt32(socialInsuranceNumber.Replace(" ", string.Empty));
            }
            catch (FormatException e)
            {
                AddToLogString("\tSIN Error: " + e.Message + "\n||\t\tTried: " + socialInsuranceNumber);
            }
            int[] tempSin = new int[9];
            int[] sin = new int[9];
            double totalSin = 0;
            bool validSin = false;
            int toAdd = 0;
            int theSin = newSin;
            for (int x = 8; x >= 0; x--) //splits the SIN into an array
            {
                sin[x] = newSin % 10;
                newSin /= 10;
            }

            if (sin[0] != 8 && sin[0] >= 1)
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
                else
                {
                    AddToLogString("\tSIN Error: Not a valid SIN.");
                }
            }
            else if (theSin == 0)
            {
                validSin = true;
            }
            else if(sin[0] == 8)
            {
                AddToLogString("\tSIN Error: Cannot start with 8.");
            }
            return validSin;
        }

        protected bool ValidateDate(string date)
        {
            bool valid = false;
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd"};
            DateTime dateValue;

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                if (dateValue <= DateTime.Now)
                {
                    valid = true;
                }
                else
                {
                    AddToLogString("\tDate of Birth Error: Cannot be in the future.");
                }
            }
            else
            {
                AddToLogString("\tDate of Birth Error: Invalid date format.");
            }
            return valid;
        }

        //used to check if date is before beforeDate -- returns true if valid
        static public bool ValidateDate(string date, DateTime beforeDate)
        {
            bool valid = false;
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd" };
            DateTime dateValue;

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                if (dateValue <= beforeDate)
                {
                    valid = true;
                }
            }
            return valid;
        }


        protected bool ValidateMoney(Decimal money)
        {
            bool valid = false;
            if (money >= 0)
            {
                valid = true;
            }
            else
            {
                AddToLogString("\tMoney Value Error: Money values must be 0 or greater.\n||\t\tTried: " + money);
            }
            return valid;
        }
    }
}
