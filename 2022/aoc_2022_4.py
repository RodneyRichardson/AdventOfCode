# aoc_template.py

import pathlib
import sys


def to_ranges(line):
    first, second = line.split(',')
    return [to_range(first), to_range(second)]

def to_range(text):
    first, second = text.split('-')
    return range(int(first), int(second) + 1)

def parse(puzzle_input):
    """Parse input."""
    data = [line.strip().split(',') for line in puzzle_input.split('\n') if len(line) > 0]
    ranges = [to_ranges(line) for line in puzzle_input.split('\n') if len(line) > 0]
    return ranges

def part1(data):
    """Solve part 1."""
    count = 0
    for section1, section2 in data:
        set1 = set(section1)
        set2 = set(section2)
        if (set1.issubset(set2) or set2.issubset(set1)):
            count += 1
    return count

def part2(data):
    """Solve part 2."""
    count = 0
    for section1, section2 in data:
        set1 = set(section1)
        set2 = set(section2)
        if len(set1.intersection(set2)) > 0:
            count += 1
    return count

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 4

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
