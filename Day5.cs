using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class BoardingPass
    {
        public int Row  { get; }
        public int Seat { get; }
        public int SeatID { get; }

        public BoardingPass(String row, String seat)
        {
            //String binaryRow = row.Replace('F', '0').Replace('B', '1');
            //String binarySeat = seat.Replace('L', '0').Replace('R', '1');
            
            Row = Convert.ToInt32(row.Replace('F', '0').Replace('B', '1'), 2);
            Seat = Convert.ToInt32(seat.Replace('L', '0').Replace('R', '1'), 2);
            SeatID = Row * 8 + Seat;
        }

        public override string ToString()
        {
            return $"{Row}:{Seat} ({SeatID})";
        }
    }

    public class Day5
    {
        public UInt64 Process(String[] input)
        {
            UInt64 result = 0;

//            input = new string[] {
//"BFFFBBFRRR", //: row 70, column 7, seat ID 567.
//"FFFBBBFRRR", //: row 14, column 7, seat ID 119.
//"BBFFBBFRLL", //: row 102, column 4, seat ID 820.        
//            };

            List<BoardingPass> passes = new List<BoardingPass>();
            foreach (String line in input)
            {
                BoardingPass pass = new BoardingPass(line.Substring(0, 7), line.Substring(7, 3));
                passes.Add(pass);
            }
            //result = (UInt64) passes.Select(p => p.SeatID).Max();
            List<BoardingPass> sorted = passes.OrderBy(p => p.SeatID).ToList();
            Int64 maxRow = passes.Select(p => p.Row).Max();

            for (int i = 0; i < passes.Count - 1; i++)
            {
                int nextSeatID;
                BoardingPass pass = sorted[i];
                if (pass.Seat == 7)
                {
                    nextSeatID = (pass.Row + 1) * 8;
                }
                else
                {
                    nextSeatID = pass.SeatID + 1;
                }
                if (sorted[i + 1].SeatID != nextSeatID)
                {
                    result = (UInt64)nextSeatID;
                    break;
                }
            }
            

            return result;
        }
    }
}
