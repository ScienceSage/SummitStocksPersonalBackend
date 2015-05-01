using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData
{
    class HistoricalWeek_PV
    {
        public DateTime Date { get; set; }
        public double Average { get; set; }
        private List<HistoricalStock_PV> stocks = new List<HistoricalStock_PV>();

        public List<HistoricalStock_PV> Stocks() { return stocks; }

        public void AddDay(HistoricalStock_PV stock) { stocks.Add(stock); }
    }
}
