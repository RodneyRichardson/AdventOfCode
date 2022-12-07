# aoc_template.py

from dataclasses import dataclass
import pathlib
import sys

@dataclass
class File:
    name : str
    size : int

class Directory:
    def __init__(self, parent, name):
        self.name = name
        self.parent = parent    
        self.files : list(File) = []
        self.children = []

    def size(self):
        file_size = sum(file.size for file in self.files)
        child_size = sum(child.size() for child in self.children)
        return file_size + child_size

    def __repr__(self):
        return self.name

    def __iter__(self):
        yield self
        for child in self.children:
            for node in child:
                yield node


def process_cd(root, cwd, dir_name):
    match dir_name:
        case "..":
            return cwd.parent
        case "/":
            return root
        case _:
            return next(child for child in cwd.children if child.name == dir_name)

def process_ls(cwd, results):
    children = [r[4:] for r in results if r.startswith("dir")]
    for child_name in children:
        # Do we need to check for existing children?
        cwd.children.append(Directory(cwd, child_name))

    files = [r.split(" ") for r in results if not r.startswith("dir")]
    for file_size, file_name in files:
        cwd.files.append(File(file_name, int(file_size)))

def build_tree(data):
    root = Directory(None, "root")
    cwd = root
    for command in data:
        if command[0].startswith("cd"):
            cwd = process_cd(root, cwd, command[0][3:])
        elif command[0] == "ls":
            process_ls(cwd, command[1:])
    return root


def parse(puzzle_input):
    """Parse input."""
    commands = [[x for x in line.strip().split("\n")] for line in puzzle_input.split('$') if len(line) > 0]
    return commands

def part1(data):
    """Solve part 1."""
    root = build_tree(data)

    # Traverse and find directories
    result = sum(d.size() for d in root if d.size() <= 100000)
    return result

def part2(data):
    """Solve part 2."""
    root = build_tree(data)

    total_space = 70000000
    required_space = 30000000
    free_space = total_space - root.size()
    target_size = required_space - free_space

    target_dir = next(d for d in sorted(root, key=Directory.size) if d.size() >= target_size)
    min_size = target_dir.size()
    return min_size

def solve(puzzle_input):
    """Solve the puzzle for the given input."""
    data = parse(puzzle_input)
    solution1 = part1(data)
    solution2 = part2(data)

    return solution1, solution2

if __name__ == "__main__":
    day = 7

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
