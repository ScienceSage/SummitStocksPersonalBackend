using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StatisticalAnalysis
{
    class MaxAndMinFinder_PV
    {
        public List<HistoricalYear_PV> FindMaxesAndMins(List<HistoricalYear_PV> years)
        {

            foreach (HistoricalYear_PV year in years)
            {
                List<HistoricalWeek_PV> minWeeks = new List<HistoricalWeek_PV>();
                List<HistoricalWeek_PV> maxWeeks = new List<HistoricalWeek_PV>();

                // give a bufferzone to test against but not to test
                // test for peaks and valleys
                for (int i = 1; i < year.Weeks().Count - 1; i++)
                {
                    if (year.Weeks().ElementAt(i).Average > year.Weeks().ElementAt(i - 1).Average
                        && year.Weeks().ElementAt(i).Average > year.Weeks().ElementAt(i + 1).Average)
                        maxWeeks.Add(year.Weeks().ElementAt(i));

                    if (year.Weeks().ElementAt(i).Average < year.Weeks().ElementAt(i - 1).Average
                        && year.Weeks().ElementAt(i).Average < year.Weeks().ElementAt(i + 1).Average)
                        minWeeks.Add(year.Weeks().ElementAt(i));
                }

                year.MaxWeeks = maxWeeks.OrderBy(o => o.Average).ToList();
                year.MaxWeeks.Reverse();
                year.MinWeeks = minWeeks.OrderBy(o => o.Average).ToList();

                // only count maxes / mins that are respective to the yearly average
                for (int i = 0; i < year.MaxWeeks.Count; i++)
                {
                    if (year.MaxWeeks.ElementAt(i).Average < year.Average)
                    {
                        year.MaxWeeks.Remove(year.MaxWeeks.ElementAt(i));
                        i--;
                    }
                }

                for (int i = 0; i < year.MinWeeks.Count; i++)
                {
                    if (year.MinWeeks.ElementAt(i).Average > year.Average)
                    {
                        year.MinWeeks.Remove(year.MinWeeks.ElementAt(i));
                        i--;
                    }
                }
            }

            return years;
        }
    }
}
