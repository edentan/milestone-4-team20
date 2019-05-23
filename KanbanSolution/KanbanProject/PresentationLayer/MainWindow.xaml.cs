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
using KanbanProject.PresentationLayer;
using KanbanProject.InterfaceLayer;


namespace KanbanProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        Service service;

        public MainWindow()
        {
            InitializeComponent();
            service = new Service();
            service.UploadData();        
        }

        public MainWindow(Service service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(service);
            loginWindow.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(service);
            registerWindow.Show();
            this.Close();
        }
    }
}
