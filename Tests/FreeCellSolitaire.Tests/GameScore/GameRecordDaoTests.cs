using FreeCellSolitaire.Data;
using System.Text;
using System.Text.Json;

namespace FreeCellSolitaire.Tests.GameScore
{
    public class GameRecordDaoTests
    {
        List<GameRecord> records = new List<GameRecord>();
        [SetUp]
        public void Setup()
        {            
            records.Add(new GameRecord
            {
                Number = 3,
                Comment = "也\"沒\"什麼'想'說的",
                ElapsedSecs = 60.5,
                PlayerId = Guid.NewGuid().ToString(),
                PlayerName = "測試員",
                StarTime = new DateTime(2022, 11, 10, 0, 0, 0),
                MovementAmount = 60,
                Success = false,
                Sync = false,
                Tracks = "t0f0,t0f1,t0f2,t0f3"
            });
            records.Add(new GameRecord
            {
                Number = 3,
                Comment = "說不完的,不說",
                ElapsedSecs = 40.5,
                PlayerId = Guid.NewGuid().ToString(),
                PlayerName = "測試員2",
                StarTime = new DateTime(2022, 11, 11, 11, 11, 11),
                MovementAmount = 70,
                Success = false,
                Sync = false,
                Tracks = "t0f0,t0f1,t0f2,t0f3,t0t1"
            });
        }
        [Test]
        public void WriteAndReadAndAppend()
        {            
            MemoryStream ms = new MemoryStream();
            new GameRecordDao().WriteTo(records, ms, false);
            string text = Encoding.UTF8.GetString(ms.ToArray());
            Console.WriteLine(text);


            var textBytes = Encoding.UTF8.GetBytes(text);
            MemoryStream ms2 = new MemoryStream(textBytes);
            ms2.Position = 0;
            List<GameRecord> items = new GameRecordDao().ReadFrom(ms2);

            Assert.AreEqual(2, items.Count);

            Assert.AreEqual(JsonSerializer.Serialize(records),
                JsonSerializer.Serialize(items));




        }
    }
}
