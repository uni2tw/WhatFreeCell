using CoreForm.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreForm.FreeCell
{
    public interface ICardBase
    {
        int Number { get; }
        CardSuit Suit { get; }

        bool Actived { get; set; }

    }
}
