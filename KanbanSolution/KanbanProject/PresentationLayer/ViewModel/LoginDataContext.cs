using System.ComponentModel;

namespace KanbanProject.PresentationLayer
{
    class LoginDataContext : INotifyPropertyChanged
    {
        string email;
        string password;
        public LoginDataContext()
        {
            email = "";
            password = "";
        }

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
                    PropertyChanged(this, new PropertyChangedEventArgs("PWD"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

       
    }
}
