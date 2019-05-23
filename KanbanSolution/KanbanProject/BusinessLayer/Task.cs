using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.BusinessLayer;

namespace KanbanProject.BusinessLayer
{
    public class Task
    {
        private string title;
        private string body;
        private DateTime creationDate;
        private DateTime dueDate;
        private int column; //1: to do, 2: in progress, 3: done.
        private int taskId;
        private string boardId;
       // private List<BusinessLayer.Task> allTasks;

        public Task(string title, string body, DateTime creationDate, DateTime dueDate, int taskId, string boardId, int col)
        {
            this.title = title;
            this.body = body;
            this.creationDate = creationDate;
            this.dueDate = dueDate;
            this.taskId = taskId;
            this.boardId = boardId;
            column = col;
        }


        public int GetTaskId() { return taskId; }
        public string GetBoardId() { return boardId; }

        public DateTime GetDueDate() { return dueDate; }
        public void SetDueDate(DateTime date) {
            if (date.Date.CompareTo(DateTime.Now) > 0)
                dueDate = date;
        }

        public DateTime GetCreationDate() { return creationDate; }

        public string GetTitle() { return title; }
        public void SetTitle(string title) {
            if (title.Length < 1 | title.Length > 50)
            {
                //ERROR
            }

           else
                this.title = title;
        }

        public string GetBody() { return body; }
        public void SetBody(string body)
        {
            if (body.Length > 300)
            { //ERROR "Task body should have up to 300 characters"
            }
            else
                this.body = body;
        }

        public int GetColumn() { return column; }
        public bool SetColumn(int n)
        {
            if(n == 1 | n == 2)
            {
                if (n == 1)
                    column = 2;
                else
                    column = 3;
                return true;
            }
            return false;
        }

     //   public void SetAllTasks(List<BusinessLayer.Task> tasks) { allTasks = tasks; }
    }
}
