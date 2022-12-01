using System;
using System.Text;
using System.Threading;

namespace FreeCellSolitaire.Data
{
    public class GameRecordSummary
    {
        public int TimesWonThisGame { get; set; }
        public int TimesLostThisGame { get; set; }
        public int TotalTimesWon { get; set; }
        public int TotalTimesLost { get; set; }
        public int WinningStreak { get; set; }
        public int LosingStreak { get; set; }
        public int RecentWinningOrLosingStreak { get; set; }
    }
}

