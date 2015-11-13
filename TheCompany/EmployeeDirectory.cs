﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllEmployees;
using Supporting;
using System.IO;

namespace TheCompany
{
    public struct Message
    {
        public int code;
        public string message;
    }
    public class EmployeeDirectory
    {

        //key: sin
        Dictionary<string,Employee> employees = new Dictionary<string, Employee>();
        FileIO file = new FileIO();
        public void Load()
        {
            file.Load(Add);
        }
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
                    newEmployee = new ParttimeEmployee();
                    break;
                case "CT":
                    newEmployee = new ContractEmployee();
                    break;
                case "SN":
                    newEmployee = new SeasonalEmployee();
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
        public void Save()
        {
            file.Save(SaveAll);
        }
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

        public List<string> ShowAll()
        {
            List<string> employeeStrings = new List<string>();
            foreach (KeyValuePair<string,Employee> employee in employees)
            {
                employeeStrings.Add(employee.Value.ToString());
            }
            return employeeStrings;
        }

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

        public Message ExistBySin(string employeeSin)
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
                returnMessage.message = removeEmployee.ToString();
            }
            return returnMessage;
        }
    }
}
