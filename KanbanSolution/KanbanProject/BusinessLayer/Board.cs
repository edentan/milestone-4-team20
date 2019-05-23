using System;
using System.IO;
using System.Collections.Generic;


namespace KanbanProject.BusinessLayer
{
    public class Board
    {
        private string boardId;
        private int maxLength;
        private List<BusinessLayer.Task> toDo;
        private List<BusinessLayer.Task> inProgress;
        private List<BusinessLayer.Task> done;
        private int taskCounter;


        public Board(int length, string id)
        {
            maxLength = length;
            toDo = new List<Task>();
            inProgress = new List<Task>();
            done = new List<Task>();
            taskCounter = 0;
            boardId = id;
        }

        public int GetMaxLength() { return maxLength; }
        public void SetMaxLength(int length) { maxLength = length; }

        public List<BusinessLayer.Task> GetColumn(int n)
        {
            if (n == 1)
                return toDo;
            else if (n == 2)
                return inProgress;
            else
                return done;
        }

        public string GetBoardId() { return boardId; }

        public BusinessLayer.Task AddTask(string title, string body, DateTime dueDate)
        {
            if (toDo.Count >= maxLength | (title.Length < 1 | title.Length > 50) | (body.Length > 300) | (dueDate.Date.CompareTo(DateTime.Now) < 0))
                return null;
            else
            {
                int taskId = taskCounter + 1;
                BusinessLayer.Task t = new BusinessLayer.Task(title, body, DateTime.Now, dueDate, taskId, boardId, 1);
                toDo.Add(t);
                taskCounter++;
                return t;
            }
        }

        public bool MoveTask(int taskId, string boardId)
        {
            BusinessLayer.Task t = FindTask(taskId, boardId);
            if (t == null)
                return false;
            else { 
            int col = t.GetColumn();
                if (col == 3)
                    return false;
                else
                {
                    if (col == 1)
                    {
                        t.SetColumn(2);
                        inProgress.Add(t);
                        toDo.Remove(t);
                    }
                    else
                    {
                        t.SetColumn(3);
                        done.Add(t);
                        inProgress.Remove(t);
                    }
                    return true;
                }
            }
        }

        public bool RemoveTask(int taskId, string boardId)
        {
            BusinessLayer.Task t = FindTask(taskId, boardId);
            if (t == null)
                return false;
            else
            {
                int col = t.GetColumn();
                if (col == 1)
                    toDo.Remove(t);
                else if (col == 2)
                    inProgress.Remove(t);
                else
                    done.Remove(t);
                return true;
            }
        }

        public BusinessLayer.Task FindTask(int taskId, string boardId)
        {
            foreach(BusinessLayer.Task t in toDo)
            {
                if (t.GetTaskId() == taskId & t.GetBoardId() == boardId)
                    return t;
            }
            foreach(BusinessLayer.Task t in inProgress)
            {
                if (t.GetTaskId() == taskId & t.GetBoardId() == boardId)
                    return t;
            }
            foreach (BusinessLayer.Task t in done)
            {
                if (t.GetTaskId() == taskId & t.GetBoardId() == boardId)
                    return t;
            }
            return null;
        }

        public void SortCols(List<BusinessLayer.Task> tasks)
        {
            foreach(BusinessLayer.Task t in tasks)
            {
                if (t.GetBoardId() == boardId)
                {
                    if (t.GetColumn() == 1)
                        toDo.Add(t);
                    else if (t.GetColumn() == 2)
                        inProgress.Add(t);
                    else
                        done.Add(t);
                }
            }

        }
    }
}
