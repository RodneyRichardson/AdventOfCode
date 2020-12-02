using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day3Data
    {
        private static readonly Regex LineRegex = new Regex(@"(\d*)-(\d*) ([a-z]): ([a-z]*)");

        //public int PolicyIndex1 { get; set; }
        //public int PolicyIndex2 { get; set; }
        //public char PolicyLetter { get; set; }
        //public String Password { get; set; }

        public Day3Data(String line)
        {
            Match match = LineRegex.Match(line);

            //PolicyIndex1 = int.Parse(match.Groups[1].Value) - 1;
            //PolicyIndex2 = int.Parse(match.Groups[2].Value) - 1;
            //PolicyLetter = match.Groups[3].Value[0];
            //Password = match.Groups[4].Value;
        }
    }

    public class Day3
    {
        public Int64 Process(IEnumerable<Day3Data> input)
        {
            int result = 0;

            foreach (Day3Data data in input)
            {
       
            }

            return result;
        }
    }
}
