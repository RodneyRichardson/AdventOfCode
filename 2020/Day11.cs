using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day11
    {
        const char OCCUPIED = '#';
        const char EMPTY = 'L';
        const char FLOOR = '.';

        public class SeatingPlan
        {
            public List<String[]> Plans { get; }
            public int XMax { get; }
            public int YMax { get; }

            public SeatingPlan(String[] input)
            {
                Plans = new List<string[]> { input };
                XMax = input[0].Length;
                YMax = input.Length;
            }

            public bool UpdatePlan()
            {
                String[] currentPlan = Plans[^1];

                List<StringBuilder> updatedPlan = new List<StringBuilder>();
                foreach (String row in currentPlan)
                {
                    updatedPlan.Add(new StringBuilder(row));
                }

                for (int y = 0; y < YMax; y++)
                {
                    StringBuilder newRow = updatedPlan[y];
                    for (int x = 0; x < XMax; x++)
                    {
                        newRow[x] = UpdateState_Part2(x, y, currentPlan);
                    }
                }

                String[] nextPlan = new string[YMax];
                for (int y = 0; y < YMax; y++)
                {
                    nextPlan[y] = updatedPlan[y].ToString();
                }

                Plans.Add(nextPlan);

                return AreEqual(nextPlan, currentPlan);
            }

            private bool AreEqual(string[] nextPlan, string[] currentPlan)
            {
                for (int y = 0; y < YMax; y++)
                {
                    if (nextPlan[y] != currentPlan[y])
                    {
                        return false;
                    }
                }
                return true;
            }

            private char UpdateState_Part1(int x, int y, string[] currentPlan)
            {
                char currentState = currentPlan[y][x];
                char newState = currentState;
                switch (currentState)
                {
                    case EMPTY:
                        if (IsUnoccupied(x - 1, y - 1, currentPlan) &&
                            IsUnoccupied(x + 0, y - 1, currentPlan) &&
                            IsUnoccupied(x + 1, y - 1, currentPlan) &&
                            IsUnoccupied(x - 1, y + 0, currentPlan) &&
                            IsUnoccupied(x + 1, y + 0, currentPlan) &&
                            IsUnoccupied(x - 1, y + 1, currentPlan) &&
                            IsUnoccupied(x + 0, y + 1, currentPlan) &&
                            IsUnoccupied(x + 1, y + 1, currentPlan))
                        {
                            newState = OCCUPIED;
                        }
                        break;

                    case OCCUPIED:
                        int count = 0;
                        if (IsOccupied(x - 1, y - 1, currentPlan)) count++;
                        if (IsOccupied(x + 0, y - 1, currentPlan)) count++;
                        if (IsOccupied(x + 1, y - 1, currentPlan)) count++;
                        if (IsOccupied(x - 1, y + 0, currentPlan)) count++;
                        if (IsOccupied(x + 1, y + 0, currentPlan)) count++;
                        if (IsOccupied(x - 1, y + 1, currentPlan)) count++;
                        if (IsOccupied(x + 0, y + 1, currentPlan)) count++;
                        if (IsOccupied(x + 1, y + 1, currentPlan)) count++;
                        if (count >= 5)
                        {
                            newState = EMPTY;
                        }

                        break;
                }

                return newState;
            }

            private char UpdateState_Part2(int x, int y, string[] currentPlan)
            {
                char currentState = currentPlan[y][x];
                char newState = currentState;
                switch (currentState)
                {
                    case EMPTY:
                        if (IsNextPositionOccupied(x, y, -1, -1, currentPlan) == false &&
                            IsNextPositionOccupied(x, y, +0, -1, currentPlan) == false &&
                            IsNextPositionOccupied(x, y, +1, -1, currentPlan) == false &&
                            IsNextPositionOccupied(x, y, -1, +0, currentPlan) == false &&
                            IsNextPositionOccupied(x, y, +1, +0, currentPlan) == false &&
                            IsNextPositionOccupied(x, y, -1, +1, currentPlan) == false &&
                            IsNextPositionOccupied(x, y, +0, +1, currentPlan) == false &&
                            IsNextPositionOccupied(x, y, +1, +1, currentPlan) == false)
                        {
                            newState = OCCUPIED;
                        }
                        break;

                    case OCCUPIED:
                        int count = 0;
                        if (IsNextPositionOccupied(x, y, -1, -1, currentPlan) != false) count++;
                        if (IsNextPositionOccupied(x, y, +0, -1, currentPlan) != false) count++;
                        if (IsNextPositionOccupied(x, y, +1, -1, currentPlan) != false) count++;
                        if (IsNextPositionOccupied(x, y, -1, +0, currentPlan) != false) count++;
                        if (IsNextPositionOccupied(x, y, +1, +0, currentPlan) != false) count++;
                        if (IsNextPositionOccupied(x, y, -1, +1, currentPlan) != false) count++;
                        if (IsNextPositionOccupied(x, y, +0, +1, currentPlan) != false) count++;
                        if (IsNextPositionOccupied(x, y, +1, +1, currentPlan) != false) count++;
                        if (count >= 5)
                        {
                            newState = EMPTY;
                        }

                        break;
                }

                return newState;
            }

            /// <summary>
            /// Looks to the next seat until it hits the edge.
            /// </summary>
            /// <param name="x">Position we're looking from</param>
            /// <param name="y">Position we're looking from</param>
            /// <param name="delta_x">Direction we're looking in</param>
            /// <param name="delta_y">Direction we're looking in</param>
            /// <param name="currentPlan">The seating plan</param>
            /// <returns>True if OCCUPIED, False if EMPTY, null if floor</returns>
            private bool IsNextPositionOccupied(int x, int y, int delta_x, int delta_y, string[] currentPlan)
            {
                int test_x = x + delta_x;
                int test_y = y + delta_y;

                while (test_x >= 0 && test_x < XMax && test_y >= 0 && test_y < YMax)
                {
                    switch (currentPlan[test_y][test_x])
                    {
                        case OCCUPIED: return true;
                        case EMPTY: return false;
                    }

                    test_x += delta_x;
                    test_y += delta_y;
                }
                // No seats in this direction
                return false;
            }

            private bool IsUnoccupied(int x, int y, string[] currentPlan)
            {
                bool isUnoccupied = true;
                if (x >= 0 && x < XMax && y >= 0 && y < YMax)
                {
                    // FLOOR or EMPTY
                    isUnoccupied = (currentPlan[y][x] != OCCUPIED);
                }
                return isUnoccupied;
            }

            private bool IsOccupied(int x, int y, string[] currentPlan)
            {
                bool isOccupied = false;
                if (x >= 0 && x < XMax && y >= 0 && y < YMax)
                {
                    isOccupied = (currentPlan[y][x] == OCCUPIED);
                }
                return isOccupied;
            }
        }

        public Int64 Process(String[] input)
        {
            Int64 result = 0;

            //input = new String[] {
            //    "L.LL.LL.LL",
            //    "LLLLLLL.LL",
            //    "L.L.L..L..",
            //    "LLLL.LL.LL",
            //    "L.LL.LL.LL",
            //    "L.LLLLL.LL",
            //    "..L.L.....",
            //    "LLLLLLLLLL",
            //    "L.LLLLLL.L",
            //    "L.LLLLL.LL",
            //};

            SeatingPlan plan = new SeatingPlan(input);

            int iterations = 0;
            while (!plan.UpdatePlan())
            {
                iterations++;
            }

            // Count seats
            foreach (String row in plan.Plans[^1])
            {
                result += row.Count(c => c == OCCUPIED);
            }


            return result;
        }

    }
}
