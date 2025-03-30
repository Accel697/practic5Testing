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
using static practic5.Services.Validation;

namespace practic5
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new users
            {
                login = txtbLogin.Text,
                password = txtbPassword.Text,
                employeeData = int.Parse(txtbId.Text),
            };

            var userValidator = new UserValidator();
            var (isUserValid, userErrors) = userValidator.Validate(newUser);

            if (!isUserValid)
            {
                MessageBox.Show(string.Join("\n", userErrors), "Ошибки валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var context = new testing5Entities())
                {
                    bool isLoginExists = context.users.Any(u => u.login == newUser.login);
                    if (isLoginExists)
                    {
                        MessageBox.Show("Этот логин уже занят. Пожалуйста, выберите другой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    context.users.Add(newUser);
                    context.SaveChanges();
                }
                MessageBox.Show("Регистрация прошла успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
