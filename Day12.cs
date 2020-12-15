using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day12
    {
        private static readonly Regex InstructionRegex = new Regex(@"([FLRNSEW])(\d*)", RegexOptions.Compiled);

        public class Waypoint
        {
            public long Distance_NS { get; private set; }
            public long Distance_EW { get; private set; }

            public Waypoint(long ns, long ew)
            {
                Distance_NS = ns;
                Distance_EW = ew;
            }

            public void Move(char key, long value)
            {
                switch (key)
                {
                    case 'N':
                        Distance_NS += value;
                        break;
                    case 'S':
                        Distance_NS -= value;
                        break;
                    case 'E':
                        Distance_EW += value;
                        break;
                    case 'W':
                        Distance_EW -= value;
                        break;
                }
            }

            public void Rotate(int angle)
            {
                long temp;

                switch (angle)
                {
                    case 0:
                        break;
                    case 90:
                        // E -> S -> W -> N
                        temp = Distance_EW;
                        Distance_EW = Distance_NS;
                        Distance_NS = -temp;
                        break;
                    case 180:
                        // N -> S, E -> W
                        Distance_EW = -Distance_EW;
                        Distance_NS = -Distance_NS;
                        break;
                    case 270:
                        // E -> N -> W -> S
                        temp = Distance_EW;
                        Distance_EW = -Distance_NS;
                        Distance_NS = temp;
                        break;
                    }
                }

        }

        public class Ship
        {
            private readonly Waypoint m_Waypoint;

            public long Distance_NS { get; private set; }
            public long Distance_EW { get; private set; }

            public Ship(Waypoint wp)
            {
                m_Waypoint = wp;
                Distance_NS = 0;
                Distance_EW = 0;
            }

            public void Move(String instruction)
            {
                Match m = InstructionRegex.Match(instruction);
                char key = m.Groups[1].Value[0];
                long value = long.Parse(m.Groups[2].Value);

                switch (key)
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W':
                        m_Waypoint.Move(key, value);
                        break;
                    case 'L':
                        m_Waypoint.Rotate((int)(360 - value % 360));
                        break;
                    case 'R':
                        m_Waypoint.Rotate((int)(value % 360));
                        break;
                    case 'F':
                        Distance_NS += value * m_Waypoint.Distance_NS;
                        Distance_EW += value * m_Waypoint.Distance_EW;
                        break;
                }
            }
        }

        public class Ship_Part1
        {
            // Lookup from current Direction based on turn amount
            Dictionary<(char, long), char> NewDirection = new Dictionary<(char, long), char> 
            {
                { ('E', 0), 'E' },
                { ('E', 90), 'S' },
                { ('E', 180), 'W' },
                { ('E', 270), 'N' },
                { ('S', 0), 'S' },
                { ('S', 90), 'W' },
                { ('S', 180), 'N' },
                { ('S', 270), 'E' },
                { ('W', 0), 'W' },
                { ('W', 90), 'N' },
                { ('W', 180), 'E' },
                { ('W', 270), 'S' },
                { ('N', 0), 'N' },
                { ('N', 90), 'E' },
                { ('N', 180), 'S' },
                { ('N', 270), 'W' },
            };

            public char Direction { get; private set; } = 'E';
            public long Distance_NS { get; private set; }
            public long Distance_EW { get; private set; }

            public void Move(String instruction)
            {
                Match m = InstructionRegex.Match(instruction);
                char key = m.Groups[1].Value[0];
                long value = long.Parse(m.Groups[2].Value);

                if (key == 'F')
                {
                    key = Direction;
                }

                switch (key)
                {
                    case 'N':
                        Distance_NS += value;
                        break;
                    case 'S':
                        Distance_NS -= value;
                        break;
                    case 'E':
                        Distance_EW += value;
                        break;
                    case 'W':
                        Distance_EW -= value;
                        break;
                    case 'L':
                        (char, long) changeLeft = (Direction, ((360 - value)% 360));
                        Direction = NewDirection[changeLeft];
                        break;
                    case 'R':
                        (char, long) changeRight = (Direction, (value % 360));
                        Direction = NewDirection[changeRight];
                        break;
                }
            }
        }

        public Int64 Process(String[] input)
        {
            Int64 result = 0;

            //input = new String[] {
            //    "F10",
            //    "N3",
            //    "F7",
            //    "R90",
            //    "F11",
            //};

            Waypoint waypoint = new Waypoint(1, 10);
            Ship ship = new Ship(waypoint);

            foreach (String instruction in input)
            {
                ship.Move(instruction);
            }

            long distance_NS = ship.Distance_NS < 0 ? -ship.Distance_NS : ship.Distance_NS;
            long distance_EW = ship.Distance_EW < 0 ? -ship.Distance_EW : ship.Distance_EW;
            result = distance_NS + distance_EW;

            return result;
        }

    }
}
