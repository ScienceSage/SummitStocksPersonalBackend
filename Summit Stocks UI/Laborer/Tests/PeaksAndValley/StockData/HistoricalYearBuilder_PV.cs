using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.MathStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData
{
    class HistoricalYearBuilder_PV
    {
        public List<HistoricalYear_PV> BuildYears(List<HistoricalWeek_PV> weeks, bool cleanse)
        {
            List<HistoricalYear_PV> years = new List<HistoricalYear_PV>();

            years.Add(new HistoricalYear_PV());
            years.Last().Date = weeks.ElementAt(0).Date;
            years.Last().AddWeek(weeks.ElementAt(0));

            for (int i = 1; i < weeks.Count; i++)
            {
                HistoricalWeek_PV week = weeks.ElementAt(i);

                // if the week is not part of the current year
                if (week.Date.Year != weeks.ElementAt(i - 1).Date.Year)
                {
                    years.Add(new HistoricalYear_PV());
                    years.Last().Date = week.Date;
                }
                years.Last().AddWeek(weeks.ElementAt(i));
            }

            if (cleanse) years = Cleanse(years);

            YearAverager_PV averager = new YearAverager_PV();

            foreach (HistoricalYear_PV year in years)
            {
                year.Average = averager.Average(year);
            }

            return years;
        }

        private List<HistoricalYear_PV> Cleanse(List<HistoricalYear_PV> years)
        {
            // if there was a major price change, discount the year as a fluke
            // if there are not enough week, discount it as an incomplete year
            for (int i = 0; i < years.Count; i++)
            {
                double min = int.MaxValue;
                double max = 0;
                HistoricalYear_PV year = years.ElementAt(i);

                foreach (HistoricalWeek_PV week in year.Weeks())
                {
                    if (week.Average < min) min = week.Average;
                    if (week.Average > max) max = week.Average;
                }

                double range = max - min;

                if (year.Weeks().Count < 45 || range > 300)
                {
                    years.Remove(year);
                    i--;
                }
            }

            return years;
        }
    }
}
