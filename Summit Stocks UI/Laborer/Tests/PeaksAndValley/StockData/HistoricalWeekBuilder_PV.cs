using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.MathStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData
{
    class HistoricalWeekBuilder_PV
    {
        // technically not a week, just every 5 days
        public List<HistoricalWeek_PV> BuildWeeks(List<HistoricalStock_PV> stocks, bool cleanse)
        {
            List<HistoricalWeek_PV> weeks = new List<HistoricalWeek_PV>();

            int counter = 5;

            foreach (HistoricalStock_PV stock in stocks)
            {
                if (counter > 3)
                {
                    if (weeks.Count > 0) weeks.Last().Average = new WeekAverager_PV().Average(weeks.Last());

                    weeks.Add(new HistoricalWeek_PV());
                    weeks.Last().Date = stock.Date;

                    counter = 0;
                }
                else
                {
                    counter++;
                }
                weeks.Last().AddDay(stock);
            }

            if (cleanse) Cleanse(weeks);

            return weeks;
        }

        //public List<HistoricalWeek> BuildWeeks(List<HistoricalStock> stocks, bool cleanse)
        //{
        //    List<HistoricalWeek> weeks = new List<HistoricalWeek>();

        //    weeks.Add(new HistoricalWeek());
        //    weeks.Last().AddDay(stocks.ElementAt(0));
        //    weeks.Last().Date = stocks.ElementAt(0).Date;

        //    for (int i = 1; i < stocks.Count; i++)
        //    {
        //        HistoricalStock stock = stocks.ElementAt(i);

        //        // if the stock is not part of the current week
        //        if (stock.Date.AddDays(-1).Day != stocks.ElementAt(i - 1).Date.Day)
        //        {
        //            weeks.Last().Average = new WeekAverager().Average(weeks.Last());

        //            weeks.Add(new HistoricalWeek());
        //            weeks.Last().Date = stock.Date;
        //        }
        //        weeks.Last().AddDay(stocks.ElementAt(i));
        //    }
        //    weeks = Cleanse(weeks);

        //    return weeks;
        //}

        private List<HistoricalWeek_PV> Cleanse(List<HistoricalWeek_PV> weeks)
        {
            for (int i = 0; i < weeks.Count; i++)
            {
                HistoricalWeek_PV week = weeks.ElementAt(i);

                if (week.Stocks().Count < 4)
                {
                    weeks.Remove(week);
                    i--;
                }
            }
            return weeks;
        }
    }
}
