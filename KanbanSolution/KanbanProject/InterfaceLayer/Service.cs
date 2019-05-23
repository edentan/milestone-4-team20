using System;
using System.Collections.Generic;
using KanbanProject.InterfaceLayer.DataObjects;
using KanbanProject.BusinessLayer;

namespace KanbanProject.InterfaceLayer
{
    public class Service
    {
        private KanbanBoard kanban;

        public Service()
        {
            kanban = new KanbanBoard();
        }

        public KanbanBoard GetKanban() { return kanban; }

        public string GetuserId() { return kanban.GetUser().GetEmail(); }

        public bool RemoveColumn(string name)
        {
            if (kanban.RemoveColumn(name))
                return true;
            return false;
        }

        public void UploadData() { kanban.UploadData(); }

        public bool LogIn(string email, string password) { return kanban.LogIn(email, password); }

        public bool IsUniqueEmail(string email) { return kanban.IsUniqueEmail(email); }

        public bool IsValidEmail(string email) { return kanban.IsValidEmail(email); }

        public bool IsValidPassword(string password) { return kanban.IsValidPassword(password); }

        public void Register(string email, string password) { kanban.Register(email, password); }

        public InterfaceLayerColumn GetColumn(string colName)
        {
            List<InterfaceLayerTask> tasks = new List<InterfaceLayerTask>();
            int colPlace = 0;
            int maxLength = 0;
            foreach (Column c in kanban.GetUser().GetBoard().GetColumns())
            {
                if (c.GetName().Equals(colName))
                {
                    colPlace = c.GetPlace();
                    maxLength = c.GetMaxLength();
                    foreach (BusinessLayer.Task t in c.GetList())
                    {
                        string str1 = t.GetCreationDate().ToString();
                        string str2 = t.GetDueDate().ToString();
                        InterfaceLayerTask temp = new InterfaceLayerTask(t.GetTaskId(), t.GetTitle(), t.GetBody(), str1, str2, t.GetuserId(), t.GetColumn());
                        tasks.Add(temp);
                    }
                }
            }
            InterfaceLayerColumn col = new InterfaceLayerColumn(tasks, colName, GetuserId(), colPlace, maxLength);
            return col;
        }

        public InterfaceLayerColumn GetColumn(int colPlace)
        {
            List<InterfaceLayerTask> tasks = new List<InterfaceLayerTask>();
            string colName = "";
            int maxLength = 0;
            foreach (Column c in kanban.GetUser().GetBoard().GetColumns())
            {
                if (c.GetPlace() == colPlace)
                {
                    colName = c.GetName();
                    maxLength = c.GetMaxLength();
                    foreach (BusinessLayer.Task t in c.GetList())
                    {
                        string str1 = t.GetCreationDate().ToString();
                        string str2 = t.GetDueDate().ToString();
                        InterfaceLayerTask temp = new InterfaceLayerTask(t.GetTaskId(), t.GetTitle(), t.GetBody(), str1, str2, t.GetuserId(), t.GetColumn());
                        tasks.Add(temp);
                    }
                }
            }
            InterfaceLayerColumn col = new InterfaceLayerColumn(tasks, colName, GetuserId(), colPlace, maxLength);
            return col;
        }

        public List<InterfaceLayerColumn> GetBoard()
        {
            List<InterfaceLayerColumn> allCols = new List<InterfaceLayerColumn>();

            foreach (Column c in kanban.GetUser().GetBoard().GetColumns())
            {
                InterfaceLayerColumn col = GetColumn(c.GetName());
                allCols.Add(col);
            }
            return allCols;
        }

        public bool AddTask(string title, string body, DateTime dueDate)
        {
            if (kanban.AddTask(title, body, dueDate))
                return true;
            return false;

        }

        public InterfaceLayerTask FindTask(int taskId)
        {
            if (kanban.FindTask(taskId))
            {
                foreach (InterfaceLayerColumn c in GetBoard())
                {
                    foreach (InterfaceLayerTask t in c.tasks)
                    {
                        if (t.taskId == taskId)
                            return new InterfaceLayerTask(taskId, t.taskTitle, t.taskBody, t.creationDate, t.dueDate, t.userId, t.colName);
                    }
                }
            }
            return null;
        }

        public bool UpdateTask(int taskId, string title, string body, DateTime dueDate, bool toMove)
        {
            bool edited = kanban.EditTask(taskId, title, body, dueDate);
            bool moved = true;
            if (toMove)
            {
                moved = kanban.MoveTask(taskId);
            }
            return edited & moved;
        }

        public bool RemoveTask(int taskId)
        {
            if (kanban.RemoveTask(taskId))
                return true;
            return false;
        }

        public bool SetColumnMax(string name, int max)
        {
            if (kanban.SetColumnMax(name, max))
                return true;
            return false;

        }

        public bool MoveForward(int place)
        {
            return kanban.MoveForward(place);
        }

        public bool MoveBack(int place)
        {
            return kanban.MoveBack(place);
        }

        public bool IsLastColumn(int place)
        {
            return kanban.IsLastColumn(place);
        }

        public bool AddColumn(string name)
        {
            if (name.Equals(""))
                return false;
            kanban.AddColumn(name);
            return true;
        }

        public List<InterfaceLayerColumn> SortByDueDate()
        {
            List<InterfaceLayerColumn> board = new List<InterfaceLayerColumn>();
            foreach (InterfaceLayerColumn col in GetBoard())
            {
                List<InterfaceLayerTask> newCol = new List<InterfaceLayerTask>();
                InterfaceLayerTask[] tasks = col.tasks.ToArray();
                if (tasks.Length > 0)
                {
                    for (int i = 0; i < tasks.Length; i++)
                    {
                        for (int j = i; j < tasks.Length; j++)
                        {
                            if (tasks[i].dueDate.CompareTo(tasks[j].dueDate) > 0)
                            {
                                InterfaceLayerTask temp = tasks[i];
                                tasks[i] = tasks[j];
                                tasks[j] = temp;
                            }
                        }
                    }
                }
                for (int i = 0; i < tasks.Length; i++)
                {
                    newCol.Add(tasks[i]);
                }
                board.Add(new InterfaceLayerColumn(newCol, col.colName, col.userId, col.colPlace, col.maxLength));
            }
            return board;
        }

        public List<InterfaceLayerColumn> SortByCreationDate()
        {
            List<InterfaceLayerColumn> board = new List<InterfaceLayerColumn>();
            foreach (InterfaceLayerColumn col in GetBoard())
            {
                List<InterfaceLayerTask> newCol = new List<InterfaceLayerTask>();
                InterfaceLayerTask[] tasks = col.tasks.ToArray();
                if (tasks.Length > 0)
                {
                    for (int i = 0; i < tasks.Length; i++)
                    {
                        for (int j = i; j < tasks.Length; j++)
                        {
                            if (tasks[i].creationDate.CompareTo(tasks[j].creationDate) > 0)
                            {
                                InterfaceLayerTask temp = tasks[i];
                                tasks[i] = tasks[j];
                                tasks[j] = temp;
                            }
                        }
                    }
                }
                for (int i = 0; i < tasks.Length; i++)
                {
                    newCol.Add(tasks[i]);
                }
                board.Add(new InterfaceLayerColumn(newCol, col.colName, col.userId, col.colPlace, col.maxLength));
            }
            return board;
        }

    }
}
