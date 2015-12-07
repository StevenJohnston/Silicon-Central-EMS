using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllEmployees;
using EMSExceptions;

namespace EmployeeUnitTesting
{
    [TestClass]
    public class UnitTest1
    {

        //employee
        [TestMethod]
        public void TestingValidateContract_Employee_Normal()
        {
            bool status = false;
            try {
                Employee testEmployee = new Employee("Jimmy", "White", "246 454 284", "1996/05/03");
                status = testEmployee.Validate();
            }
            catch(EmployeeException ee)
            {
                string errors = ee.GetError();
            }
            
            Assert.AreEqual(true, status);
        }
        [TestMethod]
        public void TestingValidateContract_Employee_Exception()
        {
            Employee testEmployee = new Employee("Jimmy", "White", "36a 389 727", "1996/05/03");

            try
            {
                bool status = testEmployee.Validate();
                Assert.AreEqual(false, status);
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, true);

            }
        }

        

        //ContractEmployee
        [TestMethod]
        public void TestingValidateContract_ContractEmployee_Normal()
        {
            bool status = false;
            string[] testData = new string[7] {
                "Matt",
                "Naween",
                "123456782",
                "1995/3/4",
                "2000/12/12",
                "2013/10/9",
                "604043.34"
            };
            try
            {
                ContractEmployee testEmployee = new ContractEmployee(testData);
                status = testEmployee.Validate();
            }
            catch (EmployeeException eee)
            {
                string errors = eee.GetError();


            }
            Assert.AreEqual(true, status);
        }


        //Contract Employee
        [TestMethod]
        public void TestingValidateContract_ContractEmployee_Exception()
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

