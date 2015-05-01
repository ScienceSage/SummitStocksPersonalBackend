using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley;
using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.Clustering;
using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StatisticalAnalysis;
using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests
{
    class PeaksAndValleyTest
    {
        public PeaksAndValleyBundle Query (string ticker)
        {
            List<HistoricalStock_PV> days = HistoricalStockDownloader_PV.DownloadData(ticker, 1962);

            List<HistoricalWeek_PV> weeks = new HistoricalWeekBuilder_PV().BuildWeeks(days, true);

            List<HistoricalYear_PV> years = new HistoricalYearBuilder_PV().BuildYears(weeks, true);

            years = new MaxAndMinFinder_PV().FindMaxesAndMins(years);

            int[] highs = new YearsToDataArray_PV().Highs(years);
            int[] lows = new YearsToDataArray_PV().Lows(years);

            int avgHigh = Average(highs);
            int avgLow = Average(lows);

            int[] overlay = new int[highs.Length];

            for (int i = 0; i < highs.Length; i++)
            {
                overlay[i] = highs[i] - lows[i];
            }

            WeekSorter sorter = new WeekSorter();

            int[] bestWeeksToSell = sorter.BuildBestWeeksToSell(highs);
            int[] bestWeeksToBuy = sorter.BuildBestWeeksToBuy(lows);

            ProbabilityRanker_PV ranker = new ProbabilityRanker_PV();

            double[] bestToSellRankings = ranker.Rank(bestWeeksToSell, highs);
            double[] bestToBuyRankings = ranker.Rank(bestWeeksToBuy, lows);

            /*if (years.Count > 7)
            {
                Console.WriteLine("Best Times to Sell");
                new IntArrayVisualizer().Visualize(highs, years.Count, avgHigh);
                Console.WriteLine("Best Times to Buy");
                new IntArrayVisualizer().Visualize(lows, years.Count, avgLow);
            }
            else
            {
                Console.WriteLine("Not Enough Data (less than 7 years)");
            }*/

            PeaksAndValleyBundle bundle = new PeaksAndValleyBundle();

            bundle.Highs = highs;
            bundle.Lows = lows;
            bundle.AverageHigh = avgHigh;
            bundle.AverageLow = avgLow;
            bundle.Overlay = overlay;

            bundle.Days = days;
            bundle.Years = years;
            bundle.Weeks = weeks;

            bundle.BestWeeksToSell = bestWeeksToSell;
            bundle.BestWeeksToBuy = bestWeeksToBuy;

            bundle.BestToSellRankings = bestToSellRankings;
            bundle.BestToBuyRankings = bestToBuyRankings;

            return bundle;
        }

        private int Average(int[] array)
        {
            double sum = 0;
            foreach (int value in array)
            {
                sum += value;
            }

            return (int)Math.Round(sum / array.Length);
        }
    }
}
