using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Bundles
{
    class Stock
    {
        public string ticker { get; set; }

        public Stock (string ticker)
        {
            this.ticker = ticker;
        }
    }
}
