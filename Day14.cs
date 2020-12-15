using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day14
    {
        private static readonly Regex MaskRegex = new Regex(@"mask = ([01X]*)", RegexOptions.Compiled);
        private static readonly Regex MemRegex = new Regex(@"mem\[(\d*)\] = (\d*)", RegexOptions.Compiled);

        public class Mask_Part1
        {
            private ulong zeroMask;
            private ulong oneMask;
            private string displayValue;

            public void Update(String newMask)
            {
                Debug.Assert(newMask.Length == 36);

                displayValue = newMask;
                zeroMask = 0;
                oneMask = 0;

                for (int i = 0; i < newMask.Length; i++)
                {
                    if (newMask[i] == '0')
                    {
                        zeroMask |= (1uL << (35 - i));
                    }
                    else if (newMask[i] == '1')
                    {
                        oneMask |= (1uL << (35 - i));
                    }
                }

                // Invert zeroMask for logical &
                zeroMask = ~zeroMask;
            }

            public ulong Apply(ulong value)
            { 
                ulong result = (value | oneMask) & zeroMask;
                return result;
            }

            public override string ToString()
            {
                return displayValue;
            }
        }

        public class Mask_Part2
        {
            private static readonly Dictionary<int, List<List<bool>>> Patterns;

            static Mask_Part2()
            {
                Patterns = new Dictionary<int, List<List<bool>>>();
                Patterns[0] = new List<List<bool>>();
                Patterns[1] = new List<List<bool>> { new List<bool> { false }, new List<bool> { true } };
                for (int i = 2; i < 16; i++)
                {
                    List<List<bool>> current = new List<List<bool>>();

                    List<List<bool>> previous = Patterns[i - 1];
                    foreach (bool newValue in new bool[] { false, true })
                    {
                        foreach (List<bool> pattern in previous)
                        {
                            List<bool> newPattern = new List<bool> { newValue };
                            newPattern.AddRange(pattern);
                            current.Add(newPattern);
                        }
                    }
                    Patterns[i] = current;
                }
            }

            private String displayValue;
            private ulong zeroMask;
            private ulong oneMask;
            private List<Mask_Part1> floatingMasks;

            public void Update(String newMask)
            {
                Debug.Assert(newMask.Length == 36);

                displayValue = newMask;

                List<int> floatingBitIndexes = newMask.Select((item, index) => new { Item = item, Index = index })
                                  .Where(o => o.Item == 'X')
                                  .Select(o => o.Index)
                                  .ToList();
                floatingMasks = new List<Mask_Part1>();

                Debug.Assert(floatingBitIndexes.Count < Patterns.Count);
                List<List<bool>> patterns = Patterns[floatingBitIndexes.Count];
                foreach (List<bool> pattern in patterns)
                {
                    Debug.Assert(pattern.Count == floatingBitIndexes.Count);
                    StringBuilder maskValue = new StringBuilder(new string('X', 36));
                    for (int i = 0; i < pattern.Count; i++)
                    {
                        maskValue[floatingBitIndexes[i]] = pattern[i] ? '1' : '0';
                    }

                    Mask_Part1 mask = new Mask_Part1();
                    mask.Update(maskValue.ToString());
                    floatingMasks.Add(mask);
                }

                zeroMask = 0;
                oneMask = 0;

                for (int i = 0; i < newMask.Length; i++)
                {
                    if (newMask[i] == '0')
                    {
                        zeroMask |= (1uL << (35 - i));
                    }
                    else if (newMask[i] == '1')
                    {
                        oneMask |= (1uL << (35 - i));
                    }
                }

                // Invert zeroMask for logical &
                zeroMask = ~zeroMask;
            }

            public IEnumerable<ulong> Apply(ulong value)
            {
                ulong maskedValue = (value | oneMask);

                return floatingMasks.Select(m => m.Apply(maskedValue));
            }

            public override string ToString()
            {
                return displayValue;
            }

        }

        public UInt64 Process(String[] input)
        {
            UInt64 result = 0;

            //input = new string[] {
            //    "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
            //    "mem[8] = 11",
            //    "mem[7] = 101",
            //    "mem[8] = 0",
            //};

            //result = DoPart1(input);

            //input = new string[] {
            //    "mask = 000000000000000000000000000000X1001X",
            //    "mem[42] = 100",
            //    "mask = 00000000000000000000000000000000X0XX",
            //    "mem[26] = 1",
            //};

            result = DoPart2(input);
            return result;
        }

        private static ulong DoPart1(string[] input)
        {
            ulong result = 0;

            Mask_Part1 mask = new Mask_Part1();

            Dictionary<ulong, ulong> values = new Dictionary<ulong, ulong>();
            foreach (String instruction in input)
            {
                Match m = MaskRegex.Match(instruction);
                if (m.Success)
                {
                    String newMask = m.Groups[1].Value;
                    mask.Update(newMask);
                }
                else
                {
                    m = MemRegex.Match(instruction);
                    Debug.Assert(m.Success);
                    ulong address = ulong.Parse(m.Groups[1].Value);
                    ulong rawValue = ulong.Parse(m.Groups[2].Value);
                    ulong actualValue = mask.Apply(rawValue);
                    values[address] = actualValue;
                }

            }

            foreach (KeyValuePair<ulong, ulong> kvp in values)
            {
                result += kvp.Value;
            }

            return result;
        }

        private static ulong DoPart2(string[] input)
        {
            ulong result = 0;

            Mask_Part2 mask = new Mask_Part2();

            Dictionary<ulong, ulong> values = new Dictionary<ulong, ulong>();
            foreach (String instruction in input)
            {
                Match m = MaskRegex.Match(instruction);
                if (m.Success)
                {
                    String newMask = m.Groups[1].Value;
                    mask.Update(newMask);
                }
                else
                {
                    m = MemRegex.Match(instruction);
                    Debug.Assert(m.Success);
                    ulong rawAddress = ulong.Parse(m.Groups[1].Value);
                    ulong value = ulong.Parse(m.Groups[2].Value);
                    IEnumerable<ulong> actualAddresses = mask.Apply(rawAddress);
                    foreach (ulong actualAddress in actualAddresses)
                    {
                        values[actualAddress] = value;
                    }
                }

            }

            foreach (KeyValuePair<ulong, ulong> kvp in values)
            {
                result += kvp.Value;
            }

            return result;
        }
    }
}
