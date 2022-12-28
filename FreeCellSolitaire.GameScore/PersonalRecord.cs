using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FreeCellSolitaire.Data
{
    public class PersonalRecord
    {
        GameRecordDao _dao;
        string _file;
        List<GameRecord> _records = new List<GameRecord>();
        private string _playerId = null;
        public string PlayerId { get { return _playerId; } }
                
        public PersonalRecord(string file)
        {
            _file = file;
            _dao = new GameRecordDao();

            Init();
        }

        private void Init()
        {
            if (string.IsNullOrEmpty(_file) || File.Exists(_file) == false)
            {
                return;
            }
            using (FileStream fs = new FileStream(_file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (fs.Length == 0)
                {
                    _dao.Init(fs);
                }
                _records = _dao.GetAll(fs);
                if (_records.Count == 0)
                {
                    _playerId = Guid.NewGuid().ToString("N").ToLower();
                }
                else
                {
                    _playerId = _records.First().PlayerId;
                }
            }
            //HashSet<string> recordHash = new HashSet<string>();
            //List<GameRecord> distinctRecords = new List<GameRecord>();
            //foreach(var rec in _records)
            //{
            //    if (recordHash.Add(rec.ToString()))
            //    {
            //        distinctRecords.Add(rec);
            //    }
            //}
            //_records = distinctRecords;
            //_records.ForEach(x => x.IsNewRecord = true);
        }

        public GameRecord AddRecord(int number, DateTime startTime, double elapsedSecs, int movementAmount, bool success, string track, string comment)
        {
            var record = new GameRecord
            {
                Number = number,
                Comment = comment,
                ElapsedSecs = elapsedSecs,
                PlayerId = this._playerId,
                StarTime = startTime,
                MovementAmount = movementAmount,
                Success = success,
                Sync = false,
                Tracks = track,
                IsNewRecord = true
            };
            _records.Add(record);
            return record;
        }
        public void Save()
        {
            using (FileStream fs = new FileStream(_file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                foreach (var record in _records)
                {
                    _dao.Save(record, fs);
                }
            }
            _records.Clear();
        }
        public GameRecordSummary GetSummary(int gameNumber)
        {            
            int winningStreak = 0;
            int losingStreak = 0;
            int sessionWinInRow = 0;
            int sessionLostInRow = 0;
            int theGameWin = 0;
            int theGameLost = 0;
            int totalGameWin = 0;
            int totalGameLost = 0;            
            int lastStreak = 0;
            foreach (var r in _records)
            {
                if (r.Success)
                {
                    sessionWinInRow++;
                    if (winningStreak < sessionWinInRow)
                    {
                        winningStreak = sessionWinInRow;
                    }                    
                    sessionLostInRow = 0;
                    totalGameWin++;
                    if (r.Number == gameNumber)
                    {
                        theGameWin++;
                    }
                }
                else
                {
                    sessionLostInRow++;
                    if (losingStreak < sessionLostInRow)
                    {
                        losingStreak = sessionLostInRow;
                    }
                    sessionWinInRow = 0;
                    totalGameLost++;
                    if (r.Number == gameNumber)
                    {
                        theGameLost++;
                    }
                }                
            }
            lastStreak = sessionWinInRow > 0 ? sessionWinInRow : sessionLostInRow > 0 ? (-1 * sessionLostInRow) : 0;
            
            var bestRecordTheGame = _records.Where(x => x.Number == gameNumber && x.Success)
                .OrderBy(x => x.MovementAmount).FirstOrDefault();

            return new GameRecordSummary
            {
                LosingStreak = losingStreak,
                WinningStreak = winningStreak,
                TimesWonThisGame = theGameWin,
                TimesLostThisGame = theGameLost,
                TotalTimesLost = totalGameLost,
                TotalTimesWon = totalGameWin,
                RecentWinningOrLosingStreak = lastStreak,
                BestRecordOfTheGame = bestRecordTheGame
            };
            
        }

        public void DeleteAll()
        {
            _records.Clear();
            using (FileStream fs = new FileStream(_file, FileMode.Create, FileAccess.ReadWrite))
            {
                _dao.Init(fs);
            }
        }
    }
}

