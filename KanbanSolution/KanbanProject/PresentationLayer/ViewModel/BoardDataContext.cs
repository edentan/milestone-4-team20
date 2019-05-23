using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using KanbanProject.InterfaceLayer;
using KanbanProject.InterfaceLayer.DataObjects;
using System.Windows.Data;

namespace KanbanProject.PresentationLayer.ViewModel
{
    class BoardDataContext : INotifyPropertyChanged
    {
        Service service;

        public BoardDataContext(Service service)
        {
            this.service = service;
            UpdateBottons();
            ShowThread();
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private ICollectionView gridView;
        public ICollectionView GridView
        {
            get
            {
                return gridView;
            }
            set
            {
                gridView = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GridView"));
            }
        }

        private ICollectionView gridViewCol;
        public ICollectionView GridViewCol
        {
            get
            {
                return gridViewCol;
            }
            set
            {
                gridViewCol = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GridViewCol"));
            }
        }

        string searchTerm = "";
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                searchTerm = value;
                UpdateFilterTasks();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SearchTerm"));
            }
        }

        private ObservableCollection<KanbanTask> tasks;
        public ObservableCollection<KanbanTask> Tasks
        {
            get
            {
                return tasks;
            }
            set
            {
                tasks = value;
                UpdateFilterTasks();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Tasks"));
            }
        }

        private ObservableCollection<KanbanColumn> columns;
        public ObservableCollection<KanbanColumn> Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
                UpdateFilterCols();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Columns"));
            }
        }

        private KanbanTask selected;
        public KanbanTask Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
            }
        }

        private KanbanColumn selectedCol;
        public KanbanColumn SelectedCol
        {
            get { return selectedCol; }
            set
            {
                selectedCol = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCol"));
            }
        }

        private void UpdateFilterTasks()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = tasks };
            ICollectionView cv = cvs.View;
            cv.Filter = o =>
            {
                KanbanTask t = o as KanbanTask;
                return (t.Title.ToUpper().Contains(SearchTerm.ToUpper()) | t.Description.ToUpper().Contains(SearchTerm.ToUpper()));
            };
            GridView = cv;
        }

        private void UpdateFilterCols()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = columns };
            ICollectionView cv = cvs.View;

            GridViewCol = cv;
        }

        public void ShowThread()
        {
            List<InterfaceLayerColumn> board = service.GetBoard();

            ObservableCollection<KanbanColumn> Columns = new ObservableCollection<KanbanColumn>();
            for (int i = 1; i <= board.Count; i++)
            {
                foreach (InterfaceLayerColumn c in board)
                {
                    if (c.colPlace == i)
                    {
                        string max = "";
                        if (c.maxLength == -1)
                            max = "-";
                        else
                            max = "" + c.maxLength;
                        Columns.Add(new KanbanColumn(c.colPlace, c.colName, max));
                    }
                }
            }
            columns = Columns;

            ObservableCollection<KanbanTask> Tasks = new ObservableCollection<KanbanTask>();
            for (int i = 1; i <= board.Count; i++)
            {
                foreach (var c in board)
                {
                    if (c.colPlace == i)
                    {
                        foreach (InterfaceLayerTask t in c.tasks)
                        {
                            Tasks.Add(new KanbanTask(c.colName, t.taskId, t.taskTitle, t.taskBody, t.dueDate, t.creationDate));
                        }
                    }
                }
            }
            tasks = Tasks;

            UpdateFilterTasks();
            UpdateFilterCols();
        }

        private string maxLength = "";
        public string MaxLength
        {
            get
            {
                return maxLength;
            }
            set
            {
                maxLength = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("MaxLength"));
            }
        }

        private string newColumn = "";
        public string NewColumn
        {
            get
            {
                return newColumn;
            }
            set
            {
                newColumn = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NewColumn"));
            }
        }

        private bool canChange = false;
        public bool CanChange
        {
            get
            {
                return canChange;
            }
            set
            {
                canChange = selected != null;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CanChange"));
            }
        }

        private bool canGoUp = false;
        public bool CanGoUp
        {
            get
            {
                return canGoUp;
            }
            set
            {
                canGoUp = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CanGoUp"));
            }
        }

        private bool canGoDown = false;
        public bool CanGoDown
        {
            get
            {
                return canGoDown;
            }
            set
            {
                canGoDown = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CanGoDown"));
            }
        }

        public void UpdateBottons()
        {
            if (selectedCol == null)
            {
                canGoUp = false;
                canGoDown = false;
            }
            else
            {
                canGoDown = service.IsLastColumn(SelectedCol.Place);
                canGoUp = SelectedCol.Place != 1;
            }
        }

        public void ShowThreadSorted(int choise)
        {
            ObservableCollection<KanbanTask> Tasks = new ObservableCollection<KanbanTask>();
            List<InterfaceLayerColumn> board = new List<InterfaceLayerColumn>();
            if (choise == 1)
                board = service.SortByCreationDate();
            else
                board = service.SortByDueDate();

            for (int i = 1; i <= board.Count; i++)
            {
                foreach (var c in board)
                {
                    if (c.colPlace == i)
                    {
                        foreach (InterfaceLayerTask t in c.tasks)
                        {
                            Tasks.Add(new KanbanTask(c.colName, t.taskId, t.taskTitle, t.taskBody, t.dueDate, t.creationDate));
                        }
                    }
                }
            }
            tasks = Tasks;

            UpdateFilterTasks();
        }
    }
}
