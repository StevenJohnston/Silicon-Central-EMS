using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllEmployees;
namespace EmployeeUnitTesting
{
    [TestClass]
    public class UnitTest1
    {

        //employee
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


        //ContractEmployee
        [TestMethod]
        public void TestingValidateContract_ContractEmployee_Normal()
        {
            string[] testData = new string[7] {
                "Matt",
                "Naween",
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


        //Contract Employee
        [TestMethod]
        public void TestingValidateContract_ContractEmployee_Exeption()
        {
            string[] testData = new string[7] {
                "Tamool",
                "Jim",
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
        //Full time
        [TestMethod]
        public void TestingValidateContract_FulltimeEmployee_Normal()
        {
            string[] testData = new string[7] {
                "qwer",
                "drfdgf",
                "046 454 286",
                "1997/3/14",
                "2000/12/12",
                "2013/10/9",
                "604043.34"
            };
            FulltimeEmployee testEmployee = new FulltimeEmployee(testData);
            bool status = testEmployee.Validate();
            Assert.AreEqual(true, status);
        }
        //full time
        [TestMethod]
        public void TestingValidateContract_FulltimeEmployee_Exeption()
        {
            string[] testData = new string[7] {
                "asdf",
                "fdas",
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

        //part time
       [TestMethod]
        public void TestingValidateContract_ParttimeEmployee_Normal()
        {
            string[] testData = new string[7] {
                "qwer",
                "drfdgf",
                "046 454 286",
                "1997/3/14",
                "2000/12/12",
                "2013/10/9",
                "45.34"
            };
            ParttimeEmployee testEmployee = new ParttimeEmployee(testData);
            bool status = testEmployee.Validate();
            Assert.AreEqual(true, status);
        }



        //part time
       [TestMethod]
       public void TestingValidateContract_ParttimeEmployee_Exeption()
       {
           string[] testData = new string[7] {
                "qwer",
                "drfdgf",
                "046 454 286",
                "1997/3/14",
                "2000/12/12",
                "2013/10/9",
                "a"
            };
           ParttimeEmployee testEmployee = new ParttimeEmployee(testData);
           bool status = testEmployee.Validate();
           Assert.AreEqual(false, status);
       }


        //seasonal
       [TestMethod]
       public void TestingValidateContract_ParttimeEmployee_Normal()
       {
           string[] testData = new string[6] {
                "qwer",
                "drfdgf",
                "046 454 286",
                "1997/3/14",
                "Winter",
                "45.34"
            };
           SeasonalEmployee testEmployee = new SeasonalEmployee(testData);
           bool status = testEmployee.Validate();
           Assert.AreEqual(true, status);
       }
       //seasonal
       [TestMethod]
       public void TestingValidateContract_ParttimeEmployee_Normal()
       {
           string[] testData = new string[6] {
                "qwer",
                "drfdgf",
                "046 454 286",
                "1997/3/14",
                "black",
                "45.34"
            };
           SeasonalEmployee testEmployee = new SeasonalEmployee(testData);
           bool status = testEmployee.Validate();
           Assert.AreEqual(true, status);
       }

    }
}
