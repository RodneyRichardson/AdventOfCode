using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class PassportField
    {
        Regex fourDigitRegex = new Regex(@"^(\d{4})$", RegexOptions.Compiled);
        Regex heightRegex = new Regex(@"^(\d*)(in|cm)$", RegexOptions.Compiled);
        Regex hairRegex = new Regex(@"^#([0-9a-f]{6})$", RegexOptions.Compiled);
        Regex eyeRegex = new Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$", RegexOptions.Compiled);
        Regex nineDigitRegex = new Regex(@"^(\d{9})$", RegexOptions.Compiled);

        public String Key { get; }
        public String Value { get; }
        public bool IsValid { get; }

        public PassportField(String key, String value)
        {
            Key = key;
            Value = value;
            IsValid = Validate(key, value);
        }

        public override string ToString()
        {
            return $"{Value}: {IsValid}";
        }

        private bool Validate(string key, string value)
        {
            bool isValid = false;
            Match m;
            switch (key)
            {
                case "byr":
                    m = fourDigitRegex.Match(value);
                    if (m.Success)
                    {
                        int year = int.Parse(m.Groups[1].Value);
                        isValid = year >= 1920 && year <= 2002;
                    }
                    break;

                case "iyr":
                    m = fourDigitRegex.Match(value);
                    if (m.Success)
                    {
                        int year = int.Parse(m.Groups[1].Value);
                        isValid = year >= 2010 && year <= 2020;
                    }
                    break;

                case "eyr":
                    m = fourDigitRegex.Match(value);
                    if (m.Success)
                    {
                        int year = int.Parse(m.Groups[1].Value);
                        isValid = year >= 2020 && year <= 2030;
                    }
                    break;

                case "hgt":
                    m = heightRegex.Match(value);
                    if (m.Success)
                    {
                        int height = int.Parse(m.Groups[1].Value);
                        switch (m.Groups[2].Value)
                        {
                            case "in":
                                isValid = height >= 59 && height <= 76;
                                break;

                            case "cm":
                                isValid = height >= 150 && height <= 193;
                                break;
                        }
                    }
                    break;

                case "hcl":
                    m = hairRegex.Match(value);
                    if (m.Success)
                    {
                        isValid = true;
                    }
                    break;

                case "ecl":
                    m = eyeRegex.Match(value);
                    if (m.Success)
                    {
                        isValid = true;
                    }
                    break;

                case "pid":
                    m = nineDigitRegex.Match(value);
                    if (m.Success)
                    {
                        isValid = true;
                    }
                    break;

                case "cid":
                    isValid = true;
                    break;
            }
            return isValid;
        }
    }

    public class PassportData
    {
        public bool IsValid { get; }
        public Dictionary<String, PassportField> Fields { get; }

        public PassportData(String input)
        {
            Fields = new Dictionary<string, PassportField>();
            foreach (String entry in input.Split())
            {
                String[] values = entry.Split(':', StringSplitOptions.RemoveEmptyEntries);
                String key = values[0].Trim();
                String value = values[1].Trim();
                Fields[key] = new PassportField(key, value);
            }

            bool hasAllFields =
                Fields.ContainsKey("byr") &&
                Fields.ContainsKey("iyr") &&
                Fields.ContainsKey("eyr") &&
                Fields.ContainsKey("hgt") &&
                Fields.ContainsKey("hcl") &&
                Fields.ContainsKey("ecl") &&
                Fields.ContainsKey("pid");
            bool allFieldsValid = Fields.Values.All(f => f.IsValid);
            IsValid = hasAllFields && allFieldsValid;
        }

    }

    public class Day04
    {
        public UInt64 Process(String[] input)
        {
            UInt64 result = 0;

            List<PassportData> passports = new List<PassportData>();
            StringBuilder sb = new StringBuilder();
            foreach (String line in input)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    PassportData data = new PassportData(sb.ToString());
                    passports.Add(data);
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
                PassportData data = new PassportData(sb.ToString());
                passports.Add(data);
                sb.Length = 0;
            }

            result = (UInt64)passports.Count(p => p.IsValid);

            return result;
        }
    }
}
