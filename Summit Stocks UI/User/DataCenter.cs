using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Summit_Stocks_UI.User
{
    static class DataCenter
    {
        public static SSMain mainUI { get; set; }
        public static TextBox bestWeeksToBuyBox { get; set; }
        public static ComboBox weeksToDisplayCombo_PV { get; set; }
        public static TextBox bestWeeksToSellBox { get; set; }
        public static Button confirmBuyButton { get; set; }
        public static TextBox numberToBuyBox { get; set; }
        public static Button confirmSellButton { get; set; }
        public static TextBox numberToSellBox { get; set; }
        public static TextBox numberOwnedBox { get; set; }
        public static TextBox grossBalanceBox { get; set; }
        public static TextBox stockBalanceBox { get; set; }
        public static TextBox bankBalanceBox { get; set; }
        public static TextBox tickerPercentChangeBox { get; set; }
        public static TextBox tickerChangeBox { get; set; }
        public static TextBox tickerBidPrice { get; set; }
        public static TextBox tickerAskPrice { get; set; }
        public static TextBox exchangeCost { get; set; }
        public static ComboBox ownedStocksCombo { get; set; }
        public static TextBox calculatorSellingNet { get; set; }
        public static ComboBox calculatorOwnedStocksBox { get; set; }
        public static TextBox calculatorStocksToSellBox { get; set; }
        public static TextBox bestWeeksToBuySDBox { get; set; }
        public static TextBox bestWeeksToSellSDBox { get; set; }
        public static ComboBox aiBestSellPVCombo { get; set; }
        public static Button aiFindBestSellPVButton { get; set; }
        public static ComboBox aiBestBuyPVCombo { get; set; }
        public static Button aiFindBestBuyPVButton { get; set; }
        public static ComboBox aiPVMinYearsAcceptedCombo { get; set; }
        public static ComboBox aiPVNumberOfCompaniesCombo { get; set; }
    }
}
