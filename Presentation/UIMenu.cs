using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheCompany;
using EMSExceptions;
namespace Presentation
{
    /// <summary>
    /// Main outline of the user-friendly menu interface where clients will be provided with several options in 
    /// viewing/accessing viewing employee history/records database and simply viewing/inserting employee-related information
    /// </summary>
    public class UIMenu
    {


        public delegate nextFunction nextFunction(); //!< Delegate that serves reference to the specific inner UI the user chose to access

        EmployeeDirectory employeeDirectory = new EmployeeDirectory(); //!< Object of type EmployeeDirectory that is used to serve as a placeholder to all of the information being extracted and altered from the flat file database

        bool newEmployee = false; //!< Flag status indicating if the following information is of a new employee or one that already exists

        public delegate bool validator(object obj);
        /// <summary>
        /// Initiliazes which part of the menu is first being appeared to the user (At first launch) and if
        /// accessed multiple times after initial launch it will clear out old parts of the menus by clearing them out of the console
        /// </summary>
        public void Launch()
        {
            nextFunction lastFunc = null;
            nextFunction nextFunc = MainMenu;
            for (;;)
            {
                lastFunc = nextFunc;
                nextFunc = nextFunc();
                if (nextFunc == null)
                {
                    break;
                }
                else if (nextFunc != lastFunc)
                {
                    Console.Clear();
                }
            }
        }
        /// <summary>
        /// Launch of the home page of the main menu, then after user decides which portion to access the 
        /// method will return the next section of the menu that will be accessed by returning a reference
        /// to the nextFunction delegate 
        /// </summary>
        /// <returns>Due to the delegate created, the return will specify the reference to the next
        /// portion of the menu that the user has requested to access</returns>
        public nextFunction MainMenu()
        {
            string menuChoice;
            nextFunction next = null;
            Console.WriteLine("Menu 1 : MAIN MENU");
            Console.WriteLine("1. Manage EMS DBase files");
            Console.WriteLine("2. Manage Employees");
            Console.WriteLine("9. Quit");
            menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    next = FileManagementMenu;
                    break;
                case "2":
                    next = EmployeeManagementMenu;
                    break;
                case "9":
                    next = null;
                    break;
                default:
                    next = MainMenu;
                    Console.Clear();
                    Console.WriteLine("**Invalid input. Enter values 1, 2, or 9**\n");
                    break;
            }
            return next;
        }


