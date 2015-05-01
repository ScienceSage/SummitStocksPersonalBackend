using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.MathStuff
{
    class YearAverager_PV
    {
        public double Average(HistoricalYear_PV year)
        {
            double total = 0;

            foreach (HistoricalWeek_PV week in year.Weeks()) { total += week.Average; }

            return total / year.Weeks().Count();
        }
    }
}
