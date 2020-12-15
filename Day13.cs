using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day13
    {

        public UInt64 Process(String[] input)
        {
            UInt64 result = 0;

            //input = new String[] {
            //    "939",
            //    "7,13,x,x,59,x,31,19",
            //};

            //            result = DoPart1(input);
            result = DoPart2(input[1], 100000000000000);

            return result;
        }

        public class Pair
        {
            public int Index;
            public ulong BusID;

            public override string ToString()
            {
                return $"{Index} (mod {BusID})";
            }
        }

        public static class ChineseRemainderTheorem
        {
            public static ulong Solve(List<Pair> pairs)
            {
                ulong[] n = pairs.Select(p => p.BusID).ToArray();
                ulong[] a = pairs.Select(p => p.BusID - (ulong)p.Index).ToArray();
                ulong result =  Solve(n, a);

                int counter = 0;
                int maxCount = n.Length - 1;
                while (counter <= maxCount)
                {
                    Console.WriteLine($"{result} ≡ {a[counter]} (mod {n[counter]}): {result % n[counter]}");
                    counter++;
                }

                Console.WriteLine($"---------");

                result = 1068781;
                counter = 0;
                while (counter <= maxCount)
                {
                    Console.WriteLine($"{result} ≡ {a[counter]} (mod {n[counter]}): {result % n[counter]}");
                    counter++;
                }

                return result;
            }

            public static ulong Solve(ulong[] n, ulong[] a)
            {
                ulong prod = n.Aggregate(1uL, (i, j) => i * j);
                ulong p;
                ulong sm = 0;
                for (int i = 0; i < n.Length; i++)
                {
                    p = prod / n[i];
                    sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
                }
                return sm % prod;
            }

            private static ulong ModularMultiplicativeInverse(ulong a, ulong mod)
            {
                ulong b = a % mod;
                for (ulong x = 1; x < mod; x++)
                {
                    if ((b * x) % mod == 1)
                    {
                        return x;
                    }
                }
                return 1;
            }
        }

        private static ulong DoPart2(String input, ulong initial)
        {
            List<ulong?> buses = input.Split(',')
                .Select(s => ulong.TryParse(s, out ulong bus)? (ulong?)bus : null)
                .ToList();

            List<Pair> pairs = new List<Pair>();
            for (int i = 0; i < buses.Count; i++)
            {
                if (buses[i].HasValue)
                {
                    pairs.Add(new Pair { Index = i, BusID = buses[i].Value });
                }
            }
            pairs = pairs.OrderBy(p => p.BusID).ToList();

            ulong step = ChineseRemainderTheorem.Solve(pairs);
            return step;

        }

        private static long DoPart1(String[] input)
        {
            long earliest = long.Parse(input[0]);
            IEnumerable<int> buses = input[1].Split(',')
                .Where(s => int.TryParse(s, out _))
                .Select(s => int.Parse(s))
                .OrderBy(i => i);


            long result;
            Dictionary<long, int> timestamps = new Dictionary<long, int>();
            foreach (int busID in buses)
            {
                long t = 0;
                while (t < earliest)
                {
                    t += busID;
                }
                timestamps[t] = busID;
            }

            long earliestBusTimestamp = timestamps.Keys.Min();
            result = (earliestBusTimestamp - earliest) * timestamps[earliestBusTimestamp];
            return result;
        }
    }
}
