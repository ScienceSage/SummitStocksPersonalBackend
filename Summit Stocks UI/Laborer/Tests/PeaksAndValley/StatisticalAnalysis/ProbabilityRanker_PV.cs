using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summit_Stocks_UI.Laborer.Tests.PeaksAndValley.StatisticalAnalysis
{
    class ProbabilityRanker_PV
    {
        // will return an array that pairs with bestWeeks giving the standard deviation difference 
        // (larger is more statistically relevent)
        public double[] Rank(int[] bestWeeks, int[] weeks)
        {
            // find average value 
            double total = 0;
            foreach (int week in weeks)
                total += week;
            double average = total / weeks.Length;
            // find the standard deviation
            total = 0;
            foreach (int week in weeks)
                total += (average - week) * (average - week);
            double sd = Math.Sqrt(total / weeks.Length);
            // find the sd difference ranking for each week
            double[] rankings = new double[weeks.Length];
            for (int i = 0; i < weeks.Length; i++ )
            {
                int week = bestWeeks[i] - 1;
                double value = weeks[week];
                rankings[i] = Math.Round(Math.Abs((average - value) / sd) * 100) / 100;
            }

            return rankings;
        }
    }
}
