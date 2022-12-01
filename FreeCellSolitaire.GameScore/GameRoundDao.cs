using Csv;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCellSolitaire.Data
{
    public class GameRecordDao
    {
        public string DataFolder { get; private set; }

        string[] columnNames = new[] {
                    nameof(GameRecord.Number),
                    nameof(GameRecord.PlayerId),
                    nameof(GameRecord.StarTime),
                    nameof(GameRecord.ElapsedSecs),
                    nameof(GameRecord.MovementAmount),
                    nameof(GameRecord.Tracks),
                    nameof(GameRecord.Success),
                    nameof(GameRecord.Comment),
                    nameof(GameRecord.Sync),
                };

        public GameRecordDao()
        {
            this.DataFolder = Helper.MapPath("data");
            if (System.IO.Directory.Exists(this.DataFolder) == false)
            {
                System.IO.Directory.CreateDirectory(this.DataFolder);
            }
        }

        public bool Save(GameRecord record, Stream stream)
        {
            try
            {                
                List<string[]> rows = new List<string[]>();
                rows.Add(record.ToArray());             
                stream.Seek(0, SeekOrigin.End);
             
                string csv = CsvWriter.WriteToText(columnNames, rows, ',', true);
                byte[] csvBytes = Encoding.UTF8.GetBytes(csv);
                stream.WriteAsync(csvBytes, 0, csvBytes.Length);
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public bool Init(Stream stream)
        {
            try
            {
                List<string[]> rows = new List<string[]>();
                string csv = CsvWriter.WriteToText(columnNames,  rows, ',', false);
                byte[] csvBytes = Encoding.UTF8.GetBytes(csv);
                stream.WriteAsync(csvBytes, 0, csvBytes.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<GameRecord> GetAll(Stream stream)
        {
            List<GameRecord> records = new List<GameRecord>();

            stream.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            string csv = sr.ReadToEnd();

            var csvLines = CsvReader.ReadFromText(csv, new CsvOptions { Separator = ',' });
            foreach(var lines in csvLines)
            {
                try
                {
                    GameRecord record = new GameRecord
                    {
                        Number = int.Parse(lines[nameof(GameRecord.Number)]),
                        PlayerId = lines[nameof(GameRecord.PlayerId)],
                        StarTime = DateTime.Parse(lines[nameof(GameRecord.StarTime)]),
                        ElapsedSecs = double.Parse(lines[nameof(GameRecord.ElapsedSecs)]),
                        MovementAmount = int.Parse(lines[nameof(GameRecord.MovementAmount)]),
                        Tracks = lines[nameof(GameRecord.Tracks)],
                        Success = bool.Parse(lines[nameof(GameRecord.Success)]),
                        Comment = lines[nameof(GameRecord.Comment)],
                        Sync = bool.Parse(lines[nameof(GameRecord.Sync)]),
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

