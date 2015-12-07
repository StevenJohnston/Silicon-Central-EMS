using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSExceptions
{
    public class EmployeeException : Exception
    {
        public string employeeType = "";        
        public string operationType = "";
        public List<string> errorList = new List<string>();
        public bool hasErrors = false;

        public EmployeeException()
        {

        }
        

        public EmployeeException(string employeeType, string operationType, List<string> errorList)
        {
            this.employeeType = employeeType;
            this.operationType = operationType;
            this.errorList = errorList;

        }

        public void AddError(string error)
        {
            hasErrors = true;
            this.errorList.Add(error);
        }

        public string GetError()
        {
            string errorString = "";
            
            if (employeeType != "")
            {
                errorString += "\tEmployee Type: " + employeeType + "\n";
            }

            if (operationType != "")
            {
                errorString += "\tOperation Type: " + operationType + "\n";
            }

            if (errorList.Count() != 0)
            {
                errorString += "\tError(s): ";
                foreach(string s in errorList)
                {
                    errorString += s + "\n";
                }
            }

            return errorString;
        }
    }
}
