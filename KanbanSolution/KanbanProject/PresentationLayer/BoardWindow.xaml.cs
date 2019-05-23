using System;
using System.Windows;
using KanbanProject.InterfaceLayer;
using KanbanProject.PresentationLayer.ViewModel;
using KanbanProject.InterfaceLayer.DataObjects;


namespace KanbanProject.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        Service service;
        BoardDataContext boardDataContext;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public BoardWindow(Service service)
        {
            InitializeComponent();
            this.service = service;
            //GetBoardName!!!!
            header.Content = service.GetBoard();
            boardDataContext = new BoardDataContext(service);
            DataContext = boardDataContext;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Log.Info("User with email " + service.GetuserId() + " logged out.");
            MainWindow main = new MainWindow(this.service);
            main.Show();
            Close();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWindow = new AddTaskWindow(service);
            addTaskWindow.Show();
            Close();
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (boardDataContext.Selected != null)
            {
                TaskWindow task = new TaskWindow(service, boardDataContext.Selected.Id);
                task.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please select a task you want to edit.");
                Log.Warn("User tried to edit a task without choosing one.");
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (boardDataContext.Selected == null)
            {
                MessageBox.Show("Please select a task first");
            }
            else if(service.RemoveTask(boardDataContext.Selected.Id))
            {
                boardDataContext.ShowThread();
            }
                
        }

        private void SearchB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Board is updated.");
        }

        private void SetMaxLength_Click(object sender, RoutedEventArgs e)
        {
            int max = -1;
            try
            {
                max = int.Parse(boardDataContext.MaxLength);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please insert a valid number");
                boardDataContext.MaxLength = "";
            }
            if (max <= 0)
            {
                MessageBox.Show("Please insert a valid positive number");
                boardDataContext.MaxLength = "";
            }

            else
            {
                if (boardDataContext.SelectedCol != null)
                {
                    InterfaceLayerColumn col = service.GetColumn(boardDataContext.SelectedCol.Name);
                    if (col.tasks.Count > max)
                    {
                        Log.Warn("The user tried to limit a column, but the column already contain more tasks than the wanted limit");
                        MessageBox.Show("There are more tasks in this column than the wanted limit.");
                    }
                    else
                    {
                        service.SetColumnMax(boardDataContext.SelectedCol.Name, max);
                        boardDataContext.ShowThread();
                        MessageBox.Show("Column's max tasks was updated.");
                        boardDataContext.SelectedCol = null;
                    }
                    boardDataContext.MaxLength = "";
                }
                else
                    MessageBox.Show("Please select a column first");
            }
        }

        private void MoveBackwords_Click(object sender, RoutedEventArgs e)
        {
            if (boardDataContext.SelectedCol == null)
                MessageBox.Show("Please select a column first");
            else
            {
                if (boardDataContext.SelectedCol.Place == 1)
                {
                    Log.Warn("The user tried to move backward the first column");
                    MessageBox.Show("You can't move backward the first column");
                }
                else
                {
                    service.MoveBack(boardDataContext.SelectedCol.Place);
                    DataContext = boardDataContext;
                    boardDataContext.ShowThread();
                }
            }
            boardDataContext.SelectedCol = null;
        }

        private void MoveForwards_Click(object sender, RoutedEventArgs e)
        {
            if (boardDataContext.SelectedCol == null)
                MessageBox.Show("Please select a column first");
            else
            {
                if (service.IsLastColumn(boardDataContext.SelectedCol.Place))
                {
                    Log.Warn("The user tried to forward the last column");
                    MessageBox.Show("You can't forward the last column");
                }
                else
                {
                    service.MoveForward(boardDataContext.SelectedCol.Place);
                    DataContext = boardDataContext;
                    boardDataContext.ShowThread();
                }
            }
            boardDataContext.SelectedCol = null;
        }
       
        private void Add_Click(object sender, RoutedEventArgs e)
        {
           bool validName = service.AddColumn(boardDataContext.NewColumn);
            if (!validName)
            {
                Log.Warn("The user tried to add a column with an empty name.");
                MessageBox.Show("You can't creat a column with an empty name.");
            }
            else
            {
                Log.Info("Column " + boardDataContext.NewColumn + " was added.");
                boardDataContext.ShowThread();
            }
                
        }

        private void AddName_TextChanged(object sender, RoutedEventArgs e)
        {
            EnableAdding();
        }

        private void EnableAdding()
        {
            if (boardDataContext.NewColumn.Length > 0)
            {
                Add.IsEnabled = true;
            }
        }

        private void RemoveColumn_Click(object sender, RoutedEventArgs e)
        {
            if (boardDataContext.SelectedCol == null)
                MessageBox.Show("Please select a column first.");
            else
            {
                bool removed = service.RemoveColumn(boardDataContext.SelectedCol.Name);
                if (removed)
                {
                    MessageBox.Show("The column was removed.");
                    Log.Info("Column" + boardDataContext.SelectedCol.Name + "was removed.");
                    boardDataContext.ShowThread();
                }
                else
                {
                    MessageBox.Show("Your board must contain at least one column.");
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            bool inLastCol = false;
            if (boardDataContext.Selected != null)
                 inLastCol = service.IsLastColumn(service.GetColumn(boardDataContext.Selected.ColName).colPlace);
            editTask.IsEnabled = boardDataContext.Selected != null & !inLastCol;
            deleteTask.IsEnabled = boardDataContext.Selected != null;
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            if(dueD.IsChecked.Value)
            {
                service.SortByDueDate();
                boardDataContext.ShowThreadSorted(2);
            }
            if(creationD.IsChecked.Value)
            {
                service.SortByCreationDate();
                boardDataContext.ShowThreadSorted(1);
            }
        }
    }
}
