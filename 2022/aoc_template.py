# aoc_template.py

import pathlib
import sys

def parse(puzzle_input):
    """Parse input."""

def part1(data):
    """Solve part 1."""

def part2(data):
    """Solve part 2."""

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 1

    paths = [ 
        f'C:\\working\\github\\RodneyRichardson\\AdventOfCode\\2022\\Inputs\\day{day}example1.txt',
        f'C:\\working\\github\\RodneyRichardson\\AdventOfCode\\2022\\Inputs\\day{day}input.txt',
    ]

    for path in paths:
        print(f"{path}:")
        puzzle_input = pathlib.Path(path).open().readlines()
        solutions = solve(puzzle_input)
        print("\n".join(str(solution) for solution in solutions))