        /// <summary>
        /// Portion of the menu that outlines how the user may interact with the database, for instance
        /// loading records from the database and or saving records back to the database for permanent storage
        /// </summary>
        /// <returns>Due to the delegate created, the return will specify the reference to the next
        /// portion of the menu that the user has requested to access</returns>
        public nextFunction FileManagementMenu()
        {
            string menuChoice;
            nextFunction next = null;
            Console.WriteLine("Menu 2 : FILE MANAGEMENT MENU");
            Console.WriteLine("1. Load EMS DBase from file");
            Console.WriteLine("2. Save Employee Set to EMS DBase file");
            Console.WriteLine("9. Return to Main Menu");
            menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    Console.Clear();
                    try {
                        employeeDirectory.Load();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message); 
                    }
                    next = FileManagementMenu;
                    Console.WriteLine("**Database Loaded**");
                    break;
                case "2":
                    Console.Clear();
                    try
                    {
                        employeeDirectory.Save();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    next = FileManagementMenu;
                    Console.WriteLine("**Database Saved**");
                    break;
                case "9":
                    next = MainMenu;
                    break;
                default:
                    next = FileManagementMenu;
                    Console.Clear();
                    Console.WriteLine("**Invalid input. Enter values 1, 2, or 9**\n");
                    break;
            }
            return next;
        }

        /// <summary>
        /// Portion of the menu solely based on providing the user with information that describes their options with 
        /// interacting with the several employee records found in the database, like the ability to view employees, modify,
        /// remove and even create a new one. 
        /// </summary>
        /// <returns>Due to the delegate created, the return will specify the reference to the next
        /// portion of the menu that the user has requested to access</returns>
        public nextFunction EmployeeManagementMenu()
        {
            string menuChoice;
            nextFunction next = null;
            Console.WriteLine("Menu 3 : EMPLOYEE MANAGEMENT MENU");
            Console.WriteLine("1. Display Employee Set");
            Console.WriteLine("2. Create a NEW Employee");
            Console.WriteLine("3. Modify an EXISTING Employee");
            Console.WriteLine("4. Remove an EXISTING Employee");
            Console.WriteLine("9. Return to Main Menu");
            menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    Console.Clear();
                    List<string> employeeStrings = employeeDirectory.ShowAll();
                    foreach (string employee in employeeStrings)
                    {
                        Console.WriteLine(employee);
                    }
                    next = EmployeeManagementMenu;
                    Console.WriteLine("Press Any Key To Continue...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "2":
                    Console.Clear();
                    newEmployee = true;
                    next = EmployeeDetailsMenu;
                    break;
                case "3":
                    Console.Clear();
                    newEmployee = false;
                    next = EmployeeDetailsMenu;
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Enter SIN of employees to remove");
                    string employeeSin = Console.ReadLine();
                    try {
                        string employeeInfos = employeeDirectory.GetEmployeeInfosBySin(employeeSin);
                        Console.WriteLine("Employee Found:");
                        Console.WriteLine(employeeInfos);
                        string lineIn = "";
                        for (; lineIn != "y" && lineIn != "n"; lineIn = Console.ReadLine())
                        {
                            Console.WriteLine("Are you sure you want to remove? y:n");
                        }
                        if (lineIn == "y")
                        {
                            try
                            {
                                employeeDirectory.RemoveBySin(employeeSin);
                                Console.WriteLine("Employee Removed");
                            }
                            catch (MissingMemberException mME)
                            {
                                Console.WriteLine(mME.Message);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Employee Not Removed");
                        }
                        Console.WriteLine("Press Any Key To Continue...");
                        Console.ReadKey();
                    } catch (MissingMemberException mME)
                    {
                        Console.WriteLine(mME.Message);
                    }
                    

                    next = EmployeeManagementMenu;
                    break;
                case "9":
                    next = MainMenu;
                    break;
                default:
                    next = FileManagementMenu;
                    Console.Clear();
                    Console.WriteLine("**Invalid input. Enter values 1, 2, 4, or 9**\n");
                    break;
            }
            return next;
        }
        /// <summary>
        /// Portion of the menu detailing options for the user to do with the Employee portion, like finding
        /// a specific employee and or managing the employee records entirely 
        /// </summary>
        /// <returns>Due to the delegate created, the return will specify the reference to the next
        /// portion of the menu that the user has requested to access</returns>
        public nextFunction EmployeeDetailsMenu()
        {
            string menuChoice;
            string[] employeeInfo = new string[10];
            nextFunction next = null;
            Console.WriteLine("Menu 4 : EMPLOYEE DETAILS MENU ");
            Console.WriteLine("1. Specify Base Employee Details");
            Console.WriteLine("2. Manage Employees");
            Console.WriteLine("9. Return to Employee Management Menu");
            menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    next = EmployeeDetailsMenu;
                    employeeInfo[0] = InputTillCorrect(new Regex(@"^(?i)(FT|PT|CT|SS|Full Time|Part Time|Contract|Seasonal)(?-i)$"), "Enter Employee Type", "Employee Types consist of (FT,PT,CT,SS,)");
                    switch (employeeInfo[0].ToLower())
                    {
                        case "ft":
                        case "full time":
                            GetFullTimeInfo().CopyTo(employeeInfo, 1);
                            break;
                        case "pt":
                        case "part time":
                            GetPartTimeInfo().CopyTo(employeeInfo, 1);
                            break;
                        case "ct":
                        case "contract":
                            GetContractEmployee().CopyTo(employeeInfo,1);
                            break;
                        case "ss":
                        case "seasonal":
                            GetSeasonalEmployee().CopyTo(employeeInfo, 1);
                            break;
                    }
                    try
                    {
                        employeeDirectory.Add(concatEmployee(employeeInfo));
                    }
                    catch (EmployeeException eE)
                    {
                        Console.WriteLine(eE.errorList);
                        eE.errorList.ForEach(x=>Console.WriteLine(x));
                        //Console.WriteLine(eE.errorList);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "2":
                    break;
                case "9":
                    next = MainMenu;
                    break;
            }
            return next;
        }
        public string[] GetFullTimeInfo()
        {
            string[] additionalInfo = new string[7];

            additionalInfo[0] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee Last Name", "Employee Last Name consist of only characters");
            additionalInfo[1] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee First Name", "Employee First Name consist of only characters");
            additionalInfo[2] = InputTillCorrect(new Regex(@"^\d{3} ?\d{3} ?\d{3}$"), "Enter Employee SIN", "Employee SIN should be formatted like so: ### ### ###");
            additionalInfo[3] = InputTillCorrect(ValidateDate, "Enter Date Of Birth", "That date is not valid");
            additionalInfo[4] = InputTillCorrect(ValidateDate, "Enter Date Of Hire", "That date is not valid");
            additionalInfo[5] = InputTillCorrect(ValidateDate, "Enter Date Of Termination", "That date is not valid");
            additionalInfo[6] = InputTillCorrect(new Regex(@"^\d+(.\d{1,2})?$"), "Enter Employee Salary", "That is not a valid salary");

            return additionalInfo;
        }
        public string[] GetPartTimeInfo()
        {
            string[] AdditionalInfo = new string[7];

            AdditionalInfo[0] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee Last Name", "Employee Last Name consist of only characters");
            AdditionalInfo[1] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee First Name", "Employee First Name consist of only characters");
            AdditionalInfo[2] = InputTillCorrect(new Regex(@"^\d{3} ?\d{3} ?\d{3}$"), "Enter Employee SIN", "Employee SIN should be formatted like so: ### ### ###");
            AdditionalInfo[3] = InputTillCorrect(ValidateDate, "Enter Date Of Birth", "That date is not valid");

            AdditionalInfo[4] = InputTillCorrect(ValidateDate, "Enter Date Of Hire", "That date is not valid");
            AdditionalInfo[5] = InputTillCorrect(ValidateDate, "Enter Date Of Termination", "That date is not valid");
            AdditionalInfo[6] = InputTillCorrect(new Regex(@"^\d+(.\d{1,2})?$"), "Enter Employee Hourly Rate", "Hourly rate must be positive and a number");

            return AdditionalInfo;
        }
        public string[] GetContractEmployee()
        {
            string[] AdditionalInfo = new string[7];

            AdditionalInfo[0] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee Last Name", "Employee Last Name consist of only characters");
            AdditionalInfo[1] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee First Name", "Employee First Name consist of only characters");
            AdditionalInfo[2] = InputTillCorrect(new Regex(@"^\d{5} ?\d{4}$"), "Enter Business Number", "Employee BN, Should be formatted like so: ##### ####");
            AdditionalInfo[3] = InputTillCorrect(ValidateDate, "Enter Date Of Incorporation", "That date is not valid");

            AdditionalInfo[4] = InputTillCorrect(ValidateDate, "Enter Contract Start Date", "That date is not valid");
            AdditionalInfo[5] = InputTillCorrect(ValidateDate, "Enter Contract Stop Date", "That date is not valid");
            AdditionalInfo[6] = InputTillCorrect(new Regex(@"^\d+(.\d{1,2})?$"), "Enter Fixed Contract Amount", "Fixed Contract Amount must be a positive number");

            return AdditionalInfo;
        }
        
        public string[] GetSeasonalEmployee()
        {
            string[] additionalInfo = new string[6];

            additionalInfo[0] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee Last Name", "Employee Last Name consist of only characters");
            additionalInfo[1] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee First Name", "Employee First Name consist of only characters");
            additionalInfo[2] = InputTillCorrect(new Regex(@"^\d{3} ?\d{3} ?\d{3}$"), "Enter Employee SIN", "Employee SIN should be formatted like so: ### ### ###");
            additionalInfo[3] = InputTillCorrect(ValidateDate, "Enter Date Of Birth", "That date is not valid");
            additionalInfo[4] = InputTillCorrect(new Regex(@"Winter|Spring|Summer|Fall"), "Enter Season (Winter, Spring, Summer, or Fall)", "Invalid Season try (Winter, Spring, Summer, or Fall)");
            additionalInfo[5] = InputTillCorrect(new Regex(@"^\d+(.\d{1,2})?$"), "Enter Employee Piece Pay", "That is not a valid Piece Pay");

            return additionalInfo;
        }
        /// <summary>
        /// Validating method used to perform error checks and validation to the information that the user inputs
        /// and will outline any error-related messages out to the console if one was found         /// </summary>
        /// <param name="regex"></param>
        /// <param name="message"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public string InputTillCorrect(Regex regex,string message, string errorMessage)
        {
            string lineIn = null;
            Console.Clear();
            for (; lineIn == null || !regex.IsMatch(lineIn); )
            {
                Console.WriteLine(message);
                lineIn = Console.ReadLine();
                Console.Clear();
                Console.WriteLine(errorMessage);
            }
            Console.Clear();
            return lineIn;
        }
        /// <summary>
        /// Validating method used to perform error checks and validation to the information that the user inputs
        /// and will outline any error-related messages out to the console if one was found 
        /// </summary>
        /// <param name="regex">The regex expression used for testing the validity of the string</param>
        /// <param name="message">Message pertaining to what is asked from the user and what information is being requested by the program</param>
        /// <param name="errorMessage">Message outlining the error involved with the inputted answer</param>
        /// <returns>Returns the string of information read from the console</returns>
        public string InputTillCorrect(validator validatorMethod,string message,string errorMessage)
        {
            string lineIn = null;
            for (; lineIn==null || !validatorMethod(lineIn); )
            {
                Console.WriteLine(message);
                lineIn = Console.ReadLine();
                Console.Clear();
                Console.WriteLine(errorMessage);
            }
            Console.Clear();
            return lineIn;
        }
        /// <summary>
        /// Used to check if date is before beforeDate
        /// </summary>
        /// <param name="date"></param>
        /// <param name="beforeDate"></param>
        /// <returns>valid</returns>
        private bool ValidateDate(object dateIn)
        {
            string date = dateIn as string;
            bool valid = false; //!< vool to see if date is validate or not
            CultureInfo culture; //!< setting up calture
            culture = CultureInfo.CreateSpecificCulture("en-US"); //!< Calture format
            string[] formats = { "yyyy/MM/dd", "yyyy/M/dd", "yyyy/M/d", "yyyy/MM/d" };
            DateTime dateValue; //!< datevalue

            if (DateTime.TryParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
            {
                valid = true;
            }
            return valid;
        }
        /// <summary>
        /// Validate SIN
        /// </summary>
        /// <param name="sin"></param>
        /// <returns></returns>
        public bool ValidateSin(object sin)
        {
            bool validSin = true;
            string sinString = (string)sin;
            return validSin;
        }
        public string concatEmployee(string[] employeeInfo)
        {
            return string.Join("|",employeeInfo);
        }
    }
}
