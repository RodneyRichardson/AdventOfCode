# aoc_template.py

import pathlib
import sys
from copy import deepcopy


def parse(puzzle_input):
    """Parse input."""
    data = [[int(x) for x in list(line.strip())] for line in puzzle_input.split('\n') if len(line) > 0]
    return data

def can_be_seen(tree_height, views):
    seen = any(tree_height > max(view) for view in views)
    return seen

def part1(data):
    """Solve part 1."""
    seen = deepcopy(data)
    for row in range(len(data)):
        for col in range(len(data[0])):
            if row == 0 or col == 0 or row == (len(data) - 1) or col == (len(data[0]) - 1):
                seen[row][col] = True
            else:
                seen[row][col] = can_be_seen(data[row][col], [
                    list(reversed(data[row][:col])),
                    data[row][col+1:],
                    list(reversed([data[i][col] for i in range(0, row)])),
                    [data[i][col] for i in range(row+1, len(data))]
                ]
                )
    # count all the "True" values
    count = sum(1 for sub in seen for j in sub if j)
    return count

def calculate_score(data, tree_row, tree_col):
    tree_height = data[tree_row][tree_col]

    # down 
    down = 0
    for row in range(tree_row + 1, len(data)):
        col = tree_col
        down += 1
        if (data[row][col] >= tree_height):
            break

    # up
    up = 0
    for row in range(tree_row - 1, -1, -1):
        col = tree_col
        up += 1
        if (data[row][col] >= tree_height):
            break
    # right
    right = 0
    for col in range(tree_col + 1, len(data[0])):
        row = tree_row
        right += 1
        if (data[row][col] >= tree_height):
            break

    # left
    left = 0
    for col in range(tree_col - 1, -1, -1):
        row = tree_row
        left += 1
        if (data[row][col] >= tree_height):
            break

    return left * right * up * down

def calculate_score2(tree_height, views):
    score = 1 
    for view in views:
        score = score * (next((index for index, height in enumerate(view) if height >= tree_height), len(view) - 1) + 1)
    return score

def part2(data):
    """Solve part 2."""
    #score = deepcopy(data)
    score2 = deepcopy(data)
    max_score = 0
    for row in range(len(data)):
        for col in range(len(data[0])):
            if row == 0 or col == 0 or row == (len(data) - 1) or col == (len(data[0]) - 1):
                #score[row][col] = 0
                score2[row][col] = 0
            else:
                #score[row][col] = calculate_score(data, row, col)
                score2[row][col] = calculate_score2(data[row][col], [
                    list(reversed(data[row][:col])),
                    data[row][col+1:],
                    list(reversed([data[i][col] for i in range(0, row)])),
                    [data[i][col] for i in range(row+1, len(data))]
                ]
                )
                max_score = max(max_score, score2[row][col])
    return max_score

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 8

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
