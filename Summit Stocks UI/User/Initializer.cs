using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Summit_Stocks_UI.User.Initialization
{
    class Initializer
    {
        public void InitializeDataCenter(SSMain mainUI)
        {
            DataCenter.mainUI = mainUI;
            DataCenter.bestWeeksToBuyBox = mainUI.bestWeeksToBuyBox;
            DataCenter.bestWeeksToSellBox = mainUI.bestWeeksToSellBox;
            DataCenter.weeksToDisplayCombo_PV = mainUI.weeksToDisplayCombo_PV;
            DataCenter.confirmBuyButton = mainUI.confirmBuyButton;
            DataCenter.numberToBuyBox = mainUI.numberToBuyBox;
            DataCenter.confirmSellButton = mainUI.confirmSellButton;
            DataCenter.numberToSellBox = mainUI.numberToSellBox;
            DataCenter.numberOwnedBox = mainUI.numberOwnedBox;
            DataCenter.grossBalanceBox = mainUI.grossBalanceBox;
            DataCenter.stockBalanceBox = mainUI.stockBalanceBox;
            DataCenter.bankBalanceBox = mainUI.bankBalanceBox;
            DataCenter.tickerBidPrice = mainUI.tickerBidPrice;
            DataCenter.tickerAskPrice = mainUI.tickerAskPrice;
            DataCenter.tickerChangeBox = mainUI.tickerChangeBox;
            DataCenter.tickerPercentChangeBox = mainUI.tickerPercentChangeBox;
            DataCenter.exchangeCost = mainUI.exchangeCost;
            DataCenter.ownedStocksCombo = mainUI.ownedStocksCombo;
            DataCenter.calculatorOwnedStocksBox = mainUI.calculatorOwnedStocksBox;
            DataCenter.calculatorSellingNet = mainUI.calculatorSellingNet;
            DataCenter.calculatorStocksToSellBox = mainUI.calculatorStocksToSellBox;
            DataCenter.bestWeeksToBuySDBox = mainUI.bestWeeksToBuySDBox;
            DataCenter.bestWeeksToSellSDBox = mainUI.bestWeeksToSellSDBox;
            DataCenter.aiBestSellPVCombo = mainUI.aiBestSellPVCombo;
            DataCenter.aiFindBestSellPVButton = mainUI.aiFindBestSellPVButton;
            //DataCenter.aiBestBuyPVCombo = mainUI.
        }

        public void InitializeTickerComboBox(ComboBox comboBox)
        {
            string[] lines = System.IO.File.ReadAllLines(@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\User\SavedData\SavedTickers.txt");

            foreach (string line in lines)
            {
                comboBox.Items.Add(line);
            }
        }
    }
}
