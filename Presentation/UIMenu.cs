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
    public class UIMenu
    {
        public delegate nextFunction nextFunction();
        EmployeeDirectory employeeDirectory = new EmployeeDirectory();
        bool newEmployee = false;
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
                    employeeInfo[1] = InputTillCorrect(new Regex(@"^w+$"), "Enter Employee Last Name", "Employee Last Name consist of only characters");
                    employeeInfo[2] = InputTillCorrect(new Regex(@"^w+$"), "Enter Employee First Name", "Employee First Name consist of only characters");

                    if(employeeInfo[0] == "")
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
    }
}
