using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class CustomsDeclaration
    {
        public HashSet<char> AnyYes { get; }
        public HashSet<char> AllYes { get; }

        public CustomsDeclaration(String answers)
        {
            AnyYes = new HashSet<char>(answers.Where(c => Char.IsLetter(c)));
            String[] eachAnswer = answers.Split();
            AllYes = new HashSet<char>(eachAnswer[0]);
            for (int i = 1; i < eachAnswer.Length; i++)
            {
                AllYes.IntersectWith(eachAnswer[i]);
            }
        }

    }

    public class Day6
    {
        public UInt64 Process(String[] input)
        {
            UInt64 result = 0;

            List<CustomsDeclaration> declarations = new List<CustomsDeclaration>();
            StringBuilder sb = new StringBuilder();
            foreach (String line in input)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    CustomsDeclaration data = new CustomsDeclaration(sb.ToString());
                    declarations.Add(data);
                    sb.Length = 0;
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(line);
                }
            }
            // The file may not end in a blank line
            if (sb.Length > 0)
            {
                CustomsDeclaration data = new CustomsDeclaration(sb.ToString());
                declarations.Add(data);
                sb.Length = 0;
            }

            //result = (UInt64)declarations.Select(d => d.AnyYes.Count).Sum();
            result = (UInt64)declarations.Select(d => d.AllYes.Count).Sum();

            return result;
        }
    }
}
