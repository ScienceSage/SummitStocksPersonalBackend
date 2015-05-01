using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.AI
{
    class Sorter
    {
        public string[,] SortBestPV(int numberOfYearsToReturn, int minYears)
        {
            string[,] best = new string[numberOfYearsToReturn, 2];

            // get a list of all tickers to test
            string[] lines = System.IO.File.ReadAllLines
                (@"c:\users\sage\documents\visual studio 2013\Projects\Summit Stocks UI\Summit Stocks UI\Laborer\Data\companylist.csv");
            
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

               // string ticker = 

            }


            return best;
        }
    }
}
