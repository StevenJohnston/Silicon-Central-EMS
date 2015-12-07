using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllEmployees;
using Supporting;
using System.IO;

namespace TheCompany
{
    /// <summary>
    /// Struct definition for Message. Error message that contains the number code of the error and the 
    /// accomodated error message that follows that error code's value (number)
    /// </summary>
    public struct Message
    {
        public int code; //!< The associated error number
        public string message; //!< The associated message that appears based on the error number 
    }


    /// <summary>
    ///  Primary purpose is to serve as a temporary placeholder for all of the record's information that will be extracted from the flat file database and used for viewing
    /// and editing purposes. 
    /// </summary>
    public class EmployeeDirectory
    {
        //key: sin
        Dictionary<string, Employee> employees = new Dictionary<string, Employee>(); //!< Generic container used to keep track of the employee's information along with a unique identifier specifying that specific employee
        FileIO file = new FileIO(); //!< The object instantiated of class FileIO which performs all file-related handlings and functionalities

        /// <summary>
        /// Loads all of the records that are found within the flat file database
        /// </summary>
        public void Load()
        {
            file.Load(Add);
        }


        /// <summary>
        /// Add a new employee type, given a set of information the method will decide which employee type is requested to be created and to provide the 
        /// information in creating that specific employee type 
        /// </summary>
        /// <param name="record">Contains a set of information regarding which employee type to create and what information it should contain</param>
        /// <returns></returns>
        public Supporting.Message Add(object record)
        {
            Supporting.Message returnMessage;
            returnMessage.code = 200;
            string[] recordStr = ((string)record).Split('|');
            Employee newEmployee = null;
            switch (recordStr[0])
            {
                case "FT":
                    newEmployee = new FulltimeEmployee(recordStr);
                    break;
                case "PT":
                    newEmployee = new ParttimeEmployee(recordStr); //PARAM Needs to be changed, temp fix
                    break;
                case "CT":
                    newEmployee = new ContractEmployee(recordStr); //PARAM Needs to be changed, temp fix
                    break;
                case "SN":
                    newEmployee = new SeasonalEmployee(recordStr); //PARAM Needs to be changed, temp fix
                    break;
                default:
                    break;
            }
            if (newEmployee != null)
            {
                if (newEmployee.IsValid)
                {
                    employees.Add(newEmployee.SocialInsuranceNumber, newEmployee);
                }
                else
                {
                    //Employee is not valid dont add Needs LOG
                }
            }
            return new Supporting.Message();
        }


        /// <summary>
        /// Save option that calls on the SaveAll method which internally takes all of the record's information and loads back into the flat file for permanent storage
        /// </summary>
        public void Save()
        {
            file.Save(SaveAll);
        }


        /// <summary>
        /// Save all of the record's information and load back into the flat file for permanent storage
        /// </summary>
        /// <param name="fileOut">The file name in which the information should be written out to using the StreamWriter class stream</param>
        /// <returns></returns>
        public Supporting.Message SaveAll(StreamWriter fileOut)
        {
            Supporting.Message returnMessage = new Supporting.Message();
            returnMessage.code = 200;
            foreach (var employee in employees)
            {
                fileOut.WriteLine(employee.Value.ToString());
            }
            return returnMessage;
        }


        /// <summary>
        /// Shows all of the record's information that was extracted from the database and stored in the Dictonary container
        /// </summary>
        /// <returns>List<string> - Returns a List containing element of type string, that contain the record's (employee's) information</returns>
        public List<string> ShowAll()
        {
            List<string> employeeStrings = new List<string>();
            foreach (KeyValuePair<string, Employee> employee in employees)
            {
                employeeStrings.Add(employee.Value.ToString());
            }
            return employeeStrings;
        }


        /// <summary>
        /// Remove an employee by specifying their SIN number as the argument 
        /// </summary>
        /// <param name="employeeSin">Based on this string value, the employee's SIN number will be removed if detected, if not then a notfying message will be displayed of the failure in removal</param>
        /// <returns>Returns a Message struct the outlines if the process failed by providing an error number and the accomodated error message that follows</returns>
        public Message RemoveBySin(string employeeSin)
        {
            Message returnMessage = new Message();
            returnMessage.code = 200;

            Employee removeEmployee = employees[employeeSin];
            if (removeEmployee == null)
            {
                returnMessage.code = 100;
                returnMessage.message = "That employee does not exist";
            }
            else
            {
                employees.Remove(employeeSin);
                returnMessage.message = "Employee Removed";
            }
            return returnMessage;
        }


        /// <summary>
        /// Finds if a particular employee exists within the database, which is queried based on a inputted employee SIN number
        /// </summary>
        /// <param name="employeeSin">Finds and compares if the provided SIN number as the argument exists within the Employee database</param>
        /// <returns>Returns a Message struct the outlines if the process failed by providing an error number and the accomodated error message that follows</returns>
        public Message ExistBySin(string employeeSin)
        {
            Message returnMessage = new Message();
            returnMessage.code = 200;


            if (employees.ContainsKey(employeeSin))
            {
                Employee removeEmployee = employees[employeeSin];
                returnMessage.message = removeEmployee.ToString();
            }
            else
            {
                returnMessage.code = 100;
                returnMessage.message = "That employee does not exist";
            }
            return returnMessage;
        }
    }
}
