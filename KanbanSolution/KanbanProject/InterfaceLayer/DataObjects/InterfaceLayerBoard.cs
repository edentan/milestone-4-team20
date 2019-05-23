using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.InterfaceLayer.DataObjects
{
    class InterfaceLayerBoard
    {
            public string userId { get; private set; }
            public int numOfCol{ get; private set; }

    public IReadOnlyCollection<InterfaceLayerColumn> columns { get; private set; }

            public InterfaceLayerBoard(string userId, IReadOnlyCollection<InterfaceLayerColumn> columns, int numOfCol)
            {
                this.userId = userId;
                this.columns = columns;
                this.numOfCol = numOfCol;
            }
        
    }

}

