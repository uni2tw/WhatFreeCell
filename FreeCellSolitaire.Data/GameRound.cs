using System;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FreeCellSolitaire.Data
{
    public class GameRecord
    {
        public int Id { get; set; }
        public int RoundNo { get; set; }
        public int MoveNumber { get; set; }
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public DateTime StarTime { get; set; }
        public double ElapsedSecs { get; set; }
        public bool Success { get; set; }
        public int? Score { get; set; }
        public string Comment { get; set; }
        public bool Sync { get; set; }        
    }
}
