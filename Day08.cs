using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventofCode2020
{
    public class Day08
    {

        public class GameEngine
        {
            public (bool, long) Execute(IList<Instruction> instructions)
            {
                bool terminated = false;
                long accumulator = 0;
                int index = 0;
                HashSet<int> executedInstructions = new HashSet<int>();

                while (index < instructions.Count &&
                       executedInstructions.Add(index))
                {
                    Instruction i = instructions[index];
                    switch (i.OpCode)
                    {
                        case "nop":
                            index++;
                            break;

                        case "acc":
                            accumulator += i.Argument1;
                            index++;
                            break;

                        case "jmp":
                            index += i.Argument1;
                            break;
                    }
                }
                if (index == instructions.Count)
                {
                    terminated = true;
                }

                return (terminated, accumulator);

            }
        }

        public struct Instruction
        {
            private static readonly Regex InstructionRegex = new Regex(
                @"(nop|acc|jmp) ([+-]\d*)",
                RegexOptions.Compiled);

            public String OpCode { get; }
            public Int32 Argument1 { get;  } 

            public Instruction(String line)
            {
                Match m = InstructionRegex.Match(line);
                OpCode = m.Groups[1].Value;
                Argument1 = Int32.Parse(m.Groups[2].Value);
            }

            public Instruction(String opCode, int argument1)
            {
                OpCode = opCode;
                Argument1 = argument1;
            }

            public override string ToString()
            {
                return $"{OpCode} {Argument1:+0;-#;+0}";
            }
        }

        public Int64 Process(String[] input)
        {
            //input = new String[] {
            //    "nop +0",
            //    "acc +1",
            //    "jmp +4",
            //    "acc +3",
            //    "jmp -3",
            //    "acc -99",
            //    "acc +1",
            //    "jmp -4",
            //    "acc +6",
            //};

            List<Instruction> instructions = new List<Instruction>();
            foreach (String line in input)
            {
                instructions.Add(new Instruction(line));
            }

            GameEngine engine = new GameEngine();
            for (int i = 0; i < instructions.Count; i++)
            {
                List<Instruction> fixedInstructions = null;
                if (instructions[i].OpCode == "nop")
                {
                    fixedInstructions = CopyAndReplace(instructions, i, new Instruction("jmp", instructions[i].Argument1));
                }
                else if (instructions[i].OpCode == "jmp")
                {
                    fixedInstructions = CopyAndReplace(instructions, i, new Instruction("nop", instructions[i].Argument1));
                }

                if (fixedInstructions != null)
                {
                    (bool terminated, Int64 result) = engine.Execute(fixedInstructions);
                    if (terminated)
                    {
                        return result;
                    }
                }
            }

            return 0;
        }

        private List<Instruction> CopyAndReplace(List<Instruction> instructions, int replaceIndex, Instruction instruction)
        {
            List<Instruction> copy = new List<Instruction>(instructions);
            copy[replaceIndex] = instruction;
            return copy;
        }
    }
}
