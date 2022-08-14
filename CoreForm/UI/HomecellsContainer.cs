using CoreForm.UI;
using System.Collections.Generic;

namespace FreeCellSolitaire.UI
{


    public class HomecellsContainer
    {
        private List<HomecellColumnPanel> _columnContainers;
        private IGameForm form;

        public HomecellsContainer()
        {
            _columnContainers = new List<HomecellColumnPanel>(4);
        }

        public HomecellsContainer(IGameForm form)
        {
            this.form = form;
        }
    }
}
