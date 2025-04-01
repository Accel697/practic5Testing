using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using practic5.Services;
using practic5.Model;

namespace UnitTestPractic5
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WrongEmployeeData()
        {
            var employeeValidator = new Validation.EmployeeValidator();
            var newEmployee = new employees
            {
                lastName = "d",
                firstName = "d",
                middleName = "d",
                birthDate = DateTime.TryParse("18", out var bornDate) ? bornDate : DateTime.MinValue,
                positionAtWork = -1,
                wages = decimal.TryParse("-10000.00", out var wages) ? wages : 0,
                phoneNumber = "8(999)999 99 - 99"
            };
            var (isEmployeeValid, employeeErrors) = employeeValidator.Validate(newEmployee);
            Assert.IsFalse(isEmployeeValid);
        }

        [TestMethod]
        public void WithoutMiddleName()
        {
            var employeeValidator = new Validation.EmployeeValidator();
            var newEmployee = new employees
            {
                lastName = "dd",
                firstName = "dd",
                middleName = null,
                birthDate = DateTime.TryParse("10.10.2000", out var bornDate) ? bornDate : DateTime.MinValue,
                positionAtWork = 1,
                wages = decimal.TryParse("30000.00", out var wages) ? wages : 0,
                phoneNumber = "89999999999"
            };
            var (isEmployeeValid, employeeErrors) = employeeValidator.Validate(newEmployee);
            Assert.IsTrue(isEmployeeValid);
        }

        [TestMethod]
        public void ValidEmployeeData()
        {
            var employeeValidator = new Validation.EmployeeValidator();
            var newEmployee = new employees
            {
                lastName = "dd",
                firstName = "dd",
                middleName = "dd",
                birthDate = DateTime.TryParse("10.10.2000", out var bornDate) ? bornDate : DateTime.MinValue,
                positionAtWork = 1,
                wages = decimal.TryParse("30000.00", out var wages) ? wages : 0,
                phoneNumber = "89999999999"
            };
            var (isEmployeeValid, employeeErrors) = employeeValidator.Validate(newEmployee);
            Assert.IsTrue(isEmployeeValid);
        }

        [TestMethod]
        public void WrongUserData()
        {
            var userValidator = new Validation.UserValidator();
            var newUser = new users
            {
                login = "d",
                password = "d",
                employeeData = -1,
            };
            var (isUserValid, userErrors) = userValidator.Validate(newUser);
            Assert.IsFalse(isUserValid);
        }

        [TestMethod]
        public void ValidUserData()
        {
            var userValidator = new Validation.UserValidator();
            var newUser = new users
            {
                login = "dd",
                password = "dddddddd",
                employeeData = 1,
            };
            var (isUserValid, userErrors) = userValidator.Validate(newUser);
            Assert.IsTrue(isUserValid);
        }

        [TestMethod]
        public void NullUserData() 
        {
            var userValidator = new Validation.UserValidator();
            var newUser = new users
            {
                login = null,
                password = null,
                employeeData = 0,
            };
            var (isUserValid, userErrors) = userValidator.Validate(newUser);
            Assert.IsFalse(isUserValid);
        }
    }
}
