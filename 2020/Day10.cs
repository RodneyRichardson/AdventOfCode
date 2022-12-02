using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day10
    {
        const long MAX_JOLTAGE_DIFF = 3;

        public Int64 Process(String[] input)
        {
            Int64 result = -1;

            //input = new String[] {
            //    "16",
            //    "10",
            //    "15",
            //    "5",
            //    "1",
            //    "11",
            //    "7",
            //    "19",
            //    "6",
            //    "12",
            //    "4",
            //};

            //input = new String[] {
            //    "28",
            //    "33",
            //    "18",
            //    "42",
            //    "31",
            //    "14",
            //    "46",
            //    "20",
            //    "48",
            //    "47",
            //    "24",
            //    "23",
            //    "49",
            //    "45",
            //    "19",
            //    "38",
            //    "39",
            //    "11",
            //    "1",
            //    "32",
            //    "25",
            //    "35",
            //    "8",
            //    "17",
            //    "7",
            //    "9",
            //    "4",
            //    "2",
            //    "34",
            //    "10",
            //    "3",
            //};

            List<Int64> values = input.Select(line => Int64.Parse(line)).OrderBy(v => v).ToList();
            // Add zero starting point.
            values.Insert(0, 0);
            // Add final adapter
            values.Add(values.Max() + 3);

            //result = CalculatePart1(values.ToArray());

            result = CalculatePart2(values.ToArray());

            return result;
        }

        private static long CalculatePart1(long[] values)
        {
            long result;
            Dictionary<Int64, int> gapCount = new Dictionary<long, int>
            {
                { 1, 0 },
                { 2, 0 },
                { 3, 0 }
            };

            for (int i = 0; i < values.Length - 1; i++)
            {
                Int64 gap = values[i + 1] - values[i];
                Debug.Assert(gap >= 1);
                Debug.Assert(gap <= 3);
                gapCount[gap]++;
            }

            result = gapCount[1] * gapCount[3];
            return result;
        }


        private static long CalculatePart2(long[] values)
        {
            IEnumerable<long> jolts = values.Reverse();
            var subtreeCounts = new Dictionary<long, long>();
            foreach (long jolt in jolts)
            {
                var possibleNext = jolts.Where(j => j > jolt && j <= jolt + 3);
                subtreeCounts[jolt] = possibleNext.Select(n => subtreeCounts[n]).Sum();
                if (subtreeCounts[jolt] == 0)
                {
                    subtreeCounts[jolt] = 1;
                }
            }
            return subtreeCounts[0];
        }
    }
}
