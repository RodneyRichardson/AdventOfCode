# aoc_template.py

import pathlib
import sys
import re
from dataclasses import dataclass

@dataclass
class Move:
    count: int
    from_index: int
    to_index: int

def parse_board(board_data):
    last_line = board_data.split('\n')[-1]
    stack_count = int((len(last_line) + 1) / 4)

    board = [[] for x in range(stack_count)]
    for line in reversed(board_data.split('\n')[:-1]):
        for i in range(stack_count):
            col = (i * 4) + 1
            if line[col] != ' ':
                board[i].append(line[col])
    return board

def parse_moves(move_data):
    moves = []
    for line in move_data.strip().split('\n'):
        m = re.match('move (\d+) from (\d) to (\d)', line)
        moves.append(Move(int(m.group(1)), int(m.group(2)) - 1, int(m.group(3)) - 1))
    return moves

def parse(puzzle_input):
    """Parse input."""
    board_data, move_data = puzzle_input.split('\n\n')
    board = parse_board(board_data)
    moves = parse_moves(move_data)
    return board, moves

def part1(data):
    """Solve part 1."""
    board, moves = data
    for m in moves:
        for _ in range(m.count):
            board[m.to_index].append(board[m.from_index].pop())
    result = ''
    for stack in board:
        s = stack[-1]
        result = result + stack[-1]
    return result

def part2(data):
    """Solve part 2."""
    board, moves = data
    for m in moves:
        board[m.to_index].extend(board[m.from_index][-m.count:])
        for _ in range(m.count):
            board[m.from_index].pop()
    result = ''
    for stack in board:
        s = stack[-1]
        result = result + stack[-1]
    return result

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    data = parse(puzzle_input)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 5

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
