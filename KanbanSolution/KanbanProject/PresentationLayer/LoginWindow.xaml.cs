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
using System.Windows.Shapes;
using KanbanProject.InterfaceLayer;


namespace KanbanProject.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginDataContext loginDataContext;
        Service service;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LoginWindow()
        {
            InitializeComponent();
            service = new Service();
            loginDataContext = new LoginDataContext();
            DataContext = loginDataContext;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (Login())
            {
                Log.Info("User with email " + loginDataContext.Email + " logged in.");
                UserBoardsWindow userBoards = new UserBoardsWindow(service);
                userBoards.Show();
                Close();
            }
            else
            {
                Log.Error("Email and password inserted do not match.");
                MessageBox.Show("Email and password do not match, please try again.");
                loginDataContext.Email = "";
                loginDataContext.PWD = "";
            }
        }


        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableLogin();
        }


        private void Password_TextChanged(object sender, RoutedEventArgs e)
        {
            EnableLogin();
        }

        private void EnableLogin()
        {
            if (email.Text.Length > 0 & password.Text.Length > 0)
            {
                login.IsEnabled = true;
            }
        }

        private bool Login()
        {
            return service.LogIn(loginDataContext.Email, loginDataContext.PWD);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void Register_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(service);
            registerWindow.Show();
            Close();
        }
    }
}
