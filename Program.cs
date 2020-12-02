using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    class Program
    {
        static async Task Main(string[] args)
        {
            String[] input = await File.ReadAllLinesAsync(@"Inputs\InputDay2.txt");

            Int64 result = new Day2().Process(input.Select(s => new Day2Data(s)));

        }

    }
}
