using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootyAPI
{
    public class JsonFactory
    {
        internal List<MatchObject> GetMatchesForLeague(string league)
        {
            var data = JObject.Parse(league)["data"];
            dynamic json = JsonConvert.SerializeObject(data);
            dynamic newJson = JsonConvert.DeserializeObject(json);
            List<MatchObject> MatchObject = new List<MatchObject>();

            foreach (var item in newJson)
            {
                MatchObject matchObject = new MatchObject();
                matchObject.League = item.sport_key;
                matchObject.CommenceTime = item.commence_time;
                string homeTeam = item.home_team;

                var teams = item.teams;
                var teamOne = teams[0].ToObject<string>();
                var teamTwo = teams[1].ToObject<string>();

                int homeTeamOdds = 0;
                int awayTeamOdds = 0;

                if (homeTeam == teamOne)
                {
                    matchObject.HomeTeam = teamOne;
                    matchObject.AwayTeam = teamTwo;

                    homeTeamOdds = 0;
                    awayTeamOdds = 1;
                } 
                else
                {
                    matchObject.HomeTeam = teamTwo;
                    matchObject.AwayTeam = teamOne;

                    homeTeamOdds = 1;
                    awayTeamOdds = 0;
                }

                var odds = item.sites;

                foreach (var odd in odds)
                {
                    if (odd.site_key.ToObject<string>() == "pinnacle")
                    {
                        matchObject.PinnacleLastUpdated = odd.last_update.ToObject<string>();
                        var pinnacleOdds = odd.odds;
                        var exactOdds = pinnacleOdds.h2h;

                        matchObject.PinnacleHomeOdds = exactOdds[homeTeamOdds].ToObject<decimal>();
                        matchObject.PinnacleAwayOdds = exactOdds[awayTeamOdds].ToObject<decimal>();
                        matchObject.PinnacleDrawOdds = exactOdds[2].ToObject<decimal>();
                    }
                }

                foreach (var odd in odds)
                {
                    if (odd.site_key.ToObject<string>() == "betfair")
                    {
                        matchObject.BetfairLastUpdated = odd.last_update.ToObject<string>();
                        var betfairOdds = odd.odds;
                        var exactOdds = betfairOdds.h2h;

                        matchObject.BetfairHomeOdds = exactOdds[homeTeamOdds].ToObject<decimal>();
                        matchObject.BetfairAwayOdds = exactOdds[awayTeamOdds].ToObject<decimal>();
                        matchObject.BetfairDrawOdds = exactOdds[2].ToObject<decimal>();
                    }
                }

                //Console.WriteLine(matchObject);
                MatchObject.Add(matchObject);
            }           
            return MatchObject;
        }
    }
}
