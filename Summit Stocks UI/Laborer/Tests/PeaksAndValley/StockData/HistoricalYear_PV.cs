using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData
{
    class HistoricalYear_PV
    {
        public DateTime Date { get; set; }
        public double Average { get; set; }
        private List<HistoricalWeek_PV> weeks = new List<HistoricalWeek_PV>();
        public List<HistoricalWeek_PV> MaxWeeks { get; set; }
        public List<HistoricalWeek_PV> MinWeeks { get; set; }

        public List<HistoricalWeek_PV> Weeks() { return weeks; }

        public void AddWeek(HistoricalWeek_PV week) { weeks.Add(week); }
    }
}
