using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.BusinessLayer;

namespace KanbanProject
{
    public class KanbanBoard
    {
        private User user;
        private UserHandler userHandler;
        private TaskHandler taskHandler;

        private List<User> allUsers;
        private List<BusinessLayer.Task> allTasks;

        public KanbanBoard()
        {
            user = new User();
            allUsers = new List<User>();
            allTasks = new List<BusinessLayer.Task>();
            if (taskHandler == null)
                taskHandler = new TaskHandler("allTasks");
            if (userHandler == null)
                userHandler = new UserHandler("allUsers");
        }

        public KanbanBoard(User user)
        {
            this.user = user;
            if (taskHandler == null)
                taskHandler = new TaskHandler("allTasks");
            if (userHandler == null)
                userHandler = new UserHandler("allUsers");

        }

        public void UploadData()
        {
            foreach (object[] o in taskHandler.List)
            {
                BusinessLayer.Task temp = new BusinessLayer.Task((string)o[2], (string)o[3], (DateTime)o[4], (DateTime)o[5], (int)o[1], (string)o[0], (int)o[6]);
                allTasks.Add(temp);
            }
            
            foreach(object[] o in userHandler.List)
            {
                User temp = new User((string)o[0], (string)o[1], (int)o[2]);
                temp.GetBoard().SetMaxLength((int)o[3]);
                allUsers.Add(temp);
            }

            user.SetAllUsers(allUsers);

            foreach(User u in allUsers)
            {
                u.GetBoard().SortCols(allTasks);
            }
            

        }

        //user functions
        public void Register(string email, string password, int length)
        {
            user = new User(email, password, length);
            userHandler.AddUserToFile("allUsers", email, password, length);
        }

        public bool LogIn(string email, string password)
        {
            foreach (User u in allUsers)
            {
                if (u.GetEmail().Equals(email))
                {
                    if (u.GetPassword().Equals(password))
                    {
                        user = u;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var add = new System.Net.Mail.MailAddress(email);
                return add.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsUniqueEmail(string email)
        {
            if (allUsers.Count > 0)
            {
                foreach (User u in allUsers)
                {
                    if (u.GetEmail().Equals(email))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsValidPassword(string password)
        {
            bool hasBigLetter = false;
            bool hasSmallLetter = false;
            bool hasNumber = false;

            if (password.Length <= 20 & password.Length >= 4)
            {
                char[] pass = password.ToCharArray();
                for (int i = 0; i < pass.Length; i++)
                {
                    if (pass[i] > 47 & pass[i] < 58)
                        hasNumber = true;
                    else if (pass[i] > 64 & pass[i] < 91)
                        hasBigLetter = true;
                    else if (pass[i] > 96 & pass[i] < 123)
                        hasSmallLetter = true;
                }
            }
            return hasSmallLetter & hasNumber & hasBigLetter;
        }

        public int GetMaxLength() { return user.GetBoard().GetMaxLength(); }

        //task functions
        public bool AddTask(string title, string body, DateTime dueDate)
        {
            BusinessLayer.Task t = user.GetBoard().AddTask(title, body, dueDate);
            if (t != null)
            {
                taskHandler.AddTaskToFile(t.GetBoardId(), t.GetTaskId(), title, body, t.GetCreationDate(), t.GetDueDate(), t.GetColumn());
                return true;
            } 
            return false;
        }

        public bool MoveTask(int taskId)
        {
            bool output = user.GetBoard().MoveTask(taskId, user.GetEmail());
            BusinessLayer.Task t = user.GetBoard().FindTask(taskId, user.GetEmail());
            if(output)
            {
                taskHandler.UpdateTask(t.GetBoardId(), taskId, t.GetTitle(), t.GetBody(), t.GetCreationDate(), t.GetDueDate(), t.GetColumn());
            }
            return output;

        }

        public bool RemoveTask(int taskId)
        {
            bool output = user.GetBoard().RemoveTask(taskId, user.GetEmail());
            BusinessLayer.Task t = user.GetBoard().FindTask(taskId, user.GetEmail());
            if (output)
            {
                taskHandler.RemoveTask(t.GetBoardId(), taskId, t.GetTitle(), t.GetBody(), t.GetCreationDate(), t.GetDueDate(), t.GetColumn());
            }
            return output;
        }

        public string PrintBoard() {
            Board board = user.GetBoard();
            string str = "";
            str += "To Do: \n";
            foreach(KanbanProject.BusinessLayer.Task t in board.GetColumn(1))
            {
                str += t.GetTaskId().ToString() + ". " + t.GetTitle() + ": " + t.GetBody() + "\n" + "Created on: " + t.GetCreationDate() + " , Due to: " + t.GetDueDate() + "\n";
            }
            str += "\n In Progress: \n";
            foreach (KanbanProject.BusinessLayer.Task t in board.GetColumn(2))
            {
                str += t.GetTaskId().ToString() + ". " + t.GetTitle() + ": " + t.GetBody() + "\n" + "Created on: " + t.GetCreationDate() + " , Due to: " + t.GetDueDate() + "\n";
            }
            str += "\n Done: \n";
            foreach (KanbanProject.BusinessLayer.Task t in board.GetColumn(3))
            {
                str += t.GetTaskId().ToString() + ". " + t.GetTitle() + ": " + t.GetBody() + "\n" + "Created on: " + t.GetCreationDate() + " , Due to: " + t.GetDueDate() + "\n";
            }
            return str;
        }
    }
}