using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.MathStuff
{
    class WeekAverager_PV
    {
        public double Average(HistoricalWeek_PV week)
        {
            double total = 0;

            foreach (HistoricalStock_PV stock in week.Stocks())
            {
                double dayAverage = stock.Open + stock.Close;
                dayAverage /= 2;
                total += dayAverage;
            }

            return total / week.Stocks().Count();
        }
    }
}
