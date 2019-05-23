
using System;
using System.ComponentModel;

namespace KanbanProject.PresentationLayer.ViewModel
{
    class AddTaskDataContext : INotifyPropertyChanged
    {
        string title;
        string body;
        DateTime dueDate;

        public AddTaskDataContext()
        {
            title = "";
            body = "";
            dueDate = DateTime.Now;
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
