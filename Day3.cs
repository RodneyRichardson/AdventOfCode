using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class TreeMap
    {
        private readonly List<List<bool>> m_Data;
        private readonly int m_XWidth;

        public TreeMap(String[] input)
        {
            m_Data = new List<List<bool>>(input.Length);
            foreach (String line in input)
            {
                m_Data.Add(line.Select(c => c == '#').ToList());
            }
            m_XWidth = input[0].Length;
        }

        public UInt64 CountTrees(int xStep, int yStep)
        {
            UInt64 count = 0;

            for (int x = xStep % m_XWidth, y = yStep; y < m_Data.Count; x = (x + xStep) % m_XWidth, y += yStep)
            {
                if (m_Data[y][x])
                {
                    count++;
                }
            }
            return count;
        }
    }

    public class Day3
    {
        public UInt64 Process(String[] input)
        {
            UInt64 result = 0;

            TreeMap map = new TreeMap(input);
            result = 
                map.CountTrees(1, 1) *
                map.CountTrees(3, 1) *
                map.CountTrees(5, 1) *
                map.CountTrees(7, 1) *
                map.CountTrees(1, 2);
            return result;
        }
    }
}
