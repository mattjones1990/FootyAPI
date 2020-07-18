using System;
using System.Collections.Generic;
using System.Text;

namespace FootyAPI
{
    public class MatchObject
    {
        public int MatchID { get; set; }
        public string League { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string CommenceTime { get; set; }
        public string PinnacleLastUpdated { get; set; }
        public decimal PinnacleHomeOdds { get; set; }
        public decimal PinnacleDrawOdds { get; set; }
        public decimal PinnacleAwayOdds { get; set; }
        public string BetfairLastUpdated { get; set; }
        public decimal BetfairHomeOdds { get; set; }
        public decimal BetfairDrawOdds { get; set; }
        public decimal BetfairAwayOdds { get; set; }
        public int Completed { get; set; }
    }
}
