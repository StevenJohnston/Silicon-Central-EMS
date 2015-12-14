using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using EMSExceptions;

namespace AllEmployees
{
    /// <summary>
    /// This represents the Employee Class which is the parent class which all other employee 
    /// will inherit from
    /// </summary>
    public class Employee
    {
        public static int[] sinCheck = new int[9] { 1, 2, 1, 2, 1, 2, 1, 2, 1 }; //!< the int array to check for
        public EmployeeException employeeEx = new EmployeeException();
        /// An enum type. 
        /// That contain the dateType of information that is needed
        ///For each employee
        public enum dateType{
            BIRTH, /// Birth of the emplogyee
            HIRE, /// Hire
            TERMINATE, /// When the employee was fired
            CONTRACT_START, ///When was there Contract started
            CONTRACT_END /// When does their contrac end
        }


        private string firstName; //!<the first name
        private string lastName; //!<the last name
        protected string socialInsuranceNumber; //!< the social Insurance Number
        protected DateTime? dateOfBirth; //!<the Date of birth
        private bool isValid = false; //!<bool validate or not
        protected string logString = ""; //!<The message to be logged 

        /// <summary>
        /// Formatting the string to be display in the logging file
        /// </summary>
        /// <param name="toLog"></param>

        public void AddToLogString(string toLog)
        {
            logString += "\n||" + toLog;
        }
        /// <summary>
        /// A getter and setter for socialInsuranceNumber the varible
        /// </summary>
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

        /// <summary>
        /// A getter and setter for isValid the varible
        /// </summary>
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

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        /// <summary>
        /// Employee Constructor that sets the first name, last name and social insurance number to default values
        /// </summary>
        public Employee()
        {
            this.FirstName = ""; //!<User first name
            this.LastName = ""; //!<User Last Name
            this.SocialInsuranceNumber = ""; //!<User Social Insurance Number
            this.dateOfBirth = DateTime.MinValue; 
        }
        /// <summary>
        /// Constructor that take 2 parameter
        /// Employee Class that set the first name and last name 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        public Employee(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        /// <summary>
        /// A constructor that takes 4 parameters and set first anme, last name, socail insurance number and date of birth
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="socialInsuranceNumber"></param>
        /// <param name="dateOfBirth"></param>
        public Employee(string firstName, string lastName, string socialInsuranceNumber, string dateOfBirth)
        {
            if (ValidateAndSetEmployee(firstName, lastName, socialInsuranceNumber, dateOfBirth))
            {
                /*this.FirstName = firstName; //!< Setting the first name 
                this.lastName = lastName; //!< setting the last name
                this.SocialInsuranceNumber = socialInsuranceNumber; //!< setting the socail insurance number
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);*/
            }
        }

        public bool Validate()
        {
            bool status = ValidateAndSetEmployee(this.FirstName, this.LastName, this.socialInsuranceNumber, (Convert.ToString(this.dateOfBirth.Value.Year)+ "/" + Convert.ToString(this.dateOfBirth.Value.Month) + "/" + Convert.ToString(dateOfBirth.Value.Day)));
            return status; 
        }


        /// <summary>
        /// Validate the employee name, last name, SIN, and Date of birth
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="socialInsuranceNumber"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns>allValid</returns>


        protected bool ValidateAndSetEmployee(string name, string lastName, string socialInsuranceNumber, string dateOfBirth)
        {
            bool allValid = true;
            bool[] valid = new bool[4] { false, false, false, false }; //!< bool array that has 4 bool set to false
            if (ValidateName(name))
            {
                this.FirstName = name;
            }
            else
            {
                allValid = false;
            }

            if (ValidateName(lastName))
            {
                this.LastName = lastName;
            }
            else
            {
                allValid = false;
            }
            
            if (ValidateDate(dateOfBirth))
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
            }
            else
            {
                allValid = false;
            }
            if (ValidateSIN(socialInsuranceNumber))
            {
                this.socialInsuranceNumber = socialInsuranceNumber;
            }
            else
            {
                allValid = false;
            }
            return allValid;
        }

