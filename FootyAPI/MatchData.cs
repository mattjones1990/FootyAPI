using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FootyAPI
{ 

    public class MatchData
    {
        public List<string> Leagues { get; set; }
        public string ApiKey { get; set; }
        public string Market { get; set; }
        public string Region { get; set; }

        public MatchData()
        {
            Leagues = new List<string>();
            //Leagues.Add("soccer_epl");
            //Leagues.Add("soccer_efl_champ");
            //Leagues.Add("soccer_england_league1");
            //Leagues.Add("soccer_england_league2");

            //Leagues.Add("soccer_fa_cup");
            //Leagues.Add("soccer_france_ligue_one");
            //Leagues.Add("soccer_france_ligue_two");
            Leagues.Add("soccer_germany_bundesliga");
            Leagues.Add("soccer_germany_bundesliga2");
            //Leagues.Add("soccer_italy_serie_a");
            //Leagues.Add("soccer_italy_serie_b");
            //Leagues.Add("soccer_mexico_ligamx");
            //Leagues.Add("soccer_netherlands_eredivisie");
            //Leagues.Add("soccer_norway_eliteserien");
            //Leagues.Add("soccer_portugal_primeira_liga");
            //Leagues.Add("soccer_russia_premier_league");
            //Leagues.Add("soccer_spain_la_liga");
            //Leagues.Add("soccer_spain_segunda_division");
            //Leagues.Add("soccer_spl");
            //Leagues.Add("soccer_uefa_champs_league");
            //Leagues.Add("soccer_uefa_europa_league");
            //Leagues.Add("soccer_usa_mls");


            ApiKey = "829a9b7c834124fa59a207f187f6a6a6";
            Market = "h2h";
            Region = "eu";

        }

        internal void ProcessMatch(MatchObject match)
        {
            var startTimeDouble = Convert.ToDouble(match.CommenceTime);
            var pinnacleLastUpdatedDouble = Convert.ToDouble(match.PinnacleLastUpdated);
            var betfairLastUpdatedDouble = Convert.ToDouble(match.BetfairLastUpdated);

            var startTime = new DateTime(1970, 1, 1).AddSeconds(startTimeDouble);
            var pinnacleLastUpdated = new DateTime(1970, 1, 1).AddSeconds(pinnacleLastUpdatedDouble);
            var betfairLastUpdated = new DateTime(1970, 1, 1).AddSeconds(betfairLastUpdatedDouble);

            if (match.PinnacleHomeOdds > 0.0m && match.BetfairHomeOdds > 0.0m && Convert.ToInt64(match.PinnacleLastUpdated) < Convert.ToInt64(match.CommenceTime))
            {
                decimal vig = CalculateOdds.getVig(match);
                decimal vigHome = CalculateOdds.getVigOdds(match.PinnacleHomeOdds, vig);
                decimal vigDraw = CalculateOdds.getVigOdds(match.PinnacleDrawOdds, vig);
                decimal vigAway = CalculateOdds.getVigOdds(match.PinnacleAwayOdds, vig);

                decimal betfairHome = CalculateOdds.getBetfairOdds(match.BetfairHomeOdds);
                decimal betfairDraw = CalculateOdds.getBetfairOdds(match.BetfairDrawOdds);
                decimal betfairAway = CalculateOdds.getBetfairOdds(match.BetfairAwayOdds);

                decimal valueHome = CalculateOdds.getValueOdds(vigHome, betfairHome);
                decimal valueDraw = CalculateOdds.getValueOdds(vigDraw, betfairDraw);
                decimal valueAway = CalculateOdds.getValueOdds(vigAway, betfairAway);

                if (DatabaseCalls.DoesRecordExist(match, startTime))
                {
                    int matchID = DatabaseCalls.getMatchRecordID(match, startTime);
                    DatabaseCalls.UpdateMatchRecord(matchID, match, vigHome, vigDraw, vigAway, match.BetfairHomeOdds, match.BetfairDrawOdds,
                        match.BetfairAwayOdds, valueHome, valueDraw, valueAway, pinnacleLastUpdated);
                }
                else
                {
                    DatabaseCalls.AddMatchRecord(match, vigHome, vigDraw, vigAway, match.BetfairHomeOdds, match.BetfairDrawOdds,
                        match.BetfairAwayOdds, valueHome, valueDraw, valueAway, pinnacleLastUpdated, startTime);
                    //add it
                    //write to historic table
                }

                //Add to historic record anyway
                DatabaseCalls.AddMatchRecordHistoric(match, vigHome, vigDraw, vigAway, match.BetfairHomeOdds, match.BetfairDrawOdds,
                        match.BetfairAwayOdds, valueHome, valueDraw, valueAway, pinnacleLastUpdated, startTime);
            }
        }

        public List<string> GetMatchData()
        {
            List<string> jsonObjects = new List<string>();

            foreach (var league in Leagues)
            {
                var URL = new UriBuilder("https://api.the-odds-api.com/v3/odds/?apiKey=" +
                    ApiKey +
                    "&sport=" + league +
                    "&region=" +
                    Region + "&mkt=" +
                    Market);

                var client = new WebClient();
                jsonObjects.Add(client.DownloadString(URL.ToString()));
            }

            return jsonObjects;
        }
    }
}
