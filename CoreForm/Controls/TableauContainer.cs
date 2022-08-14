using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCellSolitaire.Controls
{
    public class FoundactionsContainer
    {
        private List<FoundactionColumnContainer> _columnContainers;
        public FoundactionsContainer()
        {
            _columnContainers = new List<FoundactionColumnContainer>(4);
        }
        
    }

    public class GeneralColumnContainer
    {

    }


    public class FoundactionColumnContainer : GeneralColumnContainer
    {

    }

    public class HomecellColumnContainer : GeneralColumnContainer
    {

    }
    public class TableauColumnContainer : GeneralColumnContainer
    {

    }

    public class HomecellsContainer
    {
        private List<HomecellColumnContainer> _columnContainers;
        public HomecellsContainer()
        {
            _columnContainers = new List<HomecellColumnContainer>(4);
        }
    }

    public class TableauContainer
    {
        private List<TableauColumnContainer> _tableauColumns;
        public TableauContainer()
        {
            _tableauColumns = new List<TableauColumnContainer>(4);
        }
    }
}
