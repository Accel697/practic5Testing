using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using practic5.Model;
using practic5.Services;
using static practic5.Services.Validation;

namespace practic5
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        public AddEmployee()
        {
            InitializeComponent();
            LoadJobTitles();
        }

        private void LoadJobTitles()
        {
            using (var context = new testing5Entities())
            {
                var jobs = context.jobTitles.ToList();
                if (jobs.Any())
                {
                    cbPositionAtWork.ItemsSource = jobs;
                    cbPositionAtWork.DisplayMemberPath = "title";
                    cbPositionAtWork.SelectedValuePath = "idJob";
                }
                else
                {
                    MessageBox.Show("Должности не найдены");
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbPositionAtWork.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите должность", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedPosition = cbPositionAtWork.SelectedItem as jobTitles;

            var newEmployee = new employees
            {
                lastName = tbLastName.Text,
                firstName = tbFirstName.Text,
                middleName = tbMiddleName.Text,
                birthDate = DateTime.TryParse(tbBornDate.Text, out var bornDate) ? bornDate : DateTime.MinValue,
                positionAtWork = selectedPosition.idJob,
                wages = decimal.TryParse(tbWages.Text, out var wages) ? wages : 0,
                phoneNumber = tbPhoneNumber.Text
            };

            var employeeValidator = new EmployeeValidator();
            var (isEmployeeValid, employeeErrors) = employeeValidator.Validate(newEmployee);

            if (!isEmployeeValid)
            {
                MessageBox.Show(string.Join("\n", employeeErrors), "Ошибки валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var context = new testing5Entities())
                {
                    context.employees.Add(newEmployee);
                    context.SaveChanges();
                }

                MessageBox.Show("Сотрудник успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
