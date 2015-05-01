using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.Clustering
{
    class YearsToDataArray_PV
    {
        public int[] Highs(List<HistoricalYear_PV> years)
        {
            int maxYearLength = 0;

            foreach (HistoricalYear_PV year in years)
            {
                if (year.Weeks().Count > maxYearLength) maxYearLength = year.Weeks().Count;
            }

            int[] highs = new int[maxYearLength];

            foreach (HistoricalYear_PV year in years)
            {
                foreach (HistoricalWeek_PV week in year.MaxWeeks)
                {
                    int i = year.Weeks().IndexOf(week);
                    highs[i]++;
                }
            }

            return highs;
        }

        public int[] Lows(List<HistoricalYear_PV> years)
        {
            int maxYearLength = 0;

            foreach (HistoricalYear_PV year in years)
            {
                if (year.Weeks().Count > maxYearLength) maxYearLength = year.Weeks().Count;
            }

            int[] lows = new int[maxYearLength];

            foreach (HistoricalYear_PV year in years)
            {
                foreach (HistoricalWeek_PV week in year.MinWeeks)
                {
                    int i = year.Weeks().IndexOf(week);
                    lows[i]++;
                }
            }

            return lows;
        }
    }
}