        /// <summary>
        /// Validate employee name with regex
        /// </summary>
        /// <param name="name"></param>
        /// <returns>bool</returns>
        protected bool ValidateName(string name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z'-]*?$");/// return a bool depending on the regex
        }
        /// <summary>
        /// ValidateSIN base on the the rules and regulation followed by the canadian SIN standards 
        /// </summary>
        /// <param name="socialInsuranceNumber"></param>
        /// <returns></returns>
        protected virtual bool ValidateSIN(string socialInsuranceNumber)
        {
            int newSin = 0; //!<first digit can't be 0 or 8
            try {
                newSin = Convert.ToInt32(socialInsuranceNumber.Replace(" ", string.Empty));
            }
            catch (FormatException e)
            {
                employeeEx.AddError("SIN Error: " + e.Message + " Tried: " + socialInsuranceNumber);
                //AddToLogString("\tSIN Error: " + e.Message + "\n||\t\tTried: " + socialInsuranceNumber);
            }
            int[] tempSin = new int[9]; //!< temp sin number 
            int[] sin = new int[9]; //!< sin number
            double totalSin = 0; //!<
            bool validSin = false; //!< if the sin is valid or not
            int toAdd = 0;//!<
            int theSin = newSin; //!<Setting a temp storage of the sin
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
                    employeeEx.AddError("\tSIN Error: Not a valid SIN. Tried: " + socialInsuranceNumber);
                    //AddToLogString("\tSIN Error: Not a valid SIN.");
                }
            }
            else if (theSin == 0)
            {
                validSin = true;
            }
            else if(sin[0] == 8)
            {
                employeeEx.AddError("SIN Error: Cannot start with 8.");
                //AddToLogString("\tSIN Error: Cannot start with 8.");
            }
            return validSin;
        }

        /// <summary>
        /// Validate date to see if its validate base on the format yyyy/mm/dd
        /// </summary>
        /// <param name="date"></param>
        /// <returns>valid</returns>
        protected bool ValidateDate(string date)
        {
            bool valid = false; //!< bool to tell if the date was validate or not
            CultureInfo culture; //!< setting up calture
            culture = CultureInfo.CreateSpecificCulture("en-US"); //!< Calture format
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd", "yyyy/M/d", "yyyy/MM/d" }; //!< All of the date format 
            DateTime dateValue; //!< datevalue

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                if (dateValue <= DateTime.Now)
                {
                    valid = true;
                }
                else
                {
                    employeeEx.AddError("Date of Birth Error: Cannot be in the future.");
                    //AddToLogString("\tDate of Birth Error: Cannot be in the future.");
                }
            }
            else
            {
                employeeEx.AddError("\tDate of Birth Error: Invalid date format. Tried: " + dateValue.ToString());
                //AddToLogString("\tDate of Birth Error: Invalid date format.");
            }
            return valid;
        }

        /// <summary>
        /// Used to check if date is before beforeDate
        /// </summary>
        /// <param name="date"></param>
        /// <param name="beforeDate"></param>
        /// <returns>valid</returns>
        static public bool ValidateDate(string date, DateTime beforeDate)
        {
            bool valid = false; //!< vool to see if date is validate or not
            CultureInfo culture; //!< setting up calture
            culture = CultureInfo.CreateSpecificCulture("en-US"); //!< Calture format
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd", "yyyy/M/d", "yyyy/MM/d" };
            DateTime dateValue; //!< datevalue

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                if (dateValue <= beforeDate)
                {
                    valid = true;
                }
            }
            return valid;
        }

        /// <summary>
        /// Validate money for basic rules for greater then 0
        /// </summary>
        /// <param name="money"></param>
        /// <returns>valid</returns>
        protected bool ValidateMoney(Decimal money)
        {
            bool valid = false; //!< bool if the money is validate or not 
            if (money >= 0)
            {
                valid = true;
            }
            else
            {
                employeeEx.AddError("Money Value Error: Money values must be 0 or greater. Tried: " + money);
                //AddToLogString("\tMoney Value Error: Money values must be 0 or greater.\n||\t\tTried: " + money);
            }
            return valid;
        }
    }
}
