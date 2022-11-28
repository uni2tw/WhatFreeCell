using System;
using System.Collections.Generic;
using System.IO;

namespace FreeCellSolitaire.Data
{
    public class GameUser
    {
        GameRecordDao _dao;
        string _file;
        List<GameRecord> _records = new List<GameRecord>();
                
        public GameUser(string file)
        {
            _file = file;
            _dao = new GameRecordDao();

            Init();
        }

        private void Init()
        {
            using (FileStream fs = new FileStream(_file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (fs.Length == 0)
                {
                    _dao.Init(fs);
                }
                _records = _dao.GetAll(fs);
            }
        }

        public void AddRecord(GameRecord record)
        {
            using (FileStream fs = new FileStream(_file, FileMode.Open, FileAccess.ReadWrite))
            {
                _dao.Save(record, fs);
            }
        }
        public GameRecordStats GetStats(Guid playedId, int gameNumber)
        {
            return new GameRecordStats();
        }
    }
}

