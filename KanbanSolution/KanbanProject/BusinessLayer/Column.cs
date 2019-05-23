using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.BusinessLayer
{
    public class Column
    {
        private string name;
        private string userId;
        private int place;
        private List<BusinessLayer.Task> list;
        private int maxLength;

        public Column (string name, string userId, int place, int maxLength)
        {
            this.name = name;
            this.userId = userId;
            this.place = place;
            this.list = new List<Task>();
            this.maxLength = maxLength;
        }


        public int GetMaxLength() { return maxLength; }
        public void SetMaxLength(int length) { maxLength = length; }
        public int GetPlace() { return place; }
        public void SetPlace(int place) { this.place = place; }
        public string GetName() { return this.name; }
        public List<BusinessLayer.Task> GetList() { return this.list; }
        public string GetuserId() { return userId; }
        


    }
}
