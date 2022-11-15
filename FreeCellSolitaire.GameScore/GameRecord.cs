using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FreeCellSolitaire.Data
{
    public class GameRecord
    {
        public int Number { get; set; }        
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
        public DateTime StarTime { get; set; }
        public double ElapsedSecs { get; set; }
        public int MovementAmount { get; set; }
        public string Tracks { get; set; }
        public bool Success { get; set; }
        public string Comment { get; set; }        
        public bool Sync { get; set; }

        //public void SetComment(string comment)
        //{
        //    this.Comment = Convert.ToBase64String(Encoding.UTF8.GetBytes(comment));
        //}

        //public string GetComment()
        //{
        //    return Encoding.UTF8.GetString(Convert.FromBase64String(Comment));
        //}

        public string[] ToArray()
        {
            return new string[]
            {                
                Number.ToString(),
                PlayerId,
                PlayerName,
                StarTime.ToString("o"),
                ElapsedSecs.ToString(),
                MovementAmount.ToString(),
                Tracks.ToString(),
                Success.ToString(),
                Comment ,
                Sync.ToString()
            };            
        }
    }
}
