
using System.ComponentModel;
using KanbanProject.InterfaceLayer;


namespace KanbanProject.PresentationLayer.ViewModel
{
    class UserBoardsDataContext
    {
        Service service;

        public UserBoardsDataContext(Service service)
        {
            this.service = service;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private ICollectionView gridView;
        public ICollectionView GridView
        {
            get
            {
                return gridView;
            }
            set
            {
                gridView = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GridView"));
            }
        }

        private KanbanTask selected;
        public KanbanTask Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
            }
        }
    }
}
