using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllEmployees;
namespace EmployeeUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestingConstructorForEmployee()
        {
            Employee testEmployee = new Employee("Jimmy", "White", "363 389 727", "1996/05/03");
            bool status = testEmployee.IsValid;
            Assert.AreEqual(true, status);
        }


    }
}
