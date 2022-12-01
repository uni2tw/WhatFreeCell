using FreeCellSolitaire.Data;
using Microsoft.VisualBasic;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace FreeCellSolitaire.Tests.GameScore
{
    public class GameUserTests
    {
        string playerId;
        [SetUp]
        public void Setup()
        {
            playerId = Guid.NewGuid().ToString();
        }

        [Test]
        public void Test()
        {
            PersonalRecord pr = new PersonalRecord(null);


            pr.AddRecord(
                number: 1,
                startTime: DateTime.Now,
                elapsedSecs: 60,
                movementAmount: 60,
                success: false,
                track: "",
                comment: "");

            pr.AddRecord(
                number: 1,
                startTime: DateTime.Now,
                elapsedSecs: 60,
                movementAmount: 60,
                success: true,
                track: "",
                comment: "");

            pr.AddRecord(
                number: 1,
                startTime: DateTime.Now,
                elapsedSecs: 60,
                movementAmount: 60,
                success: true,
                track: "",
                comment: "");

            pr.AddRecord(
                number: 1,
                startTime: DateTime.Now,
                elapsedSecs: 60,
                movementAmount: 60,
                success: true,
                track: "",
                comment: "");


            pr.AddRecord(
                number: 2,
                startTime: DateTime.Now,
                elapsedSecs: 60,
                movementAmount: 60,
                success: false,
                track: "",
                comment: "");

            pr.AddRecord(
                number: 1,
                startTime: DateTime.Now,
                elapsedSecs: 60,
                movementAmount: 60,
                success: false,
                track: "",
                comment: "");


            var summary = pr.GetSummary(1);
            Assert.AreEqual(3, summary.WinningStreak);
            Assert.AreEqual(2, summary.LosingStreak);
            Assert.AreEqual(3, summary.TimesWonThisGame);
            Assert.AreEqual(2, summary.TimesLostThisGame);
            Assert.AreEqual(3, summary.TotalTimesWon);
            Assert.AreEqual(3, summary.TotalTimesLost);
            Assert.AreEqual(-2, summary.RecentWinningOrLosingStreak);
        }

    }
    public class GameRecordDaoTests
    {
        GameRecord rec1;
        GameRecord rec2;
        [SetUp]
        public void Setup()
        {            
            rec1 = new GameRecord
            {
                Number = 3,
                Comment = "也\"沒\"什麼'想'說的",
                ElapsedSecs = 60.5,
                PlayerId = Guid.NewGuid().ToString(),
                StarTime = new DateTime(2022, 11, 10, 0, 0, 0),
                MovementAmount = 60,
                Success = false,
                Sync = false,
                Tracks = "t0f0,t0f1,t0f2,t0f3"
            };
            rec2  =new GameRecord
            {
                Number = 3,
                Comment = "說不完的,不說",
                ElapsedSecs = 40.5,
                PlayerId = Guid.NewGuid().ToString(),
                StarTime = new DateTime(2022, 11, 11, 11, 11, 11),
                MovementAmount = 70,
                Success = false,
                Sync = false,
                Tracks = "t0f0,t0f1,t0f2,t0f3,t0t1"
            };
        }
        [Test]
        public void WriteAndReadAndAppend()
        {
            var dao = new GameRecordDao();
            MemoryStream ms = new MemoryStream();
            dao.Init(ms);
            string text = Encoding.UTF8.GetString(ms.ToArray());
            Console.WriteLine(text);

            dao.Save(rec1, ms);
            dao.Save(rec2, ms);

            
            List<GameRecord> items = dao.GetAll(ms);

            Assert.AreEqual(2, items.Count);

            List<GameRecord> records = new List<GameRecord>();
            records.Add(rec1);
            records.Add(rec2);
            Assert.AreEqual(JsonSerializer.Serialize(records),
                JsonSerializer.Serialize(items));


            var rec3 = new GameRecord
            {
                Number = 4,
                Comment = "test append",
                ElapsedSecs = 50,
                PlayerId = Guid.NewGuid().ToString(),
                StarTime = new DateTime(2022, 12, 12, 0, 0, 0),
                MovementAmount = 70,
                Success = false,
                Sync = false,
                Tracks = "t0f0,t0f1,t0f2,t0f3,t0t1"
            };

            dao.Save(rec3, ms);

            Assert.AreEqual(3, dao.GetAll(ms).Count);
        }
    }
}
