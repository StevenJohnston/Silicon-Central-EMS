﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    public class FulltimeEmployee : Employee
    {
        DateTime dateOfHire;
        DateTime dateOfTermination;
        decimal salary;

        public bool VariablesLogString(string[] employeeData)
        {
            bool success = true;
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

        public bool SuccessLogString()
        {
            bool success = true;
            if (IsValid)
            {
                AddToLogString("\t-->Creating Fulltime Employee was successful.");
            }
            else
            {
                AddToLogString("\t-->Creating Fulltime Employee failed.");
            }
            Supporting.Logging.LogString(logString);
            return success;
        }



        public FulltimeEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "FT" ? 1 : 0;
            VariablesLogString(employeeData);
            if (ValidateFulltime(employeeData[index], employeeData[index+1], employeeData[index+2], employeeData[index+3], employeeData[index+4], employeeData[index+5], Convert.ToDecimal(employeeData[index+6])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                dateOfHire = Convert.ToDateTime(employeeData[index + 4]);
                dateOfTermination = Convert.ToDateTime(employeeData[index + 5]);
                salary = Convert.ToDecimal(employeeData[index + 6]);
                IsValid = true;
            }
            SuccessLogString();
        }

        private bool ValidateFulltime(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string dateOfHire, string dateOfTermination, decimal salary)
        {
            bool[] valid = new bool[4] { false, false, false, false };
            bool allValid = false;

            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            if (valid[0])
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
            }
            valid[1] = ValidateDate(dateOfHire, dateType.HIRE);
            valid[2] = ValidateDate(dateOfTermination, dateType.TERMINATE);
            valid[3] = ValidateMoney(salary);
            if( valid[0] & valid[1] & valid[2] & valid[3])
            {
                allValid = true;
            }
            return allValid;
        }

        protected bool ValidateDate(string date, dateType type)
        {
            bool valid = false;
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd" };
            DateTime dateValue;

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
                else if(type == dateType.TERMINATE)
                {
                    AddToLogString("\tDate of Termination Error: Invalid format.\n||\t\tTried: " + date);
                }
            }
            return valid;
        }
        
        


        public override string ToString()
        {
            return "FT|"+firstName+"|"+lastName+"|"+SocialInsuranceNumber+"|"+dateOfBirth+"|"+dateOfHire+"|"+dateOfTermination+"|"+salary;
        }

        

    }
}
