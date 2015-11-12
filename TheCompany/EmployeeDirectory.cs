using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllEmployees;
using Supporting;
namespace TheCompany
{
    public class EmployeeDirectory
    {
        //key: sin
        Dictionary<string,Employee> employees = new Dictionary<string, Employee>();
        FileIO file = new FileIO();
        public void Load()
        {

        }
    }
}
