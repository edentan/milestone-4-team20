

namespace KanbanProject.InterfaceLayer.DataObjects
{
    public class InterfaceLayerTask// : IComparable<InterfaceLayerTask>
    {
        public int taskId { get; set; }
        public string taskTitle { get; set; }
        public string taskBody { get; set; }
        public string creationDate { get; set; }
        public string dueDate { get; set; }
        public string userId { get; set; }
        public string colName { get; set; }

        public InterfaceLayerTask(int taskId, string taskTitle, string taskBody, string creationDate, string dueDate, string userId, string colName)
        {
            this.taskId = taskId;
            this.taskTitle = taskTitle;
            this.taskBody = taskBody;
            this.creationDate = creationDate;
            this.dueDate = dueDate;
            this.userId = userId;
            this.colName = colName;
        }
    }
}
