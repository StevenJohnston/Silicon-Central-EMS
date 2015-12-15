using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    /// <summary>
    /// This represents the FulltimeEmployee Class which is the child class of Employee
    /// </summary>
    public class FulltimeEmployee : Employee
    {
        public DateTime? dateOfHire; //!< date of hire
        public DateTime? dateOfTermination; //!< date of termination 
        public decimal salary; //!< employee salary
        string[] myEmployeeData;
        /// <summary>
        /// Logging full time employee with will log last name, SIN, DAte of birht, date of hire, date of termination, salary
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns>success</returns>
        public bool VariablesLogString(string[] employeeData)
        {
            bool success = true; //!<status if recorded to log
            int index = employeeData[0] == "FT" ? 1 : 0;
            string toLog = "Trying to create Fulltime Employee with:\n||\tFirst Name: " + employeeData[index] +
                        "||Last Name: " + employeeData[index + 1] +
                        "||SIN: " + employeeData[index + 2] +
                        "||Date of Birth: " + employeeData[index + 3] +
                        "||Date of Hire: " + employeeData[index + 4] +
                        "||Date of Termination: " + employeeData[index + 5] +
                        "||Salary: " + employeeData[index + 6];
            AddToLogString(toLog);
            return success;
        }
        /// <summary>
        /// Log everytime an employee was added and of an employee fail to be added
        /// </summary>
        /// <returns>sucess</returns>

        public bool SuccessLogString()
        {
            bool success = true; //!< status in creating employee
            if (IsValid)
            {
                AddToLogString("\t-->Creating Fulltime Employee was successful.");
            }
            else
            {
                AddToLogString("\t-->Creating Fulltime Employee failed.");
                success = false;
            }
            Supporting.Logging.LogString(logString);
            return success;
        }
        
        public bool Validate()
        {
            int index = myEmployeeData[0] == "FT" ? 1 : 0;
            bool status = ValidateAndSetFulltime(myEmployeeData[index], myEmployeeData[index + 1], myEmployeeData[index + 2], myEmployeeData[index + 3], myEmployeeData[index + 4], myEmployeeData[index + 5], Convert.ToDecimal(myEmployeeData[index + 6]));
            return status;
        }
        /// <summary>
        /// A constructor that set the first, last, SIN, DOB, DOT, Salary, isValid if it pass the validation for the full timers
        /// </summary>
        /// <param name="employeeData"></param>
        public FulltimeEmployee(string[] employeeData)
        {
            myEmployeeData = employeeData; 
            int index = employeeData[0].ToLower() == "ft" ? 1 : 0;
            if (employeeData.Length - index == 7)
            {
                VariablesLogString(employeeData);
                employeeEx.employeeType = "Full Time";
                employeeEx.operationType = "CREATE";
                if (ValidateAndSetFulltime(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], employeeData[index + 5], Convert.ToDecimal(employeeData[index + 6])))
                {
                    IsValid = true;
                    SuccessLogString();
                }
                else
                {
                    IsValid = false;
                    SuccessLogString();
                    throw employeeEx;
                }
                SuccessLogString();

            }
            else
            {
                IsValid = false;
            }
        }
        /// <summary>
        /// Validate full timers 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="socialInsuranceNumber"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="dateOfHire"></param>
        /// <param name="dateOfTermination"></param>
        /// <param name="salary"></param>
        /// <returns>allValid</returns>
        private bool ValidateAndSetFulltime(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string dateOfHire, string dateOfTermination, decimal salary)
        {
            bool[] valid = new bool[4] { false, false, false, false };  //!< bool array of false
            bool allValid = true;  //!< bool of status if all information is valid

            allValid = ValidateAndSetEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            if (ValidateDate(dateOfHire, dateType.HIRE))
            {
                this.dateOfHire = Convert.ToDateTime(dateOfHire);
            }
            else
            {
                allValid = false;
            }
            if (dateOfTermination != "")
            {
                if (ValidateDate(dateOfTermination, dateType.TERMINATE))
                {
                    this.dateOfTermination = Convert.ToDateTime(dateOfTermination);
                }
                else
                {
                    allValid = false;
                }
            }
            else
            {
                this.dateOfTermination = null;
            }
            if (ValidateMoney(salary))
            {
                this.salary = salary;
            }
            else
            {
                allValid = false;
            }
            return allValid;
        }
        /// <summary>
        /// Validated date 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ValidateDate(string date, dateType type)
        {
            bool valid = false;  //!< bool if date was valid
            CultureInfo culture; //!< Culture information
            culture = CultureInfo.CreateSpecificCulture("en-US");  //!< Culture of date
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd", "yyyy/M/d", "yyyy/MM/d" };  //!< Date format
            DateTime dateValue;  //!< Date value

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                switch (type)
                {
                    case dateType.HIRE:

                        dateOfHire = dateValue;
                        if ((this.dateOfBirth==null||dateValue >= dateOfBirth )&& dateValue <= DateTime.Now)
                        {
                            valid = true;
                        }
                        else
                        {
                            if (dateValue <= dateOfBirth)
                            {
                                //AddToLogString("\tDate of Hire Error: Must be after the employee was born.");
                                employeeEx.AddError("\tDate of Hire Error: Must be after the employee was born.");
                            }
                            else
                            {
                                //AddToLogString("\tDate of Hire Error: Must be before the current date.");
                                employeeEx.AddError("\tDate of Hire Error: Must be before the current date.");
                            }
                        }
                        break;
                    case dateType.TERMINATE:

                        dateOfTermination = dateValue;
                        if (dateValue >= dateOfHire && dateValue <= DateTime.Now)
                        {
                            valid = true;
                        }
                        else
                        {
                            if (dateValue <= dateOfHire)
                            {
                                //AddToLogString("\tDate of Termination Error: Must be after the employee was born."); 
                                employeeEx.AddError("\tDate of Termination Error: Must be after the employee was hired.");
                            }
                            else
                            {
                                //AddToLogString("\tDate of Termination Error: Must be before the current date.");
                                employeeEx.AddError("\tDate of Termination Error: Must be before the current date.");
                            }
                        }
                        break;
                }
            }
            else
            {
                if (type == dateType.HIRE)
                {
                    //AddToLogString("\tDate of Hire Error: Invalid format.\n||\t\tTried: " + date);
                    employeeEx.AddError("\tDate of Hire Error: Invalid format. Tried: " + date);
                }
                else if(type == dateType.TERMINATE)
                {
                    //AddToLogString("\tDate of Termination Error: Invalid format.\n||\t\tTried: " + date);
                    employeeEx.AddError("\tDate of Termination Error: Invalid format. Tried: " + date);
                }
            }
            return valid;
        }
        /// <summary>
        /// A string with the information and the delimiter
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string returnString = "";
            returnString += "FT|" + FirstName + "|" + LastName + "|";
            returnString += socialInsuranceNumber.Substring(0,3) + " "+ socialInsuranceNumber.Substring(3,3) +  " " +socialInsuranceNumber.Substring(6, 3);
            returnString += "|" + dateOfBirth.Value.ToString("yyyy/MM/dd");
            returnString += "|" + dateOfHire.Value.ToString("yyyy/MM/dd");
            returnString += "|";
            if (dateOfTermination != null)
            {
                returnString += dateOfTermination.Value.ToString("yyyy/MM/dd");
            }
            returnString += "|"+salary;
            return returnString;
        }

        /** Access the protected methods from the Base Class through inherited class referece calls **/

        public bool FTValidateEmployee(string name, string lastName, string socialInsuranceNumber, string dateOfBirth)
        {
            return ValidateAndSetEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
        }

        public bool FTValidateName(string name)
        {
            return ValidateName(name);
        }

        public bool FTValidateSIN(string socialInsuranceNumber)
        {
            return ValidateSIN(socialInsuranceNumber);
        }

        public bool FTValidateDate(string date)
        {
            return ValidateDate(date);
        }

        public bool FTValidateMoney(Decimal money)
        {
            return ValidateMoney(money);
        }

        


    }
}
