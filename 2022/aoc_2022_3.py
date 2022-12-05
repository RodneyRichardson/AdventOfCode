# aoc_template.py

import pathlib
import sys
import itertools

def get_priority(item):
    if item.islower():
        priority = ord(item) - ord('a') + 1
    else:
        priority = ord(item) - ord('A') + 27
    return priority

def parse(puzzle_input):
    """Parse input."""
    data = [line.strip() for line in puzzle_input.split('\n')]
    return data

def part1(data):
    """Solve part 1."""
    common_items = []
    for rucksack_items in data:
        compartment_size = int(len(rucksack_items)/2)
        compartment1 = set(rucksack_items[0:compartment_size])
        compartment2 = set(rucksack_items[compartment_size:])
        common_items.extend(compartment1.intersection(compartment2))

    score = sum(get_priority(item) for item in common_items)
    return score


def part2(data):
    """Solve part 2."""
    badges = []
    for i in range(0, len(data), 3):
        rucksack1 = set(data[i])
        rucksack2 = set(data[i+1])
        rucksack3 = set(data[i+2])
        badges.extend(rucksack1.intersection(rucksack2).intersection(rucksack3))
    score = sum(get_priority(badge) for badge in badges)
    return score

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 3

    inputs_path = pathlib.Path(__file__).parent / 'Inputs' 
    paths = [ 
        inputs_path / f'day{day}example.txt',
        inputs_path / f'day{day}input.txt'
    ]

    for path in paths:
        print(f"{path}:")
        puzzle_input = pathlib.Path(path).read_text().strip()
        solutions = solve(puzzle_input)
        print("\n".join(str(solution) for solution in solutions))
