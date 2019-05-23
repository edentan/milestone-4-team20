using System;
using System.Windows;
using KanbanProject.InterfaceLayer;
using KanbanProject.PresentationLayer.ViewModel;

namespace KanbanProject.PresentationLayer
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        AddTaskDataContext addTaskDataContext;
        Service service;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AddTaskWindow(Service service)
        {
            InitializeComponent();
            addTaskDataContext = new AddTaskDataContext();
            DataContext = addTaskDataContext;
            this.service = service;
        }

       
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            BoardWindow boardWindow = new BoardWindow(service);
            boardWindow.Show();
            Close();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
          if(AddNewTask())
            {
                BoardWindow boardWindow = new BoardWindow(service);
                boardWindow.Show();
                Close();
            }

        }
        
        private bool AddNewTask()
        {
            bool ValidTitle = addTaskDataContext.Title.Length >= 1 & addTaskDataContext.Title.Length <= 50;
            bool ValidBody = addTaskDataContext.Body.Length <= 300;
            bool ValidDate = addTaskDataContext.DueDate.Date.CompareTo(DateTime.Now) >= 0;

            if (!ValidTitle | !ValidDate | !ValidBody)
            {
                if (!ValidTitle)
                {
                    MessageBox.Show("Invalid title, please follow the instruction.");
                }

                if (!ValidBody)
                {
                    MessageBox.Show("Invalid description, please follow the instruction.");
                }
                if (!ValidDate)
                {
                    MessageBox.Show("Invalid date, please follow the instruction.");
                }
            }
            if (ValidTitle & ValidBody & ValidDate)
            {
                if (!service.AddTask(addTaskDataContext.Title, addTaskDataContext.Body, addTaskDataContext.DueDate))
                    MessageBox.Show("Your first column is full, please move or remove at least one task from it.");
                else
                {
                    Log.Info("Task with title: '" + addTaskDataContext.Title + "' was added.");
                    return true;
                }
            }
            else
            {
                Log.Warn("Invalid input in adding task.");
            }
            return false;
        }
        
    }
}
