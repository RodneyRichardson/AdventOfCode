# aoc_template.py

import pathlib
import sys
from copy import deepcopy


def parse(puzzle_input):
    """Parse input."""
    data = [[int(x) for x in list(line.strip())] for line in puzzle_input.split('\n') if len(line) > 0]
    return data

def part1(data):
    """Solve part 1."""
    left = deepcopy(data)
    map = left
    for row in range(len(data)):
        map[row][0] = True
        for col in range(len(data[row]) - 1):
            map[row][col + 1] = data[row][col+1] > max(data[row][:col+1])

    right = deepcopy(data)
    map = right
    for row in range(len(data)):
        map[row][len(data[row]) - 1] = True
        for col in range(len(data[row]) - 1, 0, -1):
            map[row][col - 1] = data[row][col-1] > max(data[row][col:])

    top = deepcopy(data)
    map = top
    for col in range(len(data[0])):
        map[0][col] = True
        for row in range(len(data) - 1):
            map[row+1][col] = data[row+1][col] > max((data[i][col] for i in range(0, row + 1)))

    bottom = deepcopy(data)
    map = bottom
    for col in range(len(data[0])):
        map[len(data) - 1][col] = True
        for row in range(len(data) - 1, 0, -1):
            map[row-1][col] = data[row-1][col] > max((data[i][col] for i in range(row, len(data))))

    # Combine
    count = 0
    for row in range(len(data)):
        for col in range(len(data[0])):
            seen = left[row][col] or right[row][col] or top[row][col] or bottom[row][col]
            if seen:
                count += 1
    return count

def calculate_score(data, tree_row, tree_col):
    tree = data[tree_row][tree_col]

    # down 
    down = 0
    for row in range(tree_row + 1, len(data)):
        col = tree_col
        down += 1
        if (data[row][col] >= tree):
            break
    # up
    up = 0
    for row in range(tree_row - 1, -1, -1):
        col = tree_col
        up += 1
        if (data[row][col] >= tree):
            break
    # right
    right = 0
    for col in range(tree_col + 1, len(data[0])):
        row = tree_row
        right += 1
        if (data[row][col] >= tree):
            break

    # left
    left = 0
    for col in range(tree_col - 1, -1, -1):
        row = tree_row
        left += 1
        if (data[row][col] >= tree):
            break

    return left * right * up * down

def part2(data):
    """Solve part 2."""
    score = deepcopy(data)
    max_score = 0
    for row in range(len(data)):
        for col in range(len(data[0])):
            if row == 0 or col == 0 or row == (len(data) - 1) or col == (len(data[0]) - 1):
                score[row][col] = 0
            else:
                score[row][col] = calculate_score(data, row, col)
                max_score = max(max_score, score[row][col])
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
