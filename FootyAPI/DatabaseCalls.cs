using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FootyAPI
{
    public class DatabaseCalls
    {
        internal const string connectionString = @"Data Source=localhost;Initial Catalog=MattDatabase;Integrated Security=True";
        internal static bool DoesRecordExist(MatchObject match, DateTime commenceTime)
        {
            var sql = ("SELECT * FROM Match WHERE HomeTeam = '" + match.HomeTeam + "' AND AwayTeam = '" + match.AwayTeam + "' AND CommenceTime = CONVERT(DATETIME2,'" + commenceTime + "',105)");
            SqlConnection con;
            SqlCommand command;
            SqlDataReader reader;
            con = new SqlConnection(connectionString);
            con.Open();
            command = new SqlCommand(sql, con);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                return true;
            }

            return false;
        }

        public static void DeleteMatchRecords()
        {
            var sql = ("DELETE FROM MATCH");
            SqlConnection con;
            SqlCommand command;
            con = new SqlConnection(connectionString);
            con.Open();
            command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
        }

        internal static int getMatchRecordID(MatchObject match, DateTime commenceTime)
        {
            var sql = ("SELECT MatchID FROM Match WHERE HomeTeam = '" + match.HomeTeam + "' AND AwayTeam = '" + match.AwayTeam + "' AND CommenceTime = CONVERT(DATETIME2,'" + commenceTime + "',105)");
            SqlConnection con;
            SqlCommand command;
            SqlDataReader reader;
            con = new SqlConnection(connectionString);
            con.Open();
            command = new SqlCommand(sql, con);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                return reader.GetInt32(0);
            }

            return 0;
        }

        internal static void UpdateMatchRecord(int matchID, MatchObject match, decimal vigHome, decimal vigDraw, decimal vigAway, decimal betfairHome, decimal betfairDraw, decimal betfairAway, decimal valueHome, decimal valueDraw, decimal valueAway, DateTime pinnacleLastUpdated)
        {
            var sql = ("UPDATE Match SET " + 
                "LastUpdated = CONVERT(DATETIME2,'" + pinnacleLastUpdated + "',105), " + 
                "PinnacleHome = '" + match.PinnacleHomeOdds + "'," +
                "PinnacleDraw = '" + match.PinnacleDrawOdds + "'," +
                "PinnacleAway = '" + match.PinnacleAwayOdds + "'," +
                "VigFreeHome = '" + vigHome + "'," +
                "VigFreeDraw = '" + vigDraw + "'," +
                "VigFreeAway = '" + vigAway + "'," +
                "BetfairHome = '" + betfairHome + "'," +
                "BetfairDraw = '" + betfairDraw + "'," +
                "BetfairAway = '" + betfairAway + "'," +
                "ValueHome = '" + valueHome + "'," +
                "ValueDraw = '" + valueDraw + "'," +
                "ValueAway = '" + valueAway + "'" +
                "WHERE MatchID = '" + matchID + "'");
            SqlConnection con;
            SqlCommand command;
            con = new SqlConnection(connectionString);
            con.Open();
            command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        internal static void AddMatchRecord(MatchObject match, decimal vigHome, decimal vigDraw, decimal vigAway, decimal betfairHome, decimal betfairDraw, decimal betfairAway, decimal valueHome, decimal valueDraw, decimal valueAway, DateTime pinnacleLastUpdated, DateTime commenceTime)
        {
            var sql = ("INSERT INTO Match (League, HomeTeam, AwayTeam, CommenceTime, LastUpdated, PinnacleHome, PinnacleDraw, PinnacleAway, " +
                "VigFreeHome, VigFreeDraw, VigFreeAway, BetfairHome, BetfairDraw, BetfairAway, ValueHome, ValueDraw, ValueAway, Completed) VALUES(" +
                "'" + match.League + "', " +
                "'" + match.HomeTeam + "', " +
                "'" + match.AwayTeam + "', " +
                "CONVERT(DATETIME2, '" + commenceTime + "', 105), " +
                "CONVERT(DATETIME2, '" + pinnacleLastUpdated + "', 105), " +
                "'" + match.PinnacleHomeOdds + "', " +
                "'" + match.PinnacleDrawOdds + "', " +
                "'" + match.PinnacleAwayOdds + "', " +
                "'" + vigHome + "', " +
                "'" + vigDraw + "', " +
                "'" + vigAway + "', " +
                "'" + betfairHome + "', " +
                "'" + betfairDraw + "', " +
                "'" + betfairAway + "', " +
                "'" + valueHome + "', " +
                "'" + valueDraw + "', " +
                "'" + valueAway + "', " +
                "0)");
            SqlConnection con;
            SqlCommand command;
            con = new SqlConnection(connectionString);
            con.Open();
            command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        internal static void AddMatchRecordHistoric(MatchObject match, decimal vigHome, decimal vigDraw, decimal vigAway, decimal betfairHome, decimal betfairDraw, decimal betfairAway, decimal valueHome, decimal valueDraw, decimal valueAway, DateTime pinnacleLastUpdated, DateTime commenceTime)
        {
            var sql = ("INSERT INTO Match2 (League, HomeTeam, AwayTeam, CommenceTime, LastUpdated, PinnacleHome, PinnacleDraw, PinnacleAway, " +
                "VigFreeHome, VigFreeDraw, VigFreeAway, BetfairHome, BetfairDraw, BetfairAway, ValueHome, ValueDraw, ValueAway, Completed) VALUES(" +
                "'" + match.League + "', " +
                "'" + match.HomeTeam + "', " +
                "'" + match.AwayTeam + "', " +
                "CONVERT(DATETIME2, '" + commenceTime + "', 105), " +
                "CONVERT(DATETIME2, '" + pinnacleLastUpdated + "', 105), " +
                "'" + match.PinnacleHomeOdds + "', " +
                "'" + match.PinnacleDrawOdds + "', " +
                "'" + match.PinnacleAwayOdds + "', " +
                "'" + vigHome + "', " +
                "'" + vigDraw + "', " +
                "'" + vigAway + "', " +
                "'" + betfairHome + "', " +
                "'" + betfairDraw + "', " +
                "'" + betfairAway + "', " +
                "'" + valueHome + "', " +
                "'" + valueDraw + "', " +
                "'" + valueAway + "', " +
                "0)");
            SqlConnection con;
            SqlCommand command;
            con = new SqlConnection(connectionString);
            con.Open();
            command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();
        }
    }
}
