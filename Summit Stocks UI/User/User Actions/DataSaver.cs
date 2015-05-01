using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Summit_Stocks_UI.User.User_Actions
{
    class DataSaver
    {
        public void AddTicker(string ticker, ComboBox comboBox)
        {
            ticker = ticker.ToUpper();
            comboBox.Items.Add("" + ticker);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\SavedTickers.txt", true))
            {
                file.WriteLine();
                file.Write(ticker);
            }
        }

        public void AddOwnedStock(string ticker)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\OwnedStocks.txt", true))
            {
                file.WriteLine(ticker + " " + "0");
            }
        }

        public void ChangeOwnedStock(string ticker, int change, bool selling)
        {
            string[] lines = System.IO.File.ReadAllLines
                (@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\OwnedStocks.txt");

            // Find the ticker
            for (int i = 0; i < lines.Length; i++ )
            {
                string line = lines[i];
                string[] parts = line.Split(' ');
                if (parts[0].Equals(ticker))
                {
                    // change value as long as there are 0 or greater stocks left
                    int currentAmount = int.Parse(parts[1]);
                    if ((currentAmount - change >= 0 && selling) || (currentAmount + change >= 0 && !selling))
                    {
                        int finalAmount;
                        if (selling)
                            finalAmount = currentAmount - change;
                        else
                            finalAmount = currentAmount + change;
                        parts[1] = "" + finalAmount;

                        if (selling)
                            DataCenter.numberToSellBox.Text = "";
                        else
                            DataCenter.numberToBuyBox.Text = "";
                    }
                    else
                    {
                        if (selling)
                            DataCenter.numberToSellBox.Text = "error";
                        else
                            DataCenter.numberToBuyBox.Text = "error";
                    }
                }
                
                line = "";
                foreach (string part in parts)
                {
                    line += part + " "; 
                }
                lines[i] = line;
            }
            // Save changed values
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\OwnedStocks.txt", false))
            {
                foreach (string line in lines)
                {
                    if (!line.Trim().Equals(""))
                    {
                        file.Write(line.Trim());
                        file.WriteLine();
                    }
                }
            }
            // Change Portfolio Balance
            double bankBalance = double.Parse(DataCenter.bankBalanceBox.Text);
            double stockBalance = double.Parse(DataCenter.stockBalanceBox.Text);
            double adjustedBankBalance = 0;
            double adjustedStockBalance = 0;
            if (selling)
            {
                adjustedBankBalance = bankBalance + change * double.Parse(DataCenter.tickerBidPrice.Text);
                adjustedStockBalance = stockBalance - change * double.Parse(DataCenter.tickerBidPrice.Text);
            }
            else
            {
                adjustedBankBalance = bankBalance - change * double.Parse(DataCenter.tickerAskPrice.Text);
                if (!DataCenter.tickerBidPrice.Text.Equals("N/A\n"))
                    adjustedStockBalance = stockBalance + change * double.Parse(DataCenter.tickerBidPrice.Text);
                else
                    adjustedStockBalance = stockBalance + change * double.Parse(DataCenter.tickerAskPrice.Text);
            }
            adjustedBankBalance -= double.Parse(DataCenter.exchangeCost.Text);

            // Save changed values
            double adjustedGrossBalance = adjustedStockBalance + adjustedBankBalance;

            adjustedBankBalance = Math.Round(adjustedBankBalance * 100) / 100;
            adjustedStockBalance = Math.Round(adjustedStockBalance * 100) / 100;
            adjustedGrossBalance = Math.Round(adjustedGrossBalance * 100) / 100;

            string[] balances = { "" + adjustedGrossBalance, "" + adjustedBankBalance, "" + adjustedStockBalance };
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
            // Add to transaction history
            string transaction = ticker;
            if (selling)
                transaction += " selling ";
            else
                transaction += " buying ";

            transaction += change + " for ";

            if (selling) // use bid price
            {
                transaction += double.Parse(DataCenter.tickerBidPrice.Text);
            }
            else // use ask price
            {
                transaction += double.Parse(DataCenter.tickerAskPrice.Text);
            }

            transaction += " ea";

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\TransactionHistory.txt", true))
            {
                file.WriteLine(transaction.Trim());
            }

        }
    }
}
