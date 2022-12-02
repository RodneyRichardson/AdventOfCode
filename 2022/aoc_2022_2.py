# aoc_template.py

import pathlib
import sys

scores = {
    'AX' : 1 + 3 , # Rock, Rock (Draw)
    'AY' : 2 + 6 , # Rock, Paper (Win)
    'AZ' : 3 + 0 , # Rock, Scissors (Lose)
    'BX' : 1 + 0 , # Paper, Rock (Lose)
    'BY' : 2 + 3 , # Paper, Paper (Draw)
    'BZ' : 3 + 6 , # Paper, Scissors (Win)
    'CX' : 1 + 6 , # Scissors, Rock (Win)
    'CY' : 2 + 0 , # Scissors, Paper (Lose)
    'CZ' : 3 + 3 , # Scissors, Scissors (Draw)
}

part_2_strategy = {
    'AX' : 'Z' , # Rock, Lose (Scissors)
    'AY' : 'X' , # Rock, Draw (Rock)
    'AZ' : 'Y' , # Rock, Win (Paper)
    'BX' : 'X' , # Paper, Lose (Rock)
    'BY' : 'Y' , # Paper, Draw (Paper)
    'BZ' : 'Z' , # Paper, Win (Scissors)
    'CX' : 'Y' , # Scissors, Lose (Paper)
    'CY' : 'Z' , # Scissors, Draw (Scissors)
    'CZ' : 'X' , # Scissors, Win (Rock)

}

def parse(puzzle_input):
    """Parse input."""
    data = [line.strip().split(' ') for line in puzzle_input if len(line) > 0]
    return data

def part1(data):
    """Solve part 1."""
    total = sum([scores[''.join(game)] for game in data])
    return total

def part2(data):
    """Solve part 2."""
    strategies = [part_2_strategy[''.join(game)] for game in data]
    games = zip([game[0] for game in data], strategies)
    total = part1(games)
    return total

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 2

    paths = [ 
        f'C:\\working\\github\\RodneyRichardson\\AdventOfCode\\2022\\Inputs\\day{day}example.txt',
        f'C:\\working\\github\\RodneyRichardson\\AdventOfCode\\2022\\Inputs\\day{day}input.txt',
    ]

    for path in paths:
        print(f"{path}:")
        puzzle_input = pathlib.Path(path).open().readlines()
        solutions = solve(puzzle_input)
        print("\n".join(str(solution) for solution in solutions))
