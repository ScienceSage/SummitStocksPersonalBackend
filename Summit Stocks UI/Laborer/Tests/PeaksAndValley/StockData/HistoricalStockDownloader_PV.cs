using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StockData
{
    class HistoricalStockDownloader_PV
    {
        public static List<HistoricalStock_PV> DownloadData(string ticker, int yearToStartFrom)
        {
            List<HistoricalStock_PV> retval = new List<HistoricalStock_PV>();

            using (WebClient web = new WebClient())
            {
                string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&c={1}", ticker, yearToStartFrom));

                string[] rows = data.Split('\n');

                //First row is headers so Ignore it
                for (int i = 1; i < rows.Length; i++)
                {
                    if (rows[i].Replace("n", "").Trim() == "") continue;

                    string[] cols = rows[i].Split(',');

                    HistoricalStock_PV hs = new HistoricalStock_PV();

                    hs.Date = Convert.ToDateTime(cols[0]);
                    hs.Open = Convert.ToDouble(cols[1]);
                    hs.High = Convert.ToDouble(cols[2]);
                    hs.Low = Convert.ToDouble(cols[3]);
                    hs.Close = Convert.ToDouble(cols[4]);
                    hs.Volume = Convert.ToDouble(cols[5]);
                    hs.AdjClose = Convert.ToDouble(cols[6]);

                    retval.Insert(0, hs); // sorts the numbers in chromatic fashion by date
                }

                return retval;
            }
        }
    }
}
