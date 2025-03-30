using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using practic5.Model;

namespace practic5.Services
{
    public class Validation
    {
        public class EmployeeValidator
        {
            public (bool isValid, List<string> errors) Validate(employees employee)
            {
                var errors = new List<string>();

                if (string.IsNullOrWhiteSpace(employee.lastName) || employee.lastName.Length < 2 || employee.lastName.Length > 30)
                    errors.Add("Фамилия должна содержать от 2 до 30 символов.");

                if (string.IsNullOrWhiteSpace(employee.firstName) || employee.firstName.Length < 2 || employee.firstName.Length > 30)
                    errors.Add("Имя должно содержать от 2 до 30 символов.");

                if (!string.IsNullOrWhiteSpace(employee.middleName) && (employee.middleName.Length < 2 || employee.middleName.Length > 30))
                    errors.Add("Отчество должно содержать от 2 до 30 символов.");

                if (employee.birthDate == default(DateTime))
                    errors.Add("Дата рождения обязательна.");

                if (employee.positionAtWork <= 0)
                    errors.Add("Должность на работе обязательна.");

                if (employee.wages < 0)
                    errors.Add("Заработная плата не может быть отрицательной.");

                if (string.IsNullOrWhiteSpace(employee.phoneNumber) || employee.phoneNumber.Length > 12)
                    errors.Add("Номер телефона должен содержать до 12 символов.");

                return (errors.Count == 0, errors);
            }
        }

        public class UserValidator
        {
            public (bool isValid, List<string> errors) Validate(users user)
            {
                var errors = new List<string>();

                if (string.IsNullOrWhiteSpace(user.login) || user.login.Length < 2 || user.login.Length > 50)
                    errors.Add("Логин должен содержать от 2 до 50 символов.");

                if (string.IsNullOrWhiteSpace(user.password) || user.password.Length < 8 || user.password.Length > 256)
                    errors.Add("Пароль должен содержать от 8 до 256 символов.");

                if (user.employeeData <= 0)
                    errors.Add("Данные сотрудника обязательны.");

                return (errors.Count == 0, errors);
            }
        }

    }
}
