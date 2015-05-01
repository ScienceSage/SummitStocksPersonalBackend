using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.Clustering
{
    class WeekSorter
    {
        public int[] BuildBestWeeksToSell (int[] highsIn)
        {
            int[] highs = highsIn.Clone() as int[];

            int[] bestWeeks = new int[highs.Length];

            for (int j = 0; j < highs.Length; j++)
            {
                int topValue = 0;
                int topIndex = 0;
                for (int i = 0; i < highs.Length; i++)
                {
                    if (topValue < highs[i])
                    {
                        topValue = highs[i];
                        topIndex = i;
                    }
                }
                bestWeeks[j] = topIndex + 1;
                highs[topIndex] = -100;
            }

            return bestWeeks;
        }

        public int[] BuildBestWeeksToBuy(int[] lowsIn)
        {
            int[] lows = lowsIn.Clone() as int[];

            int[] bestWeeks = new int[lows.Length];

            for (int j = 0; j < lows.Length; j++)
            {
                int topValue = 0;
                int topIndex = 0;
                for (int i = 0; i < lows.Length; i++)
                {
                    if (topValue < lows[i])
                    {
                        topValue = lows[i];
                        topIndex = i;
                    }
                }
                bestWeeks[j] = topIndex + 1;
                lows[topIndex] = -100;
            }

            return bestWeeks;
        }
    }
}
