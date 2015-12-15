using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SO HERES MY 
//Edited file
//Different comment
namespace AllEmployees
{
    /// <summary>
    /// This represents the Contract Employee Class which is the child class of Employee
    /// </summary>
    public class ContractEmployee : Employee
    {
        string[] myEmployeeData;
        public DateTime? contractStartDate; //!< DateTime when contract start
        public DateTime? contractStopDate; //!< DateTime when contract end
        public decimal fixedContractAmount; //!< Contract Length
        /// <summary>
        /// Log when trying to create an employee
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>
        public bool VariablesLogString(string[] employeeData)
        {
            bool success = true; ///< bool which gets returned
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
            bool success = true; ///< bool which gets return indicating if it was valid
            if (IsValid)
            {
                AddToLogString("\t-->Creating Contract Employee was successful.");
            }
            else
            {
                success = false;
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

        new public bool Validate()
        {
            int index = myEmployeeData[0] == "CT" ? 1 : 0;
            return ValidateContract(myEmployeeData[index], myEmployeeData[index + 1], myEmployeeData[index + 2], myEmployeeData[index + 3], myEmployeeData[index + 4], myEmployeeData[index + 5], Convert.ToDecimal(myEmployeeData[index + 6]));
        }


        /// <summary>
        /// Constructor that validate employees and sets them
        /// </summary>
        /// <param name="employeeData"></param>
        public ContractEmployee(string[] employeeData)
        {
            int index = employeeData[0] == "CT" ? 1 : 0;
            employeeEx.employeeType = "Contract";
            employeeEx.operationType = "CREATE";
            myEmployeeData = employeeData;
            VariablesLogString(employeeData);
            if (ValidateContract(employeeData[index], employeeData[index + 1], employeeData[index + 2], employeeData[index + 3], employeeData[index + 4], employeeData[index + 5], Convert.ToDecimal(employeeData[index + 6])))
            {
                /*FirstName = employeeData[index];
                lastName = employeeData[index + 1];
                socialInsuranceNumber = employeeData[index + 2];
                dateOfBirth = Convert.ToDateTime(employeeData[index + 3]);
                contractStartDate = Convert.ToDateTime(employeeData[index + 4]);
                contractStopDate = Convert.ToDateTime(employeeData[index + 5]);
                fixedContractAmount = Convert.ToDecimal(employeeData[index + 6]);*/
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
            
            bool allValid = true; //!< bool which gets return indicating if it was valid
            bool[] valid = new bool[5]; //!< bool array
            allValid = ValidateAndSetEmployee(name, lastName, businessNumber, dateOfBirth);
            /*if (valid[0])
            {
                this.dateOfBirth = Convert.ToDateTime(dateOfBirth);
                valid[4] = ValidateBusinessNumber(businessNumber);
            }*/
            /*if (ValidateBusinessNumber(businessNumber))
            {
                this.socialInsuranceNumber = businessNumber;
            }*/
            if (ValidateDate(contractStartDate, dateType.CONTRACT_START))
            {
                this.contractStartDate = Convert.ToDateTime(contractStartDate);
            }
            else
            {
                allValid = false;
            }
            if (ValidateDate(contractStopDate, dateType.CONTRACT_END))
            {
                this.contractStopDate = Convert.ToDateTime(contractStopDate);
            }
            else
            {
                allValid = false;
            }

            if (ValidateMoney(fixedContractAmount))
            {
                this.fixedContractAmount = fixedContractAmount;
            }
            else
            {
                allValid = false;
            }
            return allValid;
        }


        protected override bool ValidateSIN(string businessNumber)
        {
            int newSin = 0; //!< SIN
            try
            {
                Int32.TryParse(businessNumber.Replace(" ", string.Empty), out newSin);
            }
            catch (FormatException e)
            {
                employeeEx.AddError("\tBusiness Number Error: " + e.Message + " Tried: " + businessNumber);
                //AddToLogString("Business Number Error: " + e.Message + " \n||\t\tTried: " + businessNumber);
            }

            int[] tempSin = new int[9]; ///< int array
            int[] sin = new int[9]; ///< int array
            int[] year = new int[4]; ///< int array

            if (dateOfBirth == null)
            {
                dateOfBirth = new DateTime(Convert.ToInt16("19" + businessNumber.Substring(0, 2)), 1, 1);
            }
            int tempYear = dateOfBirth.Value.Year;

            double totalSin = 0;   ///<totalSin
            bool validSin = false; ///< bool which gets return indicating if it was valid
            int toAdd = 0; //!< Amount to add
            int theSin = newSin; //!< assign theSin to newSin
            for (int x = 8; x >= 0; x--) //splits the SIN into an array
            {
                sin[x] = newSin % 10;
                newSin /= 10;
            }
            for (int x = 3; x >= 0; x--)
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
                    //AddToLogString("\tBusiness Number Error: Not a valid business number");
                    employeeEx.AddError("\tBusiness Number Error: Not a valid business number. Tried: " + businessNumber);
                }
            }
            else if (theSin == 0)
            {
                validSin = true;
            }
            else
            {
                //AddToLogString("\tBusiness Number Error: First two digits must match the company's date of incorporation.");
                employeeEx.AddError("\tBusiness Number Error: First two digits must match the company's date of incorporation.");
            }
            return validSin;
        }
        /// <summary>
        /// Validate BusinessNumber that follows the bussiness rules and formating of the information
        /// </summary>
        /// <param name="businessNumber"></param>
        /// <returns>validSin</returns>
        /*private bool ValidateBusinessNumber(string businessNumber)
        {

            int newSin = 0; //!< SIN
            try {
                Int32.TryParse(businessNumber.Replace(" ", string.Empty), out newSin);
            }
            catch(FormatException e)
            {
                employeeEx.AddError("\tBusiness Number Error: " + e.Message + " Tried: " + businessNumber);
                //AddToLogString("Business Number Error: " + e.Message + " \n||\t\tTried: " + businessNumber);
            }

            int[] tempSin = new int[9]; ///< int array
            int[] sin = new int[9]; ///< int array
            int[] year = new int[4]; ///< int array
            int tempYear = dateOfBirth.Year;
            double totalSin = 0;   ///<totalSin
            bool validSin = false; ///< bool which gets return indicating if it was valid
            int toAdd = 0; //!< Amount to add
            int theSin = newSin; //!< assign theSin to newSin
            for (int x = 8; x >= 0; x--) //splits the SIN into an array
            {
                sin[x] = newSin % 10;
                newSin /= 10;
            }
            for(int x = 3; x >= 0; x--)
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
                    //AddToLogString("\tBusiness Number Error: Not a valid business number");
                    employeeEx.AddError("\tBusiness Number Error: Not a valid business number. Tried: " + businessNumber);
                }
            }
            else if (theSin == 0)
            {
                validSin = true;
            }
            else
            {
                //AddToLogString("\tBusiness Number Error: First two digits must match the company's date of incorporation.");
                //employeeEx.AddError("Business Number Error: First two digits must match the company's date of incorporation.");
            }
            return validSin;
        
        }*/
        /// <summary>
        /// Validating all dates for the contract employees
        /// </summary>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ValidateDate(string date, dateType type)
        {
            bool valid = false; //!< validation on date
            CultureInfo culture; //!< culture format
            culture = CultureInfo.CreateSpecificCulture("en-US"); //!< en-US usa style of date
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd", "yyyy/M/d", "yyyy/MM/d" }; //!< different date format
            DateTime dateValue;

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                switch (type)
                {
                    case dateType.CONTRACT_START:

                        contractStartDate = dateValue;
                        if (dateValue >= dateOfBirth && dateValue <= DateTime.Now)
                        {
                            valid = true;
                        }
                        else
                        {
                            if (dateValue <= dateOfBirth)
                            {
                                //AddToLogString("\tContract Start Date Error: Must be after the company was created.");
                                employeeEx.AddError("\tContract Start Date Error: Must be after the company was created.");
                            }
                            else
                            {
                                //AddToLogString("\tContract Start Date Error: Must be before the current date.");
                                employeeEx.AddError("\tContract Start Date Error: Must be before the current date.");
                            }
                        }
                        break;
                    case dateType.CONTRACT_END:

                        contractStopDate = dateValue;
                        if (dateValue >= contractStartDate && dateValue <= DateTime.Now)
                        {
                            valid = true;
                        }
                        else
                        {
                            if (dateValue <= dateOfBirth)
                            {
                                //AddToLogString("\tContract Stop Date Error: Must be after the company was created.");
                                employeeEx.AddError("\tContract Stop Date Error: Must be after the company was created.");
                            }
                            else
                            {
                                //AddToLogString("\tContract Stop Date Error: Must be before the current date.");
                                employeeEx.AddError("\tContract Stop Date Error: Must be before the current date.");
                            }
                        }
                        break;
                }
            }
            else
            {
                if (type == dateType.CONTRACT_START)
                {
                    //AddToLogString("\tContract Start Date Error: Invalid format.\n||\t\tTried: " + date);
                    employeeEx.AddError("\tContract Start Date Error: Invalid format. Tried: " + date);
                }
                else if (type == dateType.CONTRACT_END)
                {
                    employeeEx.AddError("\tContract Stop Date Error: Invalid format. Tried: " + date);
                    //AddToLogString("\tContract Stop Date Error: Invalid format.\n||\t\tTried: " + date);
                }
            }
            return valid;
        }
        /// <summary>
        /// A string with the information and the delimiter
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {string returnString = "";
            returnString += "CT|" + FirstName + "|" + LastName + "|";
            returnString += socialInsuranceNumber.Substring(0, 5) + " " + socialInsuranceNumber.Substring(5, 4);
            returnString += "|" + dateOfBirth.Value.ToString("yyyy/MM/dd");
            returnString += "|" + contractStartDate.Value.ToString("yyyy/MM/dd");
            returnString += "|";
            returnString += contractStopDate.Value.ToString("yyyy/MM/dd");
            returnString += "|" + fixedContractAmount;
            return returnString;
        }
    }
}
