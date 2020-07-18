using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace FootyAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            MatchData md = new MatchData();
            List<string> leagueData = md.GetMatchData();
            List<MatchObject> matchObjects = new List<MatchObject>();

            foreach (var league in leagueData)
            {
                JsonFactory js = new JsonFactory();

                //delete all from match
                DatabaseCalls.DeleteMatchRecords();
                System.Threading.Thread.Sleep(1000);
                List<MatchObject> leagueMatches = js.GetMatchesForLeague(league);

                foreach (var match in leagueMatches)
                {
                    md.ProcessMatch(match);
                }
            }
        }
    }
}

