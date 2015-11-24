using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    /// <summary>
    /// This represents the ParttimeEmployee Class which is the child class of Employee
    /// </summary>
    public class ParttimeEmployee : Employee
    {
        DateTime dateOfHire;  //!< date of hire
        DateTime dateOfTermination;  //!< date of termination
        decimal hourlyRate;  //!< hourly pay
        public bool VariablesLogString(string[] employeeData)
        {
            bool success = true; //!< bool of creating part time employee
            int index = employeeData[0] == "PT" ? 1 : 0;
            string toLog = "Trying to create Parttime Employee with:\n||\tFirst Name: " + employeeData[index] +
                        "||Last Name: " + employeeData[index + 1] +
                        "||SIN: " + employeeData[index + 2] +
                        "||Date of Birth: " + employeeData[index + 3] +
                        "||Date of Hire: " + employeeData[index + 4] +
                        "||Date of Termination: " + employeeData[index + 5] +
                        "||Hourly Rate: " + employeeData[index + 6];
            AddToLogString(toLog);
            return success;
        }
        /// <summary>
        /// log if users was crated sucessfully string
        /// </summary>
        /// <returns></returns>
        public bool SuccessLogString()
        {
            bool success = true;  //!< bool in creating employee
            if (IsValid)
            {
                AddToLogString("\t-->Creating Parttime Employee was successful.");
            }
            else
            {
                AddToLogString("\t-->Creating Parttime Employee failed.");
            }
            Supporting.Logging.LogString(logString);
            return success;
        }

        public ParttimeEmployee()
        {

        }
        /// <summary>
        /// Constructor that validate part time employee
        /// </summary>
        /// <param name="employeeData"></param>
        public ParttimeEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "PT" ? 1 : 0;
            VariablesLogString(employeeData);
            if (ValidateParttime(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], employeeData[index + 5], Convert.ToDecimal(employeeData[index+6])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                dateOfHire = Convert.ToDateTime(employeeData[index + 4]);
                dateOfTermination = Convert.ToDateTime(employeeData[index + 5]);
                hourlyRate = Convert.ToDecimal(employeeData[index + 6]);
                IsValid = true;
            }
            SuccessLogString();
        }
        /// <summary>
        /// Constructor that validate part time employees
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="socialInsuranceNumber"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="dateOfHire"></param>
        /// <param name="dateOfTermination"></param>
        /// <param name="hourlyRate"></param>
        /// <returns></returns>
        private bool ValidateParttime(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string dateOfHire, string dateOfTermination, decimal hourlyRate)
        {
            bool allValid = false; //!< validate bool
            bool[] valid = new bool[5]; //!<list of bool to see if it was validate or not
            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            if (valid[0])
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
            }
            valid[1] = ValidateDate(dateOfHire, dateType.HIRE);
            valid[2] = ValidateDate(dateOfTermination, dateType.TERMINATE);
            valid[3] = ValidateMoney(hourlyRate);
            if (valid[0] & valid[1] & valid[2] & valid[3])
            {
                allValid = true;
            }
            return allValid;
        }
        /// <summary>
        /// Validate date for parttime employees
        /// </summary>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns>valid</returns>

        protected bool ValidateDate(string date, dateType type)
        {
            bool valid = false; //!< validate date status
            CultureInfo culture; //!< Culture information
            culture = CultureInfo.CreateSpecificCulture("en-US"); //!< Culture format
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd", "yyyy/M/d", "yyyy/MM/d" }; //!< Date format
            DateTime dateValue; //!< the date it self

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                switch (type)
                {
                    case dateType.HIRE:
                        if (dateValue >= dateOfBirth && dateValue <= DateTime.Now)
                        {
                            valid = true;
                            dateOfHire = dateValue;
                        }
                        else
                        {
                            if (dateValue <= dateOfBirth)
                            {
                                AddToLogString("\tDate of Hire Error: Must be after the employee was born.");
                            }
                            else
                            {
                                AddToLogString("\tDate of Hire Error: Must be before the current date.");
                            }
                        }
                        break;
                    case dateType.TERMINATE:
                        if (dateValue >= dateOfHire && dateValue <= DateTime.Now)
                        {
                            valid = true;
                            dateOfTermination = dateValue;
                        }
                        else
                        {
                            if (dateValue <= dateOfHire)
                            {
                                AddToLogString("\tDate of Termination Error: Must be after the employee was born.");
                            }
                            else
                            {
                                AddToLogString("\tDate of Termination Error: Must be before the current date.");
                            }
                        }
                        break;
                }
            }
            else
            {
                if (type == dateType.HIRE)
                {
                    AddToLogString("\tDate of Hire Error: Invalid format.\n||\t\tTried: " + date);
                }
                else if (type == dateType.TERMINATE)
                {
                    AddToLogString("\tDate of Termination Error: Invalid format.\n||\t\tTried: " + date);
                }
            }
            return valid;
        }

    }
}