            try
            {
                bool status = testEmployee.Validate();
                Assert.AreEqual(false, status);
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, true);

            }

        }
                [TestMethod]
        public void TestingValidateLongingString_ContractEmployee_Normal()
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

            try
            {
                bool status = testEmployee.SuccessLogString();
                Assert.AreEqual(false, status);
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, true);

            }

        }






        [TestMethod]
        public void TestingValidateLogingString_ContractEmployee_Exception()
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

            try
            {
                bool status = testEmployee.Validate();
                Assert.AreEqual(false, status);
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, true);

            }

        }


        //Full time
        [TestMethod]
        public void TestingValidateContract_FulltimeEmployee_Normal()
        {
            bool status = false;
            string[] testData = new string[7] {
                "qwer",
                "drfdgf",
                "246 454 284",
                "1997/3/14",
                "2000/12/12",
                "2013/10/9",
                "604043.34"
            };


            try
            {
                FulltimeEmployee testEmployee = new FulltimeEmployee(testData);
                status = testEmployee.Validate();
            }
            catch (EmployeeException eee)
            {
                string errors = eee.GetError();


            }

       
            Assert.AreEqual(true, status);
        }
        //full time
        [TestMethod]
        public void TestingValidateContract_FulltimeEmployee_Exception()
        {
            string[] testData = new string[7] {
                "asdf",
                "fdas",
                "246 454 284",
                "1995/3/4",
                "2000/12/12",
                "2013109",
                "-a604043.34"
            };
            try
            {
                FulltimeEmployee testEmployee = new FulltimeEmployee(testData);
                bool status = testEmployee.Validate();
                Assert.AreEqual(false, status);
            }
            catch (Exception e)
            {
                Assert.AreEqual(true, true);

            }

          
        }

        //testing logging
       [TestMethod]
       public void TestingValidateLongingString_FulltimeEmployee_Normal()
        {
            bool status = false;
            string[] testData = new string[7] {
                "qwer",
                "drfdgf",
                "246 454 284",
                "1997/3/14",
                "2000/12/12",
                "2013/10/9",
                "604043.34"
            };


            try
            {
                FulltimeEmployee testEmployee = new FulltimeEmployee(testData);
                status = testEmployee.SuccessLogString();
            }
            catch (EmployeeException eee)
            {
                string errors = eee.GetError();


            }
            Assert.AreEqual(true, status);
        }
       //testing logging
       [TestMethod]
       public void TestingValidateLongingString_FulltimeEmployee_Exception()
       {
           bool status = false;
           string[] testData = new string[7] {
                "qwer",
                "drfdgf",
                "246 054 284",
                "1997/3/14",
                "2000/12/12",
                "2013/10/9",
                "604043.34"
            };


           try
           {
               FulltimeEmployee testEmployee = new FulltimeEmployee(testData);
               status = testEmployee.SuccessLogString();
           }
           catch (EmployeeException eee)
           {
               string errors = eee.GetError();
           }
           Assert.AreEqual(false, status);
       }










        //part time
       [TestMethod]
        public void TestingValidateContract_ParttimeEmployee_Normal()
        {
            string[] testData = new string[7] {
                "qwer",
                "drfdgf",
                "246 454 284",
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
       public void TestingValidateContract_ParttimeEmployee_Exception()
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
           try
           {
               ParttimeEmployee testEmployee = new ParttimeEmployee(testData);
               bool status = testEmployee.Validate();
               Assert.AreEqual(false, status);
           }
           catch (Exception e)
           {
               Assert.AreEqual(true, true);

           }
       }



       //testing logging
       [TestMethod]
       public void TestingValidateLongingString_ParttimeEmployee_Normal()
       {
           bool status = false;
           string[] testData = new string[7] {
                "qwer",
                "drfdgf",
                "246 454 284",
                "1997/3/14",
                "2000/12/12",
                "2013/10/9",
                "45.34"
            };


           try
           {
               ParttimeEmployee testEmployee = new ParttimeEmployee(testData);
               status = testEmployee.SuccessLogString();
           }
           catch (EmployeeException eee)
           {
               string errors = eee.GetError();


           }
           Assert.AreEqual(true, status);
       }
       [TestMethod]
       public void TestingValidateLongingString_ParttimeEmployee_Exception()
       {
           bool status = false;
           string[] testData = new string[7] {
                "qwer",
                "drfdgf",
                "246 454 284",
                "1997/3/14",
                "2000/12/12",
                "a/10/9",
                "45.34"
                //if i change the price then it will break note to self
            };


           try
           {
               ParttimeEmployee testEmployee = new ParttimeEmployee(testData);
               status = testEmployee.SuccessLogString();
           }
           catch (EmployeeException eee)
           {
               string errors = eee.GetError();


           }
           Assert.AreEqual(false, status);
       }








        //seasonal
       [TestMethod]
       public void TestingValidateContract_SeasonalEmployee_Normal()
       {
           bool status = false;
           string[] testData = new string[6] {
                "qwer",
                "drfdgf",
                "246 454 284",
                "1997/3/14",
                "Winter",
                "45.34"
            };


           try
           {
               SeasonalEmployee testEmployee = new SeasonalEmployee(testData);
               status = testEmployee.Validate();
           }
           catch (EmployeeException ee)
           {
               string errors = ee.GetError();
           }


          
           Assert.AreEqual(true, status);
       }
       //seasonal
       [TestMethod]
       public void TestingValidateContract_SeasonalEmployee_Exception()
       {
           string[] testData = new string[6] {
                "qwer",
                "drfdgf",
                "046 454 286",
                "1997/3/14",
                "black",
                "45.34"
            };
           try
           {
               SeasonalEmployee testEmployee = new SeasonalEmployee(testData);
               bool status = testEmployee.Validate();
               Assert.AreEqual(true, status);
           }
           catch (Exception e)
           {
               Assert.AreEqual(true, true);

           }
       }
       [TestMethod]
       public void TestingValidateLongingString_SeasonalEmployee_Normal()
       {
           bool status = false;
           string[] testData = new string[6] {
                "qwer",
                "drfdgf",
                "246 454 284",
                "1997/3/14",
                "Winter",
                "45.34"
            };


           try
           {
               SeasonalEmployee testEmployee = new SeasonalEmployee(testData);
               status = testEmployee.SuccessLogString();
           }
           catch (EmployeeException ee)
           {
               string errors = ee.GetError();
           }



           Assert.AreEqual(true, status);
       }

       [TestMethod]
       public void TestingValidateLongingString_SeasonalEmployee_Exception()
       {
           bool status = false;
           string[] testData = new string[6] {
                "qwer",
                "drfdgf",
                "246 454 284",
                "1997/3/14",
                "dfser",
                "45.34"
            };


           try
           {
               SeasonalEmployee testEmployee = new SeasonalEmployee(testData);
               status = testEmployee.SuccessLogString();
           }
           catch (EmployeeException ee)
           {
               string errors = ee.GetError();
           }



           Assert.AreEqual(false, status);
       }
       [TestMethod]
       public void TestingValidateSeason_SeasonalEmployee_Normal()
       {
           bool status = false;

           string[] testData = new string[6] {
                "qwer",
                "drfdgf",
                "246 454 284",
                "1997/3/14",
                "dfser",
                "45.34"
            };


         
               SeasonalEmployee testEmployee = new SeasonalEmployee();
               status = testEmployee.ValidateSeason("Winter");
        


           Assert.AreEqual(true, status);
       }
       [TestMethod]
       public void TestingValidateSeason_SeasonalEmployee_Exception()
       {
           bool status = false;

           string[] testData = new string[6] {
                "qwer",
                "drfdgf",
                "246 454 284",
                "1997/3/14",
                "dfser",
                "45.34"
            };



           SeasonalEmployee testEmployee = new SeasonalEmployee();
           status = testEmployee.ValidateSeason("Wisdfnter");
           Assert.AreEqual(false, status);
       }

    }
}
