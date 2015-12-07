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
            Employee testEmployee = new Employee("Jimmy", "White", "046 454 286", "1996/05/03");
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
            string[] testData = new string[7] {
                "Will",
                "Pring",
                "123456782",
                "1995/3/4",
                "2000/12/12",
                "2013/10/9",
                "604043.34"
            };
            ContractEmployee testEmployee = new ContractEmployee(testData);
            bool status = testEmployee.Validate();
            Assert.AreEqual(true, status);
        }
        [TestMethod]
        public void TestingValidateContract_ContractEmployee_Exeption()
        {
            string[] testData = new string[7] {
                "Will",
                "Pring",
                "123456782",
                "1995/3/4",
                "2000/12/12",
                "2013/10/9",
                "-6a04043.34"
            };
            ContractEmployee testEmployee = new ContractEmployee();
            bool status = testEmployee.Validate();
            Assert.AreEqual(false, status);
        }

        [TestMethod]
        public void TestingValidateContract_FulltimeEmployee_Normal()
        {
            string[] testData = new string[7] {
                "Will",
                "Pring",
                "046 454 286",
                "1995/3/4",
                "2000/12/12",
                "2013/10/9",
                "604043.34"
            };
            FulltimeEmployee testEmployee = new FulltimeEmployee(testData);
            bool status = testEmployee.Validate();
            Assert.AreEqual(true, status);
        }
        [TestMethod]
        public void TestingValidateContract_FulltimeEmployee_Exeption()
        {
            string[] testData = new string[7] {
                "Will",
                "Pring",
                "046 454 286",
                "1995/3/4",
                "2000/12/12",
                "2013109",
                "604043.34"
            };
            FulltimeEmployee testEmployee = new FulltimeEmployee(testData);
            bool status = testEmployee.Validate();
            Assert.AreEqual(false, status);
        }


    }
}
