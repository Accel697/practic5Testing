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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using practic5.Model;

namespace practic5
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        public Autho()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e) 
        {
            string login = txtbLogin.Text.Trim();
            string password = pswbPassword.Password.Trim();

            using (var context = new testing5Entities())
            {
                var user = context.users.Where(x => x.login == login && x.password == password).FirstOrDefault();

                if (user != null)
                {
                    txtbLogin.Clear();
                    pswbPassword.Clear();
                    LoadPage(user, user.employees.positionAtWork.ToString());
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");

                    pswbPassword.Clear();
                }
            }
        }

        private void LoadPage(users user, string idPositionAtWork)
        {
            switch (idPositionAtWork)
            {
                case "1":
                    NavigationService.Navigate(new Admin(user));
                    break;
                default:
                    NavigationService.Navigate(new Client(user));
                    break;
            }
        }
    }
}
