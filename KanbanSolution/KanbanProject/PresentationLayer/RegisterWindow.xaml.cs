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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        RegisterDataContext registerDataContext;
        Service service;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RegisterWindow(Service service)
        {
            InitializeComponent();
            registerDataContext = new RegisterDataContext();
            DataContext = registerDataContext;
            this.service = service;
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableRegister();
        }

        private void Password_TextChanged(object sender, RoutedEventArgs e)
        {
            EnableRegister();
        }

        private void EnableRegister()
        {
            if (password.Text.Length > 0 & email.Text.Length > 0)
            {
                register.IsEnabled = true;
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if(Register())
            {
                MessageBox.Show("Registered successfully, please log in.");
                Log.Info("New user registered with the email: " + registerDataContext.Email);
                LoginWindow login = new LoginWindow();
                login.Show();
                Close();
            }
        }

        private bool Register()
        {
            bool validEmail = false;
            bool uniqueEmail = false;
            bool validPass = false;
            validEmail = service.IsValidEmail(registerDataContext.Email);

            if(!validEmail)//invalid email
            {
                Log.Warn("Input email was invalid.");
                MessageBox.Show("Email is invalid.");
                this.email.Text = null;
            }

            uniqueEmail = service.IsUniqueEmail(registerDataContext.Email);

            if (!uniqueEmail)//email already exists in the system
            {
                Log.Warn("Input email already is used in the system.");
                MessageBox.Show("Email already exists in the system, please insert a new one.");
                this.email.Text = null;
               
            }
            validPass = service.IsValidPassword(registerDataContext.PWD);
            if(!validPass) //invalid password
            {
                Log.Warn("Input password was invalid.");
                MessageBox.Show("Password is invalid, please follow the instructions below.");
                this.password.Text = null;

            }
            if (validEmail & validPass & uniqueEmail)
            {
                service.Register(registerDataContext.Email, registerDataContext.PWD);
            }
            return validEmail & validPass & uniqueEmail;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            Close();
        }
    }
}
