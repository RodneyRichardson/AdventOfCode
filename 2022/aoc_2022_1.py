# aoc_2022_1.py

import pathlib
import sys

def parse(puzzle_input):
    """Parse input."""
    values = []
    current_value = []
    for line in [x.strip() for x in puzzle_input]:
        if len(line) > 0:
            current_value.append(int(line))
        elif len(current_value) > 0:
            values.append(current_value)
            current_value = []

    if len(current_value) > 0:
            values.append(current_value)

    return values        

def part1(data):
    """Solve part 1."""
    max_value = max(sum(values) for values in data)
    return max_value

def part2(data):
    """Solve part 2."""
    sums = [sum(x) for x in data]
    sums.sort(reverse=True)
    return sum(sums[0:3])

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 1

    paths = [ 
        f'C:\\working\\github\\RodneyRichardson\\AdventOfCode\\2022\\Inputs\\day{day}example.txt',
        f'C:\\working\\github\\RodneyRichardson\\AdventOfCode\\2022\\Inputs\\day{day}input.txt',
    ]

    for path in paths:
        print(f"{path}:")
#        puzzle_input = pathlib.Path(path).read_text().strip()
        puzzle_input = pathlib.Path(path).open().readlines()
        solutions = solve(puzzle_input)
        print("\n".join(str(solution) for solution in solutions))
