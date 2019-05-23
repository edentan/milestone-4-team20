using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace KanbanProject.PresentationLayer
{
    class UserWindowDataContext : INotifyPropertyChanged
    {
        KanbanBoard kanban;

        public UserWindowDataContext()
        {
            kanban = new KanbanBoard();
            kanban.UploadData();
        }

        string email = "";
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        string password = "";
        public string PWD
        {
            get
            {
                return password;
            }
            set
            {
                password = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("password"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public bool Login()
        {
            return kanban.LogIn(email, password);
        }

        public void Register()
        {
            //length
            kanban.Register(email, password);
        }
    }
}
