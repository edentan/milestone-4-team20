using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.BusinessLayer;


namespace KanbanProject
{
    public class User
    {
        private string email;
        private string password;
        private List<User> allUsers;
        private string boardId;
        private Board board;

        public User()
        {
            email = null;
            password = null;
            board = null;
            boardId = null;
            allUsers = new List<User>();
        }

        public User(string email, string password, int length)
        {
             this.email = email;
             this.password = password;
             boardId = email;
             board = new Board(length, email);
        }

        public string GetEmail() { return email; }
        /*     public void SetEmail(string email) {
            this.email = email;
        }*/

        public string GetPassword() { return password; }
        /*public void SetPassword(string password) {
            this.password = password;
        }*/

        public Board GetBoard() { return board; }

        public List<User> GetAllUsers() { return allUsers; }
        public void SetAllUsers(List<User> users) { allUsers = users; }

        
    }  
}