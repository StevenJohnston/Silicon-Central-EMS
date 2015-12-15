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
        public DateTime? dateOfHire;  //!< date of hire
        public DateTime? dateOfTermination;  //!< date of termination
        public decimal hourlyRate;  //!< hourly pay
        string[] myEmployeeData;
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

        public bool Validate()
        {
            int index = myEmployeeData[0] == "PT" ? 1 : 0;
            bool status = ValidateAndSetParttime(myEmployeeData[index], myEmployeeData[index + 1], myEmployeeData[index + 2], myEmployeeData[index + 3], myEmployeeData[index + 4], myEmployeeData[index + 5], Convert.ToDecimal(myEmployeeData[index + 6]));
            return status;
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
                success = false;
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
            myEmployeeData = employeeData;
            int index = employeeData[0] == "PT" ? 1 : 0;
            VariablesLogString(employeeData);
            employeeEx.employeeType = "Part Time";
            employeeEx.operationType = "CREATE";
            if (ValidateAndSetParttime(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], employeeData[index + 5], Convert.ToDecimal(employeeData[index+6])))
            {
                /*FirstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                dateOfHire = Convert.ToDateTime(employeeData[index + 4]);
                dateOfTermination = Convert.ToDateTime(employeeData[index + 5]);
                hourlyRate = Convert.ToDecimal(employeeData[index + 6]);*/
                IsValid = true;
                SuccessLogString();
            }
            else
            {
                IsValid = false;
                SuccessLogString();
                throw employeeEx;
            }
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
        private bool ValidateAndSetParttime(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string dateOfHire, string dateOfTermination, decimal hourlyRate)
        {
            bool allValid = true; //!< validate bool
            bool[] valid = new bool[5]; //!<list of bool to see if it was validate or not
            valid[0] = ValidateAndSetEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            /*if (valid[0])
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
            }
            else
            {
                allValid = false;
            }*/
            if (ValidateDate(dateOfHire, dateType.HIRE))
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
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
            if (ValidateMoney(hourlyRate))
            {
                this.hourlyRate = hourlyRate;
            }
            else
            {
                allValid = false;
            }
            return allValid;
        }
        /// <summary>
        /// Validate date for parttime employees
        /// </summary>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns>valid</returns>
        public bool ValidateDate(string date, dateType type)
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

                        dateOfHire = dateValue;
                        if (dateValue >= dateOfBirth && dateValue <= DateTime.Now)
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
                                employeeEx.AddError("\tDate of Termination Error: Must be after the employee was born.");
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
                    employeeEx.AddError("\tDate of Hire Error: Invalid format. Tried: " + date);
                    //AddToLogString("\tDate of Hire Error: Invalid format.\n||\t\tTried: " + date);
                }
                else if (type == dateType.TERMINATE)
                {
                    employeeEx.AddError("\tDate of Termination Error: Invalid format. Tried: " + date);
                    //AddToLogString("\tDate of Termination Error: Invalid format.\n||\t\tTried: " + date);
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
            returnString += "PT|" + FirstName + "|" + LastName + "|";
            returnString += socialInsuranceNumber.Substring(0, 3) + " " + socialInsuranceNumber.Substring(3, 3) + " " + socialInsuranceNumber.Substring(6, 3);
            returnString += "|" + dateOfBirth.Value.ToString("yyyy/MM/dd");
            returnString += "|" + dateOfHire.Value.ToString("yyyy/MM/dd");
            returnString += "|";
            if (dateOfTermination != null)
            {
                returnString += dateOfTermination.Value.ToString("yyyy/MM/dd");
            }
            returnString += "|" + hourlyRate;
            return returnString;
        }
    }
}
