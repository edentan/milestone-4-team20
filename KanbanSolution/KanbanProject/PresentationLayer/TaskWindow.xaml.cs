using System;
using System.Windows;
using KanbanProject.InterfaceLayer;
using KanbanProject.PresentationLayer.ViewModel;

namespace KanbanProject.PresentationLayer
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        Service service;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        UpdateTaskDataContext updateTask;
        private int taskId;

        public TaskWindow(Service service, int taskId)
        {
            InitializeComponent();
            this.service = service;
            this.taskId = taskId;
            updateTask = new UpdateTaskDataContext(service, service.FindTask(taskId));
            DataContext = updateTask;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            BoardWindow boardWindow = new BoardWindow(service);
            boardWindow.Show();
            Close();
        }

        private void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            if (EditTask())
            {
                BoardWindow boardWindow = new BoardWindow(service);
                boardWindow.Show();
                Close();
            }
        }
        

        private bool EditTask()
        {
            bool updated = false;
            bool validTitle = updateTask.Title.Length >= 1 & updateTask.Title.Length <= 50;
            if(!validTitle)
            {
                MessageBox.Show("Invalid title, please follow the instruction.");
            }

            bool validBody = updateTask.Body.Length <= 300;
            if (!validBody)
            {
                MessageBox.Show("Invalid description, please follow the instruction.");
            }

            bool validDate = updateTask.DueDate.Date.CompareTo(DateTime.Now) >= 0;
            if (!validDate)
            {
                MessageBox.Show("Invalid date, please follow the instruction.");
            }

            if (validTitle & validBody & validDate)
            {
                updated = service.UpdateTask(taskId, updateTask.Title, updateTask.Body, updateTask.DueDate, forwardTask.IsChecked.Value);
                Log.Info("Task with id: " + taskId + " was updated.");
            }
            return updated;
        }


    }
}
