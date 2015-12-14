using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllEmployees;
using Supporting;
using System.IO;
using EMSExceptions;
namespace TheCompany
{
    /// <summary>
    ///  Primary purpose is to serve as a temporary placeholder for all of the record's information that will be extracted from the flat file database and used for viewing
    /// and editing purposes. 
    /// </summary>
    public class EmployeeDirectory
    {
        Employee currentEdit = new Employee();
        List<Employee> CurrentSearch = new List<Employee>();
        //key: sin
        List<Employee> employees = new List<Employee>(); //!< Generic container used to keep track of the employee's information along with a unique identifier specifying that specific employee
        FileIO file = new FileIO(); //!< The object instantiated of class FileIO which performs all file-related handlings and functionalities

        /// <summary>
        /// Loads all of the records that are found within the flat file database
        /// </summary>
        public void Load()
        {
            try
            {
                file.Load(Add);
            }
            catch (FileLoadException fLE)
            {
                throw fLE;
            }
            catch (MissingMemberException mME)
            {
                throw mME;
            }
            catch (EmployeeException eE)
            {
                throw eE;
            }

        }


        /// <summary>
        /// Add a new employee type, given a set of information the method will decide which employee type is requested to be created and to provide the 
        /// information in creating that specific employee type 
        /// </summary>
        /// <param name="record">Contains a set of information regarding which employee type to create and what information it should contain</param>
        /// <returns></returns>
        public void Add(object record)
        {
            try
            {
                string[] recordStr = ((string)record).Split('|');
                Employee newEmployee = null;
                switch (recordStr[0].ToUpper())
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
                        //Exist by sin
                        if (!employeeSinExist(newEmployee))
                        {
                            employees.Add(newEmployee);
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    else
                    {
                        //Employee is not valid dont add Needs LOG
                    }
                }
            }
            catch (MissingMemberException mME)
            {
                //throw mME;
            }
            catch (ArgumentException aE)
            {
                throw new Exception("Employee exist with that SIN");
            }
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
        public void SaveAll(StreamWriter fileOut)
        {
            foreach (var employee in employees)
            {
                fileOut.WriteLine(employee.ToString());
            }
        }


        /// <summary>
        /// Shows all of the record's information that was extracted from the database and stored in the Dictonary container
        /// </summary>
        /// <returns>List<string> - Returns a List containing element of type string, that contain the record's (employee's) information</returns>
        public List<string> ShowAll()
        {
            List<string> employeeStrings = new List<string>();
            foreach (var employee in employees)
            {
                employeeStrings.Add(employee.ToString());
            }
            return employeeStrings;
        }


        /// <summary>
        /// Remove an employee by specifying their SIN number as the argument 
        /// </summary>
        /// <param name="employeeSin">Based on this string value, the employee's SIN number will be removed if detected, if not then a notfying message will be displayed of the failure in removal</param>
        /// <returns>Returns a Message struct the outlines if the process failed by providing an error number and the accomodated error message that follows</returns>
        public void RemoveBySin(string employeeSin)
        {
            int employeesRemoved = employees.RemoveAll(element => element.SocialInsuranceNumber == employeeSin);
            //Employee removeEmployee = employees[employeeSin];
            if (employeesRemoved == 0)
            {
                throw new MissingMemberException("Employee with SIN/BN " + employeeSin + " not found");
            }
            else
            {
                throw new Exception("Removed " + employeesRemoved + " employees from database");
            }
        }


        public string GetEmployeeInfosBySin(string sin)
        {
            return string.Join("\n",from emp in employees where emp.SocialInsuranceNumber == sin select emp.ToString());
        }

        public bool employeeSinExist(Employee employee)
        {
            bool exist = false;
            if (employee.GetType() != typeof(ContractEmployee))
            {
                exist = employees.Count(value => value.SocialInsuranceNumber == employee.SocialInsuranceNumber) == 0 ? false : true;
            }
            return exist;
        }
        public List<string> updateSearch(string call, string search)
        {
            switch (call)
            {
                case "first":
                    CurrentSearch.RemoveAll(x=> x.FirstName != search);
                    break;
                case "last":
                    CurrentSearch.RemoveAll(x => x.LastName != search);
                    break;
                case "sin":
                    CurrentSearch.RemoveAll(x => x.SocialInsuranceNumber != search);
                    break;
            }
            List<string> employeeStrings = new List<string>();
            foreach (var employee in CurrentSearch)
            {
                employeeStrings.Add(employee.ToString());
            }
            return employeeStrings;
        }
        public void ClearSearch()
        {
            CurrentSearch = employees;
        }
        public void SelectIndex(int index)
        {
            currentEdit = CurrentSearch[index];
        }
        public string getEmployeeInfo()
        {
            return currentEdit.ToString();
        }
    }
}
