using System;
using System.Collections.Generic;
using System.Linq;
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

namespace practic5
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        private List<employees> _employeesList;
        private List<employees> _filteredEmployees;
        private List<string> _jobTitles;

        public Admin(users user)
        {
            InitializeComponent();
            LoadEmployees();
            LoadJobTitles();
        }

        private void LoadEmployees()
        {
            using (var context = new testing5Entities())
            {
                _employeesList = context.employees.ToList();
                EmployeesListView.ItemsSource = _employeesList;
            }
        }

        private void LoadJobTitles()
        {
            using (var context = new testing5Entities())
            {
                _jobTitles = context.jobTitles.Select(j => j.title).Distinct().ToList();

                _jobTitles.Insert(0, "Все должности");

                cbJobTitle.ItemsSource = _jobTitles;

                cbJobTitle.SelectedIndex = 0;
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void cbJobTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void FilterEmployees()
        {
            string searchText = tbSearch.Text.ToLower();
            string selectedJobTitle = cbJobTitle.SelectedItem as string;

            _filteredEmployees = _employeesList.Where(emp =>
                (emp.lastName + " " + emp.firstName + " " + emp.middleName).ToLower().Contains(searchText) &&
                (selectedJobTitle == "Все должности" || emp.jobTitles.title == selectedJobTitle))
                .ToList();

            EmployeesListView.ItemsSource = null;
            EmployeesListView.ItemsSource = _filteredEmployees;
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployee());
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }
    }
}
