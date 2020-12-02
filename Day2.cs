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
        private static Regex LineRegex = new Regex(@"(\d*)-(\d*) ([a-z]): ([a-z]*)");

        public int PolicyMinCount { get; set; }
        public int PolicyMaxCount { get; set; }
        public char PolicyLetter { get; set; }

        public String Password { get; set; }

        public Day2Data(String line)
        {
            Match match = LineRegex.Match(line);
            PolicyMinCount = int.Parse(match.Groups[1].Value);
            PolicyMaxCount = int.Parse(match.Groups[2].Value);
            PolicyLetter = match.Groups[3].Value[0];
            Password = match.Groups[4].Value;
        }
    }

    public class Day2
    {
        public Int64 Process(IEnumerable<Day2Data> input)
        {
            int result = 0;

            foreach (Day2Data data in input)
            {
                int count = data.Password.Count(c => c == data.PolicyLetter);
                if (count >= data.PolicyMinCount &&
                    count <= data.PolicyMaxCount)
                {
                    result++;
                }
            }

            return result;
        }
    }
}
