using Summit_Stocks_UI.Laborer.Bundles;
using Summit_Stocks_UI.Laborer.Tests;
using Summit_Stocks_UI.Laborer.Tests.PeaksAndValley;
using Summit_Stocks_UI.User.User_Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Commands
{
    public static class CommandCenter
    {
        public enum Commands { UpdateAll }

        public static void SendCommand(string ticker, Commands command)
        {
            switch (command)
            {
                case Commands.UpdateAll:
                    Updater.UpdateCurrentTicker(ticker);
                    PeaksAndValleyTest pVTest = new PeaksAndValleyTest();
                    PeaksAndValleyBundle pVBundle = pVTest.Query(ticker);
                    Updater.UpdatePV(pVBundle);
                    Updater.UpdatePersonalData();
                    Updater.UpdatePersonalExchange(ticker);
                    Updater.UpdatePortfolioBalances();
                    break;
                default:
                    return;
            }
        }
    }
}
