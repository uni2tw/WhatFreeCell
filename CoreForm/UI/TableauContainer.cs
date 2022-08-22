using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreeCellSolitaire.UI
{

    public class GeneralColumnPanel : Panel
    {

    }

    public class TableauColumnPanel : GeneralColumnPanel
    {

    }

    public class TableauContainer : ContainerControl
    {
        private List<TableauColumnPanel> _tableauColumns;
        private IGameForm form;

        public TableauContainer()
        {
            _tableauColumns = new List<TableauColumnPanel>(4);
        }

        public TableauContainer(IGameForm form)
        {
            this.form = form;
        }
    }
}
