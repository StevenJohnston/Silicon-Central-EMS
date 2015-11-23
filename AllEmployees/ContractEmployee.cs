﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AllEmployees
{
    public class ContractEmployee : Employee
    {
        DateTime contractStartDate;
        DateTime contractStopDate;
        decimal fixedContractAmount;
        /// <summary>
        /// Log when trying to create an employee
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        public bool VariablesLogString(string[] employeeData)
        {
            bool success = true;
            int index = employeeData[0] == "CT" ? 1 : 0;
            logString += "Trying to create Contract Employee with:\n||\tFirst Name: " + employeeData[index] +
                        "||Last Name: " + employeeData[index + 1] +
                        "||BN: " + employeeData[index + 2] +
                        "||Date of Incorporation: " + employeeData[index + 3] +
                        "||Contract Start: " + employeeData[index + 4] +
                        "||Contract End: " + employeeData[index + 5] +
                        "||Contract Amount: " + employeeData[index + 6];
            return success;
        }
        /// <summary>
        /// log if it was successful or not in creating contract employees
        /// </summary>
        /// <returns>sucess</returns>
        public bool SuccessLogString()
        {
            bool success = true;
            if (IsValid)
            {
                AddToLogString("\t-->Creating Contract Employee was successful.");
            }
            else
            {
                AddToLogString("\t-->Creating Contract Employee failed.");
            }
            Supporting.Logging.LogString(logString);
            return success;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ContractEmployee()
        {

        }
        /// <summary>
        /// Constructor that validate employees and sets them
        /// </summary>
        /// <param name="employeeData"></param>
        public ContractEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "CT" ? 1 : 0;
            VariablesLogString(employeeData);
            if (ValidateContract(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], employeeData[index + 5], Convert.ToDecimal(employeeData[index + 6])))
            {
                firstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                contractStartDate = Convert.ToDateTime(employeeData[index + 4]);
                contractStopDate = Convert.ToDateTime(employeeData[index + 5]);
                fixedContractAmount = Convert.ToDecimal(employeeData[index + 6]);
                IsValid = true;
            }
            SuccessLogString();
        }
        /// <summary>
        /// Validate employee contract
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="businessNumber"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="contractStartDate"></param>
        /// <param name="contractStopDate"></param>
        /// <param name="fixedContractAmount"></param>
        /// <returns></returns>
        private bool ValidateContract(string name, string lastName, string businessNumber, string dateOfBirth, string contractStartDate, string contractStopDate, decimal fixedContractAmount)
        {
            bool allValid = false;
            bool[] valid = new bool[5];
            valid[0] = ValidateEmployee(name, lastName, socialInsuranceNumber, dateOfBirth);
            if (valid[0])
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
                valid[4] = ValidateBusinessNumber(businessNumber);
            }
            
            valid[1] = ValidateDate(contractStartDate, dateType.CONTRACT_START);
            valid[2] = ValidateDate(contractStopDate, dateType.CONTRACT_END);
            valid[3] = ValidateMoney(fixedContractAmount);
            if (valid[0] & valid[1] & valid[2] & valid[3] & valid[4])
            {
                allValid = true;
            }
            return allValid;
        }
        /// <summary>
        /// Validate BusinessNumber that follows the bussiness rules and formating of the information
        /// </summary>
        /// <param name="businessNumber"></param>
        /// <returns>validSin</returns>
        private bool ValidateBusinessNumber(string businessNumber)
        {

            int newSin = 0;
            try {
                Int32.TryParse(socialInsuranceNumber.Replace(" ", string.Empty), out newSin);
            }
            catch(FormatException e)
            {
                AddToLogString("Business Number Error: " + e.Message + " \n||\t\tTried: " + businessNumber);
            }

            int[] tempSin = new int[9];
            int[] sin = new int[9];
            int[] year = new int[4];
            int tempYear = dateOfBirth.Year;
            double totalSin = 0;
            bool validSin = false;
            int toAdd = 0;
            int theSin = newSin;
            for (int x = 8; x >= 0; x--) //splits the SIN into an array
            {
                sin[x] = newSin % 10;
                newSin /= 10;
            }
            for(int x = 4; x >= 0; x--)
            {
                year[x] = tempYear % 10;
                tempYear /= 10;
            }
            if (sin[0] == year[2] && sin[1] == year[3])
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
                    AddToLogString("\tBusiness Number Error: Not a valid business number");
                }
            }
            else if (theSin == 0)
            {
                validSin = true;
            }
            else
            {
                AddToLogString("\tBusiness Number Error: First two digits must match the company's date of incorporation.");
            }
            return validSin;
        
        }
        /// <summary>
        /// Validating all dates for the contract employees
        /// </summary>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected bool ValidateDate(string date, dateType type)
        {
            bool valid = false;
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd", "yyyy/M/d", "yyyy/MM/d" };
            DateTime dateValue;

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                switch (type)
                {
                    case dateType.CONTRACT_START:
                        if (dateValue >= dateOfBirth && dateValue <= DateTime.Now)
                        {
                            valid = true;
                            contractStartDate = dateValue;
                        }
                        else
                        {
                            if (dateValue <= dateOfBirth)
                            {
                                AddToLogString("\tContract Start Date Error: Must be after the company was created.");
                            }
                            else
                            {
                                AddToLogString("\tContract Start Date Error: Must be before the current date.");
                            }
                        }
                        break;
                    case dateType.CONTRACT_END:
                        if (dateValue >= contractStartDate && dateValue <= DateTime.Now)
                        {
                            valid = true;
                            contractStopDate = dateValue;
                        }
                        else
                        {
                            if (dateValue <= dateOfBirth)
                            {
                                AddToLogString("\tContract Stop Date Error: Must be after the company was created.");
                            }
                            else
                            {
                                AddToLogString("\tContract Stop Date Error: Must be before the current date.");
                            }
                        }
                        break;
                }
            }
            else
            {
                if (type == dateType.CONTRACT_START)
                {
                    AddToLogString("\tContract Start Date Error: Invalid format.\n||\t\tTried: " + date);
                }
                else if (type == dateType.CONTRACT_END)
                {
                    AddToLogString("\tContract Stop Date Error: Invalid format.\n||\t\tTried: " + date);
                }
            }
            return valid;
        }

    }
}
