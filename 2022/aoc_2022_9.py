# aoc_template.py

import pathlib
import sys

moves = {
    'U': (0, 1),
    'D': (0, -1),
    'L': (-1, 0),
    'R': (1, 0)
}

def sign(x):
    if x < 0:
        return -1
    if x > 0:
        return 1
    return 0

def parse(puzzle_input):
    """Parse input."""
    data = [line.strip().split(' ') for line in puzzle_input.split('\n') if len(line) > 0]
    return data

def part1(data):
    """Solve part 1."""
    head_x, head_y = 0, 0
    tail_x, tail_y = 0, 0
    visited = {(0, 0)}

    for line in data:
        direction = line[0]
        step_count = int(line[1])

        for _ in range(step_count):
            head_x, head_y = head_x + moves[direction][0], head_y + moves[direction][1]
            dist_x, dist_y = head_x - tail_x, head_y - tail_y

            if (abs(dist_x) >= 2) or (abs(dist_y) >= 2):
                tail_x, tail_y = tail_x + sign(dist_x), tail_y + sign(dist_y)

            visited |= {(tail_x, tail_y)}

    return len(visited)

def part2(data):
    """Solve part 2."""
    rope  = [(0, 0)] * 10
    head_x, head_y = 0, 0
    tail_x, tail_y = 0, 0
    visited = {(0, 0)}

    for line in data:
        direction = line[0]
        step_count = int(line[1])

        for _ in range(step_count):
            head_x, head_y = rope[0]
            rope[0] = head_x + moves[direction][0], head_y + moves[direction][1]

            for i in range(len(rope) - 1):
                head_x, head_y = rope[i]
                tail_x, tail_y = rope[i + 1]
                dist_x, dist_y = head_x - tail_x, head_y - tail_y

                if (abs(dist_x) >= 2) or (abs(dist_y) >= 2):
                    rope[i + 1] = tail_x + sign(dist_x), tail_y + sign(dist_y)

            visited |= {rope[-1]}

    return len(visited)

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 9

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
