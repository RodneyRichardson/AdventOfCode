using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day2Data
    {
        private static readonly Regex LineRegex = new Regex(@"(\d*)-(\d*) ([a-z]): ([a-z]*)");

        public int PolicyIndex1 { get; set; }
        public int PolicyIndex2 { get; set; }
        public char PolicyLetter { get; set; }

        public String Password { get; set; }

        public Day2Data(String line)
        {
            Match match = LineRegex.Match(line);

            PolicyIndex1 = int.Parse(match.Groups[1].Value) - 1;
            PolicyIndex2 = int.Parse(match.Groups[2].Value) - 1;
            PolicyLetter = match.Groups[3].Value[0];
            Password = match.Groups[4].Value;
        }
    }

    public class Day02
    {
        public Int64 Process(IEnumerable<Day2Data> input)
        {
            int result = 0;

            foreach (Day2Data data in input)
            {
                char char1 = data.Password[data.PolicyIndex1];
                char char2 = data.Password[data.PolicyIndex2];

                if ((char1 == data.PolicyLetter && char2 != data.PolicyLetter) ||
                    (char1 != data.PolicyLetter && char2 == data.PolicyLetter))
                {
                    result++;
                }
            }

            return result;
        }
    }
}
