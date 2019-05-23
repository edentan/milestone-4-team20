using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.InterfaceLayer.DataObjects;


namespace KanbanProject.PresentationLayer.ViewModel
{

    public class KanbanTask// : IComparable<KanbanTask>
    {
     
        public string ColName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string CreationDate { get; set; }
        public int Id { get; set; }

        public KanbanTask(string colName, int taskId, string title, string description, string dueDate, string creationDate)
        {
            Id = taskId;
            ColName = colName;
            Title = title;
            Description = description;
            DueDate = dueDate;
            CreationDate = creationDate;
        }
    }
}
