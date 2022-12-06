# aoc_template.py

import pathlib
import sys

def find_marker(line, size):
    for i in range(len(line) - size):
        snippet = line[i:i+size]
        if len(set(snippet)) == size:
            return i + size
    return -1

def parse(puzzle_input):
    """Parse input."""
    lines = [line for line in puzzle_input.split('\n') if len(line) > 0]
    return lines

def part1(data):
    """Solve part 1."""
    size = 4
    results = [find_marker(line, size) for line in data]
    return results

def part2(data):
    """Solve part 2."""
    size = 14
    results = [find_marker(line, size) for line in data]
    return results

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 6

    inputs_path = pathlib.Path(__file__).parent / 'Inputs' 
    paths = [ 
        inputs_path / f'day{day}example.txt',
        inputs_path / f'day{day}input.txt'
    ]

    for path in paths:
        print(f"{path}:")
        puzzle_input = pathlib.Path(path).read_text()
        solutions = solve(puzzle_input)
        print("\n".join(str(solution) for solution in solutions))
