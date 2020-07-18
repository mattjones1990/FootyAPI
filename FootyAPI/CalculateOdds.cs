using System;
using System.Collections.Generic;
using System.Text;

namespace FootyAPI
{
    class CalculateOdds
    {
        internal static decimal getVigOdds(decimal odds, decimal vig)
        {
            decimal n = 3.0m;
            return (n * odds) / (n - (vig - 1) * odds);
        }

        internal static decimal getVig(MatchObject match)
        {
            return (1 / match.PinnacleHomeOdds) + (1 / match.PinnacleDrawOdds) + (1 / match.PinnacleAwayOdds);
        }

        internal static decimal getBetfairOdds(decimal odds)
        {
            decimal commission = 2.0m;
            return 1+(1-(commission / 100))*(odds - 1);
        }

        internal static decimal getValueOdds(decimal vigHome, decimal betfairHome)
        {
            return ((betfairHome / vigHome) - 1) * 100;
        }
    }
}
