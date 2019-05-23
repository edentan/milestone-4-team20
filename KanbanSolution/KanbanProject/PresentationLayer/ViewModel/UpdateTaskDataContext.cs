using System;
using System.ComponentModel;
using KanbanProject.InterfaceLayer.DataObjects;
using KanbanProject.InterfaceLayer;

namespace KanbanProject.PresentationLayer.ViewModel
{
    class UpdateTaskDataContext : INotifyPropertyChanged
    {
        string title;
        string body;
        DateTime dueDate;
        DateTime creationDate;
        bool canForward;
        Service service;
        
        public UpdateTaskDataContext(Service service, InterfaceLayerTask task)
        {
            this.service = service;
            title = task.taskTitle;
            body = task.taskBody;
            dueDate = DateTime.Parse(task.dueDate);
            creationDate = DateTime.Parse(task.creationDate);
            InterfaceLayerColumn currentCol = service.GetColumn(task.colName);
            InterfaceLayerColumn nextCol = service.GetColumn(currentCol.colPlace + 1);
            canForward = (nextCol.colPlace <= service.GetBoard().Count) & ((nextCol.tasks.Count < nextCol.maxLength) | nextCol.maxLength == -1);
        }


        public bool CanForward
        {
            get
            {
                return canForward;
            }
            set
            {
                canForward = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CanForward"));
            }

        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;

                if (PropertyChanged != null)
                      PropertyChanged(this, new PropertyChangedEventArgs("Title"));
            }
        }

        public string Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Body"));
            }
        }

        public DateTime DueDate
        {
            get
            {
                return dueDate;
            }
            set
            {
                dueDate = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DueDate"));
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return dueDate;
            }
            private set
            {
                dueDate = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DueDate"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
