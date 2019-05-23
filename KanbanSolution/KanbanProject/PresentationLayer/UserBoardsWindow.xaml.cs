using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KanbanProject.InterfaceLayer;
using KanbanProject.PresentationLayer.ViewModel;

namespace KanbanProject.PresentationLayer
{
    /// <summary>
    /// Interaction logic for UserBoardsWindow.xaml
    /// </summary>
    public partial class UserBoardsWindow : Window
    {
        Service service;
        UserBoardsDataContext dataContext;

        public UserBoardsWindow(Service service)
        {
            InitializeComponent();
            this.service = service;
            int index = service.GetBoardId().IndexOf('@');
            string userName = service.GetUserId().Substring(0, index);
            welcome.Content = "Welcome " + userName + "!";
            dataContext = new UserBoardsDataContext(service);
            DataContext = dataContext;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            remove.IsEnabled = dataContext.Selected != null;
            open.IsEnabled = dataContext.Selected != null;

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
