using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day9
    {

        public Int64 Process(String[] input)
        {
            Int64 result = -1;

            //input = new String[] {
            //    "35",
            //    "20",
            //    "15",
            //    "25",
            //    "47",
            //    "40",
            //    "62",
            //    "55",
            //    "65",
            //    "95",
            //    "102",
            //    "117",
            //    "150",
            //    "182",
            //    "127",
            //    "219",
            //    "299",
            //    "277",
            //    "309",
            //    "576",
            //};

            Int64[] values = input.Select(line => Int64.Parse(line)).ToArray();

            // Test
            //(int index, Int64 value) = FindFirstMissingSum(values, 5);
            //result = FindEncryptionWeakness(values, value, index);


            // Actual data
            (int index, Int64 value) = FindFirstMissingSum(values, 25);
            result = FindEncryptionWeakness(values, value, index);

            return result;
        }

        private long FindEncryptionWeakness(long[] values, long targetValue, int maxIndex)
        {
            for (int i = 0; i < maxIndex; i++)
            {
                // Assume must sum at least 2 values.
                long sum = values[i];
                for (int j = i+1; j < maxIndex; j++)
                {
                    sum += values[j];
                    if (sum == targetValue)
                    {
                        long[] temp = values[i..(j + 1)];
                        long min = temp.Min();
                        long max = temp.Max();
                        return min + max;
                    }
                    // Assume all positive integers
                    if (sum > targetValue)
                    {
                        break;
                    }
                }
            }
            return -1;
        }

        private static (int, long) FindFirstMissingSum(long[] values, int size)
        {
            for (int i = size; i < values.Length; i++)
            {
                bool found = false;

                for (int j = i - size; j < i - 1; j++)
                {
                    for (int k = j + 1; k < i; k++)
                    {
                        if (values[j] + values[k] == values[i])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }

                if (!found)
                {
                    return (i, values[i]);
                }
            }

            return (-1, -1);
        }
    }
}
