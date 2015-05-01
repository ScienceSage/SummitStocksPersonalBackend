using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley
{
    class PeaksAndValleyBundle
    {
        public int[] Highs { get; set; }
        public int[] Lows { get; set; }
        public int[] Overlay { get; set; }
        public int[] BestWeeksToSell { get; set; }
        public int[] BestWeeksToBuy { get; set; }
        public double[] BestToSellRankings { get; set; }
        public double[] BestToBuyRankings { get; set; }

        public int AverageHigh { get; set; }
        public int AverageLow { get; set; }

        public List<HistoricalStock_PV> Days { get; set; }
        public List<HistoricalWeek_PV> Weeks { get; set; }
        public List<HistoricalYear_PV> Years { get; set; }
    }
}
