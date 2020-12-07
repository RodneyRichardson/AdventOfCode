using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day7
    {
        public class BagRuleEngine
        {
            public Dictionary<String, BagRule> Rules { get; } = new Dictionary<string, BagRule>();
            public Dictionary<String, List<String>> ContainedBy { get; } = new Dictionary<string, List<string>>();

            public void AddRule(BagRule rule)
            {
                Rules.Add(rule.BagName, rule);

                foreach (BagWithCount bag in rule.ContainedBags)
                {
                    if (!ContainedBy.ContainsKey(bag.BagName))
                    {
                        ContainedBy[bag.BagName] = new List<string>();
                    }
                    ContainedBy[bag.BagName].Add(rule.BagName);
                }
            }

            public Int64 CountContainedBags(String bag)
            {
                Int64 count = 0;

                BagRule rule = Rules[bag];
                foreach (BagWithCount innerBag in rule.ContainedBags)
                {
                    long thisCount = innerBag.Count;
                    count += thisCount;
                    count += thisCount * CountContainedBags(innerBag.BagName);
                }

                return count;
            }

            public HashSet<String> Part1_FindContainingBags(String bag)
            {
                HashSet<String> containingBags = new HashSet<string>();

                IEnumerable<String> newBags = FindContainingBags(bag, containingBags);
                
                while (newBags.Count() > 0)
                {
                    HashSet<String> allNextBags = new HashSet<string>();
                    foreach(String newBag in newBags)
                    {
                        List<String> nextBags = FindContainingBags(newBag, containingBags);
                        foreach (String nextBag in nextBags)
                        {
                            allNextBags.Add(nextBag);
                        }
                    }
                    newBags = allNextBags;
                } 

                return containingBags;
            }

            public List<string> FindContainingBags(string bag, HashSet<string> existingBags)
            {
                List<string> newBags = new List<string>();

                if (ContainedBy.ContainsKey(bag))
                {
                    foreach (String bagName in ContainedBy[bag])
                    {
                        if (existingBags.Add(bagName))
                        {
                            newBags.Add(bagName);
                        }
                    }
                }
                return newBags;
            }
        }

        public class BagRule
        {
            private static readonly Regex LeafRegex = new Regex(
                @"([a-z ]*) bags contain no other bags\.",
                RegexOptions.Compiled);
            private static readonly Regex LineRegex = new Regex(
                @"([a-z ]*) bags contain (\d*) ([a-z ]*) bags?(.*)\.",
                RegexOptions.Compiled);
            private static readonly Regex ClauseRegex = new Regex(
                @"(\d*) ([a-z ]*) bags?",
                RegexOptions.Compiled);

            public String BagName { get; }
            public List<BagWithCount> ContainedBags { get; } = new List<BagWithCount>();

            public BagRule(String line)
            {
                Match m = LeafRegex.Match(line);
                if (m.Success)
                {
                    BagName = m.Groups[1].Value;
                }
                else
                {
                    m = LineRegex.Match(line);

                    BagName = m.Groups[1].Value;
                    ContainedBags.Add(new BagWithCount(Int32.Parse(m.Groups[2].Value), m.Groups[3].Value));

                    if (m.Groups.Count > 4)
                    {
                        MatchCollection matches = ClauseRegex.Matches(m.Groups[4].Value);
                        foreach (Match match in matches)
                        {
                            ContainedBags.Add(new BagWithCount(Int32.Parse(match.Groups[1].Value), match.Groups[2].Value));
                        }
                    }
                }
            }
        }

        public class BagWithCount
        {
            public int Count { get; }
            public String BagName { get; }

            public BagWithCount(int count, String bagName)
            {
                Count = count;
                BagName = bagName;
            }

            public override string ToString()
            {
                return $"{Count} {BagName}";
            }
        }


        public UInt64 Process(String[] input)
        {
            UInt64 result = 0;

            //            input = new String[] {
            //"light red bags contain 1 bright white bag, 2 muted yellow bags.",
            //"dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
            //"bright white bags contain 1 shiny gold bag.",
            //"muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
            //"shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
            //"dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
            //"vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
            //"faded blue bags contain no other bags.",
            //"dotted black bags contain no other bags.",
            //            };

//            input = new string[] {
//"shiny gold bags contain 2 dark red bags.",
//"dark red bags contain 2 dark orange bags.",
//"dark orange bags contain 2 dark yellow bags.",
//"dark yellow bags contain 2 dark green bags.",
//"dark green bags contain 2 dark blue bags.",
//"dark blue bags contain 2 dark violet bags.",
//"dark violet bags contain no other bags.",
//            };

            BagRuleEngine rules = new BagRuleEngine();
            foreach (String line in input)
            {
                BagRule rule = new BagRule(line);
                rules.AddRule(rule);
            }

            String bag = "shiny gold";

            // Part 1
            //HashSet<String> containingBags = rules.Part1_FindContainingBags(bag);
            //result = (UInt64)containingBags.Count;

            // Part 2
            result = (UInt64)rules.CountContainedBags(bag);


            return result;
        }
    }
}
