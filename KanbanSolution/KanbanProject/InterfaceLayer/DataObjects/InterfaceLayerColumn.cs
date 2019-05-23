using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.InterfaceLayer.DataObjects
{
    public class InterfaceLayerColumn
    {
        public List<InterfaceLayerTask> tasks { get; set; }
        public string colName { get; set; }
        public string userId { get; set; }
        public int colPlace { get; set; }
        public int maxLength { get; set; }

        public InterfaceLayerColumn(List<InterfaceLayerTask> tasks, string colName, string userId, int colPlace, int maxLength)
        {
            this.tasks = tasks;
            this.colName = colName;
            this.userId = userId;
            this.colPlace = colPlace;
            this.maxLength = maxLength;
        }
    }
}
