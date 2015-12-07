using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllEmployees;
namespace EmployeeUnitTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestingConstructorForEmployee_Employee_Normal()
        {
            Employee testEmployee = new Employee("Jimmy", "White", "363 389 727", "1996/05/03");
            bool status = testEmployee.Validate();
            Assert.AreEqual(true, status);
        }
        [TestMethod]
        public void TestingConstructorForEmployee_Employee_Exeption()
        {
            Employee testEmployee = new Employee("Jimmy", "White", "36a 389 727", "1996/05/03");
            bool status = testEmployee.Validate();
            Assert.AreEqual(false, status);
        }
        [TestMethod]
        public void TestingValidateContract_ContractEmployee_Normal()
        {
            
            ContractEmployee testEmployee = new ContractEmployee();
            bool status = testEmployee.Validate();
            Assert.AreEqual(true, status);
        }
        [TestMethod]
        public void TestingValidateContract_ContractEmployee_Exeption()
        {
            ContractEmployee testEmployee = new ContractEmployee();
            bool status = testEmployee.Validate();
            Assert.AreEqual(false, status);
        }

    }
}
