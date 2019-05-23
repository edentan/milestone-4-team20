

namespace KanbanProject.PresentationLayer.ViewModel
{
    class KanbanColumn
    {
        public int Place { get; set; }
        public string Name { get; set; }
        public string TasksLimit { get; set; }

        public KanbanColumn(int colPlace, string colName, string maxTasks)
        {
            Name = colName;
            Place = colPlace;
            TasksLimit = maxTasks;
        }

    }
}
