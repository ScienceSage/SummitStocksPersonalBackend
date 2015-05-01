using Summit_Stocks_UI.Laborer.Bundles;
using Summit_Stocks_UI.Laborer.Commands;
using Summit_Stocks_UI.User.Initialization;
using Summit_Stocks_UI.User.User_Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Summit_Stocks_UI
{
    public partial class SSMain : Form
    {
        DataSaver actions = new DataSaver();

        public SSMain()
        {
            InitializeComponent();
            Initializer initializer = new Initializer();
            initializer.InitializeDataCenter(this);
            initializer.InitializeTickerComboBox(tickerComboBox);
            Updater.UpdatePersonalData();
        }

        private void tickerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommandCenter.SendCommand(tickerComboBox.Text.ToUpper(), CommandCenter.Commands.UpdateAll);
        }

        private void addTickerButton_Click(object sender, EventArgs e)
        {
            actions.AddTicker(newTickerTextBox.Text, tickerComboBox);
            newTickerTextBox.Text = "";
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            CommandCenter.SendCommand(tickerComboBox.Text, CommandCenter.Commands.UpdateAll);
        }

        private void comfirmSellButton_Click(object sender, EventArgs e)
        {
            DataSaver saver = new DataSaver();
            saver.ChangeOwnedStock(this.tickerComboBox.Text, int.Parse(numberToSellBox.Text), true);
            CommandCenter.SendCommand(this.tickerComboBox.Text, CommandCenter.Commands.UpdateAll);
        }

        private void confirmBuyButton_Click(object sender, EventArgs e)
        {
            DataSaver saver = new DataSaver();
            saver.ChangeOwnedStock(this.tickerComboBox.Text, int.Parse(numberToBuyBox.Text), false);
            CommandCenter.SendCommand(this.tickerComboBox.Text, CommandCenter.Commands.UpdateAll);
        }

        private void ownedStocksCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tickerComboBox.Text = ownedStocksCombo.Text;
        }

        private void calculatorOwnedStocksBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Updater.UpdatePorfolioSellingCalculator();
        }

        private void aiFindBestPVButton_Click(object sender, EventArgs e)
        {
            Updater.UpdateAIBestPV();
        }

    }
}
