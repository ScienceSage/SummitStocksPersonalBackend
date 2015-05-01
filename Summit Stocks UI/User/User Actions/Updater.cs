using Summit_Stocks_UI.Laborer.AI;
using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.User.User_Actions
{
    static class Updater
    {
        public static void UpdateAIBestPV()
        {
            // Use AI to search all known tickers for the best standard deviation weeks
            // Only include companies with a history of larger than (10) years
            int minYears = 10; // change to int.Parse(DataCenter.aiPVMinYearsCombo.Text);
            int numberOfYearsToReturn = 20; // change to int.Parse(DataCenter.aiPVNumberOfCompaniesCombo);

            //foreach (string ticker in new Sorter().SortBestPV(numberOfYearsToReturn, minYears))
            string[,] bestPV = new Sorter().SortBestPV(numberOfYearsToReturn, minYears);
            for (int i = 0; i < bestPV.Length; i++)
                DataCenter.aiBestSellPVCombo.Items.Add(bestPV[i, 0] + " " + bestPV[i, 1]);
        }

        public static void UpdatePorfolioSellingCalculator()
        {
            string ticker = DataCenter.calculatorOwnedStocksBox.Text;
            double stockPrice;

            using (WebClient web = new WebClient())
            {
                string data = web.DownloadString(string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}&f=b", ticker));
                stockPrice = double.Parse(data);
            }
            double net = 0;

            // average the worth of TICKER stocks that you bought
            string[] lines = System.IO.File.ReadAllLines
                (@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\TransactionHistory.txt");
            // find number of each kind of transaction
            int buyingTransactionCount = 0;
            int sellingTransactionCount = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');

                if (parts[0].Equals(ticker))
                {
                    if (parts[1].Equals("buying"))
                        buyingTransactionCount++;
                    else
                        sellingTransactionCount++;
                }
             }
            // get all transaction data
            double[,] buyingTranactions = new double[buyingTransactionCount, 2];
            double[,] sellingTranactions = new double[sellingTransactionCount, 2];

            int buyingIndex = 0;
            int sellingIndex = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');

                if (parts[0].Equals(ticker))
                {
                    if (parts[1].Equals("buying"))
                    {
                        buyingTranactions[buyingIndex, 0] = double.Parse(parts[2]);
                        buyingTranactions[buyingIndex, 1] = double.Parse(parts[4]);
                        buyingIndex++;
                    }
                    else
                    {
                        sellingTranactions[sellingIndex, 0] = double.Parse(parts[2]);
                        sellingTranactions[sellingIndex, 1] = double.Parse(parts[4]);
                        sellingIndex++;
                    }
                }
            }
            // find the total selling transactions and subtract that from buying transactions chronologically
            int countSold = 0;
            for (int i = 0; i < sellingTransactionCount; i++ )
                countSold += (int)sellingTranactions[i, 0];

            double approxBoughtPrice = 0;
            for (int i = 0; i < buyingTransactionCount; i++)
            {
                if (countSold > 0)
                {
                    if (countSold - buyingTranactions[i, 0] >= 0)
                        countSold -= (int)buyingTranactions[i, 0];
                    else
                        approxBoughtPrice = buyingTranactions[i, 1];
                }
                else
                    approxBoughtPrice = buyingTranactions[i, 1];
            }

            net = stockPrice * double.Parse(DataCenter.calculatorStocksToSellBox.Text) - approxBoughtPrice * double.Parse(DataCenter.calculatorStocksToSellBox.Text);

            net = Math.Round(net * 100) / 100;

            net -= double.Parse(DataCenter.exchangeCost.Text);

            DataCenter.calculatorSellingNet.Text = "" + net;
        }

        public static void UpdatePortfolioBalances()
        {
            string[] lines = System.IO.File.ReadAllLines
                (@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\OwnedStocks.txt");
            
            double stockBalance = 0;

            using (WebClient web = new WebClient())
            {
                foreach (string line in lines)
                {
                    string[] split = line.Split(' ');
                    
                    string ticker = split[0];
                    string stringCount = split[1];

                    string data = web.DownloadString(string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}&f=b", ticker));

                    double bidPrice = double.Parse(data);
                    double count = double.Parse(stringCount);

                    stockBalance += count * bidPrice;
                }
            }

            double adjustedGrossBalance = stockBalance + double.Parse(DataCenter.bankBalanceBox.Text);
            // save owned stock value and adjust gross
            string[] balances = { "" + adjustedGrossBalance, "" + DataCenter.bankBalanceBox.Text, "" + stockBalance };
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\PersonalAccountData.txt", false))
            {
                foreach (string line in balances)
                {
                    if (!line.Trim().Equals(""))
                    {
                        file.Write(line.Trim());
                        file.WriteLine();
                    }
                }
            }

            DataCenter.grossBalanceBox.Text = "" + adjustedGrossBalance;
            DataCenter.stockBalanceBox.Text = "" + stockBalance;
        }

        public static void UpdatePersonalExchange(string ticker)
        {
            string[] lines = System.IO.File.ReadAllLines
                (@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\OwnedStocks.txt");
            
            // Find the ticker name
            bool isData = false;
            foreach (string line in lines)
            {
                string[] split = line.Split(' ');
                if (split[0].Equals(ticker))
                {
                    isData = true;
                    DataCenter.numberOwnedBox.Text = split[1];
                }
            }
            if (!isData)
            {
                new DataSaver().AddOwnedStock(ticker);
                DataCenter.numberOwnedBox.Text = "0";
            }

        }

        public static void UpdateCurrentTicker(string ticker)
        {
            using (WebClient web = new WebClient())
            {
                string data = web.DownloadString(string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}&f=a", ticker));

                DataCenter.tickerAskPrice.Text = data;

                data = web.DownloadString(string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}&f=b", ticker));

                DataCenter.tickerBidPrice.Text = data; 
                
                data = web.DownloadString(string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}&f=c1", ticker));

                DataCenter.tickerChangeBox.Text = data;

                data = web.DownloadString(string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}&f=p2", ticker));

                DataCenter.tickerPercentChangeBox.Text = data;
            }
        }

        public static void UpdatePersonalData()
        {
            string[] lines = System.IO.File.ReadAllLines
                (@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\PersonalAccountData.txt");
            int i = 0;
            // line 1 is Gross Balance
            DataCenter.grossBalanceBox.Text = lines[i++];
            // line 2 is Bank Balance
            DataCenter.bankBalanceBox.Text = lines[i++];
            // line 3 is Stock Balance
            DataCenter.stockBalanceBox.Text = lines[i++];

            // update owned stocks combo
            lines = System.IO.File.ReadAllLines
                (@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\OwnedStocks.txt");
            foreach (String line in lines)
            {
                string[] split = line.Split(' ');
                if (int.Parse(split[1]) > 0)
                {
                    DataCenter.ownedStocksCombo.Items.Add(split[0]);
                    DataCenter.calculatorOwnedStocksBox.Items.Add(split[0]);
                }
            }

        }

        public static void UpdatePV (PeaksAndValleyBundle bundle)
        {
            int amountToDisplay;

            if (!DataCenter.weeksToDisplayCombo_PV.Text.Equals(""))
                amountToDisplay = int.Parse(DataCenter.weeksToDisplayCombo_PV.Text);
            else
                amountToDisplay = 1;

            // Best weeks to sell
            string toDisplay = "";
            for (int i = 0; i < amountToDisplay; i ++)
            {
                toDisplay += "[" + bundle.BestWeeksToSell[i] + "]";
            }
            DataCenter.bestWeeksToSellBox.Text = toDisplay;

            // Best weeks to buy
            toDisplay = "";
            for (int i = 0; i < amountToDisplay; i++)
            {
                toDisplay += "[" + bundle.BestWeeksToBuy[i] + "]";
            }
            DataCenter.bestWeeksToBuyBox.Text = toDisplay;

            // Best weeks to sell SD
            toDisplay = "";
            for (int i = 0; i < amountToDisplay; i++)
            {
                toDisplay += "[" + bundle.BestToSellRankings[i] + "]";
            }
            DataCenter.bestWeeksToSellSDBox.Text = toDisplay;

            // Best weeks to buy SD
            toDisplay = "";
            for (int i = 0; i < amountToDisplay; i++)
            {
                toDisplay += "[" + bundle.BestToBuyRankings[i] + "]";
            }
            DataCenter.bestWeeksToBuySDBox.Text = toDisplay;
        }

    }
}
