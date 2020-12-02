using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day1
    {
        public async Task<Int64> Process(String[] input)
        {
            int result = 0;

            List<Int64> sorted = new List<Int64>(input.Select(s => Int64.Parse(s)));
            sorted.Sort();

            for (int i = 0; i < sorted.Count - 1; i++)
            {
                for (int j = sorted.Count - 1; j > i; j--)
                {
                    for (int k = j - 1; k > i; k--)
                    {
                        if (sorted[i] + sorted[j] + sorted[k] == 2020)
                        {
                            return sorted[i] * sorted[j] * sorted[k];
                        }
                    }
                }
            }

            return result;
        }
    }
}
