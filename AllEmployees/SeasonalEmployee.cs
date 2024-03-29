﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllEmployees
{
    /// <summary>
    /// This represents the SeasonalEmployee Class which is the parent class which all other employee 
    /// will inherit from
    /// </summary>
    public class SeasonalEmployee : Employee
    {
        /*public enum seasons
        {
            winter,
            spring,
            summer,
            fall,
        }
        seasons season { get; set; }*/
        string[] myEmployeeData;
        public string season; /// What season
        public Decimal piecePay { get; set; } 

        public bool VariablesLogString(string[] employeeData)
        {
            bool success = true; //!< status of the sucess on logging
            int index = employeeData[0] == "SN" ? 1 : 0;
            string toLog = "Trying to create Seasonal Employee with:\n||\tFirst Name: " + employeeData[index] +
                        "||Last Name: " + employeeData[index + 1] +
                        "||SIN: " + employeeData[index + 2] +
                        "||Date of Birth: " + employeeData[index + 3] +
                        "||Season: " + employeeData[index + 4] +
                        "||Piece Pay: " + employeeData[index + 5];
            AddToLogString(toLog);
            return success;
        }
        /// <summary>
        /// Loging the string when season Employee is sucessful 
        /// </summary>
        /// <returns></returns>
        public bool SuccessLogString()
        {
            bool success = true; //!< bool that tell if it was valid or not
            if (IsValid)
            {
                AddToLogString("\t-->Creating Seasonal Employee was successful.");
            }
            else
            {
                success = false;
                AddToLogString("\t-->Creating Seasonal Employee failed.");
            }
            Supporting.Logging.LogString(logString);
            return success;
        }
        /// <summary>
        /// Seasonal Employee 
        /// </summary>
        public SeasonalEmployee()
        {

        }


        public bool Validate()
        {
            int index = myEmployeeData[0] == "SN" ? 1 : 0;
            bool status = ValidateAndSetSeasonal(myEmployeeData[index], myEmployeeData[index + 1], myEmployeeData[index + 2], myEmployeeData[index + 3], myEmployeeData[index + 4], Convert.ToDecimal(myEmployeeData[index + 5]));
            return status;
        }
        /// <summary>
        /// Constructor that validate employeeData
        /// </summary>
        public SeasonalEmployee(string[] employeeData)
        {
            myEmployeeData = employeeData;
            int index = employeeData[0] == "SN" ? 1 : 0;
            VariablesLogString(employeeData);
            employeeEx.employeeType = "Seasonal";
            employeeEx.operationType = "CREATE";

            if (ValidateAndSetSeasonal(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], Convert.ToDecimal(employeeData[index + 5])))
            {
                /*FirstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                season = employeeData[index + 4];
                piecePay = Convert.ToDecimal(employeeData[index + 5]);*/

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
        /// <summary>
        /// Constructor that validate name, last name, SIN, DOB, Season, piecePay
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="socialInsuranceNumber"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="season"></param>
        /// <param name="piecePay"></param>
        /// <returns></returns>
        private bool ValidateAndSetSeasonal(string name, string lastName, string socialInsuranceNumber, string dateOfBirth, string season, decimal piecePay)
        {
            bool[] valid = new bool[3] { false, false, false };
            bool allValid = true;

            allValid = ValidateAndSetEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            if (ValidateSeason(season))
            {
                this.season = season;
            }
            if (ValidateMoney(piecePay))
            {
                this.piecePay = piecePay;
            }
            return allValid;
        }
        /// <summary>
        /// Validate the season 
        /// </summary>
        /// <param name="newSeason"></param>
        /// <returns></returns>
        public bool ValidateSeason(string newSeason)
        {
            bool valid = false; //!<Season valid or not

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
            else
            {
                //AddToLogString("\tSeason Error: Invalid season.\n\t\tTried: " + newSeason);
                employeeEx.AddError("Season Error: Invalid season. Tried: " + newSeason);
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
            returnString += "SN|" + FirstName + "|" + LastName + "|";
            returnString += socialInsuranceNumber.Substring(0, 3) + " " + socialInsuranceNumber.Substring(3, 3) + " " + socialInsuranceNumber.Substring(6, 3);
            returnString += "|" + dateOfBirth.Value.ToString("yyyy/MM/dd");
            returnString += "|";
            returnString+= season==null||season==""?"None":season;
            returnString += "|" + piecePay;
            return returnString;
        }
    }
}
