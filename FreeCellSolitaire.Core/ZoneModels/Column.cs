using FreeCellSolitaire.Core.CardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCellSolitaire.Core.GameModels
{
    public class Column
    {
        public Column(IZone owner, int index, int capacity)
        {
            _owner = owner;
            _index = index;
            _capacity = capacity;
            _cards = new List<CardView>();
        }
        private List<CardView> _cards;
        public bool AddCards(Card card)
        {
            if (_cards.Count >= _capacity)
            {
                return false;
            }
            var cardView = new CardView(this, card);
            _cards.Add(cardView);
            return true;
        }

        private IZone _owner;
        public IZone Owner
        {
            get
            {
                return _owner;
            }
        }

        private int _capacity;
        private int _index;

        public int Index
        {
            get
            {
                return _index;
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{_owner}[{_index}]:");
            foreach (var card in _cards)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.Append($"{card}");
            }
            return sb.ToString();

        }

    }
}
