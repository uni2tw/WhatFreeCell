using Csv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCellSolitaire.Data
{
    public class GameRoundDao
    {
        public string DataFolder { get; private set; }
        public GameRoundDao()
        {
            this.DataFolder = Helper.MapPath("data");
            if (System.IO.Directory.Exists(this.DataFolder) == false)
            {
                System.IO.Directory.CreateDirectory(this.DataFolder);
            }
        }
        /*
    public class GameRecord
    {
        public int Id { get; set; }
        public int Number { get; set; }        
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public DateTime StarTime { get; set; }
        public double ElapsedSecs { get; set; }
        public int MovementAmount { get; set; }
        public string Tracks { get; set; }
        public bool Success { get; set; }
        public int? Score { get; set; }
        public string Comment { get; set; }        
        public bool Sync { get; set; }
    }
         */
        public bool Save(GameRecord model)
        {
            try
            {
                var columnNames = new[] {
                    nameof(GameRecord.Number),
                    nameof(GameRecord.PlayerId),
                    nameof(GameRecord.PlayerName),
                    nameof(GameRecord.StarTime),
                    nameof(GameRecord.ElapsedSecs),
                    nameof(GameRecord.MovementAmount),
                    nameof(GameRecord.Tracks),
                    nameof(GameRecord.Success),
                    nameof(GameRecord.Comment),
                    nameof(GameRecord.Sync),
                };
                var rows = new []
                {
                    model.ToArray()
                };

                string csv;
                if (System.IO.File.Exists("record.csv") == false)
                {
                    csv = CsvWriter.WriteToText(columnNames, rows, ',', true);
                }
                else
                {
                    csv = CsvWriter.WriteToText(columnNames, rows, ',', false);
                }
                File.AppendAllText("record.csv", csv);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<GameRecord> List()
        {
            List<GameRecord> records = new List<GameRecord>();
            var columnNames = new[] {
                    nameof(GameRecord.Number),
                    nameof(GameRecord.PlayerId),
                    nameof(GameRecord.PlayerName),
                    nameof(GameRecord.StarTime),
                    nameof(GameRecord.ElapsedSecs),
                    nameof(GameRecord.MovementAmount),
                    nameof(GameRecord.Tracks),
                    nameof(GameRecord.Success),
                    nameof(GameRecord.Comment),
                    nameof(GameRecord.Sync),
                };

            
            string csv = File.ReadAllText("record.csv");

            var csvLines = CsvReader.ReadFromText(csv, new CsvOptions { Separator = ',' });
            foreach(var lines in csvLines)
            {
                try
                {
                    GameRecord record = new GameRecord
                    {
                        Number = int.Parse(lines[nameof(GameRecord.Number)]),
                        PlayerId = nameof(GameRecord.PlayerId),
                        PlayerName = lines[nameof(GameRecord.PlayerName)],
                        StarTime = DateTime.Parse(lines[nameof(GameRecord.StarTime)]),
                        ElapsedSecs = double.Parse(lines[nameof(GameRecord.StarTime)]),
                        MovementAmount = int.Parse(lines[nameof(GameRecord.StarTime)]),
                        Tracks = lines[nameof(GameRecord.StarTime)],
                        Success = bool.Parse(lines[nameof(GameRecord.StarTime)]),
                        Comment = lines[nameof(GameRecord.StarTime)],
                        Sync = bool.Parse(lines[nameof(GameRecord.StarTime)]),
                    };
                    records.Add(record);
                } 
                catch
                {

                    throw new Exception("資料毀損或其它不明原因，無法解析");
                }
            }
            return records;
        }
    }
}

