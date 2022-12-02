# aoc_2022_1.py

import pathlib
import sys
from heapq import nlargest

def parse(puzzle_input):
    """Parse input."""
    values = [[int(x) for x in elf.strip().split("\n")] for elf in puzzle_input.split('\n\n')]
    return values        

def part1(data):
    """Solve part 1."""
    max_value = max(sum(values) for values in data)
    return max_value

def part2(data):
    """Solve part 2."""
    total = sum(nlargest(3, (sum(x) for x in data)))
    return total

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 1

    inputs_path = pathlib.Path(__file__).parent / 'Inputs' 
    paths = [ 
        inputs_path / f'day{day}example.txt',
        inputs_path / f'day{day}input.txt'
    ]

    for path in paths:
        print(f"{path}:")
#        puzzle_input = pathlib.Path(path).read_text().strip()
        puzzle_input = pathlib.Path(path).read_text()
        solutions = solve(puzzle_input)
        print("\n".join(str(solution) for solution in solutions))
