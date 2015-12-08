using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TheCompany;
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
                    employeeDirectory.Load();
                    next = FileManagementMenu;
                    Console.WriteLine("**Database Loaded**");
                    break;
                case "2":
                    Console.Clear();
                    employeeDirectory.Save();
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
                    Console.WriteLine("Enter SIN number of employee to remove");
                    string employeeSin = Console.ReadLine();
                    TheCompany.Message existMessage = employeeDirectory.ExistBySin(employeeSin);
                    if (existMessage.code == 200)
                    {
                        Console.WriteLine("Employee Found:");
                        Console.WriteLine(existMessage.message);
                        string lineIn = "";
                        for (; lineIn != "y" && lineIn!="n";lineIn = Console.ReadLine())
                        {
                            Console.WriteLine("Are you sure you want to remove? y:n");
                        }
                        if (lineIn == "y")
                        {
                            TheCompany.Message removeMessage = employeeDirectory.RemoveBySin(employeeSin);
                            Console.WriteLine(removeMessage.message);
                        }
                        else
                        {
                            Console.WriteLine("Employee Not Removed");
                        }
                        Console.WriteLine("Press Any Key To Continue...");
                        Console.ReadKey();
                    }
                    else if (existMessage.code == 100)
                    {
                        Console.WriteLine(existMessage.message);
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
                    //InputTillCorrect(new Regex[] { new Regex(@"^FT$"), new Regex(@"^PT$"), new Regex(@"^CT$"), new Regex(@"^SS$") },"Enter Employee Type", "Employee Types consist of (FT,PT,CT,SS,)");
                    employeeInfo[0] = InputTillCorrect(new Regex(@"^(?i)(FT|PT|CT|SS|Full Time|Part Time|Contract|Seasonal)(?-i)$"), "Enter Employee Type", "Employee Types consist of (FT,PT,CT,SS,)");
                    //employeeInfo[1] = InputTillCorrect(new Regex(@"^w+$"), "Enter Employee Last Name", "Employee Last Name consist of only characters");
                    //employeeInfo[2] = InputTillCorrect(new Regex(@"^w+$"), "Enter Employee First Name", "Employee First Name consist of only characters");

                    switch (employeeInfo[0].ToLower())
                    {
                        case "ft":
                        case "full time":
                            
                            break;
                        case "pt":
                        case "part time":
                            break;
                        case "ct":
                        case "contract":
                            break;
                        case "ss":
                        case "seasonal":
                            break;

                    }
                    switch (employeeInfo[0].ToLower())
                    {
                        case "ft":
                        case "full time":
                            break;
                        case "pt":
                        case "part time":
                            break;
                        case "ct":
                        case "contract":
                            break;
                        case "ss":
                        case "seasonal":
                            break;

                    }
                    if (employeeInfo[0].ToLower() == "ft" || employeeInfo[0].ToLower() == "full time")
                    employeeInfo[3] = InputTillCorrect(new Regex(@"^$"), "Enter Employee ", "Employee  consist of ()");
                    employeeInfo[4] = InputTillCorrect(new Regex(@"^$"), "Enter Employee ", "Employee  consist of ()");
                    employeeInfo[5] = InputTillCorrect(new Regex(@"^$"), "Enter Employee ", "Employee  consist of ()");
                    break;
                case "2":
                    break;
                case "9":
                    break;
            }
            return next;
        }
        public string[] GetFullTimeInfo()
        {
            string[] fullTimeInfo = new string[8];

            fullTimeInfo[0] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee Last Name", "Employee Last Name consist of only characters");
            fullTimeInfo[1] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee First Name", "Employee First Name consist of only characters");
            fullTimeInfo[2] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee SIN", "Employee SIN should be formatted like so: ### ### ###");
            fullTimeInfo[3] = InputTillCorrect(ValidateDate, "Enter Employee Date Of Birth", "That date is not valid");
            fullTimeInfo[4] = InputTillCorrect(ValidateDate, "Enter Employee Date Of Birth", "That date is not valid");

            //fullTimeInfo[5] = InputTillCorrect(EmployeeDirectory.isDateAfterBirth, "Enter Employee Date Of Hire", "Date must be after date of birth");
            //fullTimeInfo[6] = InputTillCorrect(EmployeeDirectory.isDateAfterHire, "Enter Date of Termination", "Date must be after date of hire");
            fullTimeInfo[7] = InputTillCorrect(new Regex(@"^\d[(.\d)]$"), "Enter Employee Salary", "That is not a valid salary");

            return fullTimeInfo;
        }

        public nextFunction FullTimeMenu()
        {
            nextFunction next = EmployeeDetailsMenu;
            string[] fullTimeInfo = new string[8];
            //Base Employee
            fullTimeInfo[0] = "FT";
            fullTimeInfo[1] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee Last Name", "Employee Last Name consist of only characters");
            fullTimeInfo[2] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee First Name", "Employee First Name consist of only characters");
            fullTimeInfo[3] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee SIN", "Employee SIN should be formatted like so: ### ### ###");
            fullTimeInfo[4] = InputTillCorrect(ValidateDate, "Enter Employee Date Of Birth", "That date is not valid");


            //fullTimeInfo[5] = InputTillCorrect(EmployeeDirectory.isDateAfterBirth, "Enter Employee Date Of Hire", "Date must be after date of birth");
            //fullTimeInfo[6] = InputTillCorrect(EmployeeDirectory.isDateAfterHire, "Enter Date of Termination", "Date must be after date of hire");
            fullTimeInfo[7] = InputTillCorrect(new Regex(@"^\d[(.\d)]$"), "Enter Employee Salary", "That is not a valid salary");
            return next;
        }
        public nextFunction PartTimeMenu()
        {
            nextFunction next = EmployeeDetailsMenu;
            string[] fullTimeInfo = new string[8];
            fullTimeInfo[0] = "FT";
            fullTimeInfo[1] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee Last Name", "Employee Last Name consist of only characters");
            fullTimeInfo[2] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee First Name", "Employee First Name consist of only characters");
            fullTimeInfo[3] = InputTillCorrect(new Regex(@"^\w+$"), "Enter Employee SIN", "Employee SIN should be formatted like so: ### ### ###");
            fullTimeInfo[4] = InputTillCorrect(ValidateDate, "Enter Employee Date Of Hire", "That date is not valid");
          //  fullTimeInfo[5] = InputTillCorrect(EmployeeDirectory.isDateAfterBirth, "Enter Employee Date Of Hire", "Date must be after date of birth");
            //fullTimeInfo[6] = InputTillCorrect(EmployeeDirectory.isDateAfterHire, "Enter Date of Termination", "Date must be after date of hire");
            fullTimeInfo[7] = InputTillCorrect(new Regex(@"^\d[(.\d)]$"), "Enter Employee Salary", "That is not a valid salary");
            return next;
        }
        /// <summary>
        /// Contract Menu
        /// </summary>
        /// <returns></returns>
        public nextFunction ContractMenu()
        {
            nextFunction next = EmployeeDetailsMenu;
            return next;
        }
        /// <summary>
        /// Seasonal Employee menu
        /// </summary>
        /// <returns></returns>
        public nextFunction SeasonalMenu()
        {
            nextFunction next = EmployeeDetailsMenu;
            return next;
        }
        /// <summary>
        /// Get full time employee information date of hire, termination and salary
        /// </summary>
        /// <returns></returns>
        public string[] getFullTimeInfo()
        {
            string[] fullTimeInfo = new string[3];
            fullTimeInfo[0] = InputTillCorrect(ValidateDate, "Enter Employee Date Of Hire","That date is not valid");
            fullTimeInfo[1] = InputTillCorrect(ValidateDate, "Enter Employee Date Of Termination", "That date is not valid");
            fullTimeInfo[2] = InputTillCorrect(new Regex(@"^\d[(.\d)]$"), "Enter Employee Salary", "That is not a valid salary");
            return fullTimeInfo;
        }
        /// <summary>
        /// Get part time info hour rate, start date and end date
        /// </summary>
        /// <returns></returns>
        public string[] getPartTimeInfo()
        {
            string[] partTimeInfo = new string[3];
            partTimeInfo[0] = InputTillCorrect(ValidateDate, "Enter Employee Date Of Hire", "That date is not valid");
            partTimeInfo[1] = InputTillCorrect(ValidateDate, "Enter Employee Date Of Termination", "That date is not valid");
            partTimeInfo[2] = InputTillCorrect(new Regex(@"^\d[(.\d)]$"), "Enter Employee Hourly Rate", "That is not a valid Hourly Rate");
            return partTimeInfo;
        }
        /// <summary>
        /// Get contract information with the start date , end date and the contract amount
        /// </summary>
        /// <returns></returns>
        public string[] getContractInfo()
        {
            string[] contractInfo = new string[3];
            contractInfo[0] = InputTillCorrect(ValidateDate, "Enter Contract Start Date", "That date is not valid");
            contractInfo[1] = InputTillCorrect(ValidateDate, "Enter Contract Stop Date", "That date is not valid");
            contractInfo[2] = InputTillCorrect(new Regex(@"^\d[(.\d)]$"), "Enter Fixed Contract Amount", "That is a not a vaild fixed contract amount");
            return contractInfo;
        }
        /// <summary>
        /// Get the season information wiht the date and end date
        /// </summary>
        /// <returns></returns>
        public string[] getSeasonalInfo()
        {
            string[] seasonalInfo = new string[3];
            seasonalInfo[0] = InputTillCorrect(new Regex(@"^$"), "Enter Contract Start Date", "That date is not valid");
            seasonalInfo[1] = InputTillCorrect(ValidateDate, "Enter Contract Stop Date", "That date is not valid");
            seasonalInfo[2] = InputTillCorrect(new Regex(@"^\d[(.\d)]$"), "Enter Fixed Contract Amount", "That is a not a vaild fixed contract amount");
            return seasonalInfo;
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
            string lineIn = "";
            //for(;(from oneRegex in regex where oneRegex.IsMatch(lineIn) == true select oneRegex).Count() == 0; lineIn = Console.ReadLine())
            for(;!regex.IsMatch(lineIn);lineIn = Console.ReadLine())
            {
                Console.Clear();
                Console.WriteLine(errorMessage);
                Console.WriteLine(message);
            }
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
            string lineIn = "";
            for (; !validatorMethod(lineIn); lineIn = Console.ReadLine())
            {
                Console.Clear();
                Console.WriteLine(errorMessage);
                Console.WriteLine(message);
            }
            return lineIn;
        }
        /// <summary>
        /// Validate Date
        /// </summary>
        /// <param name="sin"></param>
        /// <returns></returns>
        public bool ValidateDate(object sin)
        {
            bool validDate = false;
            return validDate;
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
    }
}
