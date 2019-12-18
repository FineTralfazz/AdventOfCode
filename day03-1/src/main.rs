use std::cmp::{max, min};
use std::collections::HashSet;
use std::io::{self, Read};
use std::ops::RangeInclusive;

#[derive(Eq, PartialEq, Hash)]
struct Point {
    x: i32,
    y: i32,
}

type Path = HashSet<Point>;

fn main() {
    let mut input_text = String::new();
    io::stdin()
        .read_to_string(&mut input_text)
        .expect("Failed reading from stdin.");
    let mut lines = input_text.lines();
    let path_a = path_from_string(lines.next().expect("Not enough lines!"));
    let path_b = path_from_string(lines.next().expect("Not enough lines!"));
    let intersections = path_intersections(path_a, path_b);
    let distance = shortest_distance(intersections).expect("No intersections!");
    println!("Closest point is {} units away", distance);
}

fn path_from_string(path_string: &str) -> Path {
    let mut path = Path::new();
    let mut start_x = 0;
    let mut start_y = 0;
    for movement in path_string.split(",") {
        let direction = movement.chars().nth(0).expect("Got empty move string");
        let distance: i32 = movement[1..].parse().expect("Couldn't parse distance");
        let mut new_x = start_x;
        let mut new_y = start_y;
        match direction {
            'U' => new_y += distance,
            'R' => new_x += distance,
            'D' => new_y -= distance,
            'L' => new_x -= distance,
            _ => panic!("Unrecognized direction: {}", direction),
        }
        let mut first = true;
        for x in RangeInclusive::new(min(start_x, new_x), max(start_x, new_x)) {
            for y in RangeInclusive::new(min(start_y, new_y), max(start_y, new_y)) {
                if first {
                    first = false
                } else {
                    // println!("Adding point {}, {}", x, y);
                    path.insert(Point { x: x, y: y });
                }
            }
        }
        start_x = new_x;
        start_y = new_y;
    }
    println!("Path has {} points", path.len());
    return path;
}

fn path_intersections(path_a: Path, path_b: Path) -> Vec<Point> {
    let mut result = Vec::new();
    for point in path_a {
        if path_b.get(&point).is_some() {
            result.push(point);
        }
    }
    return result;
}

fn shortest_distance(points: Vec<Point>) -> Option<i32> {
    let mut result = None;
    for point in points {
        let distance = point.x.abs() + point.y.abs();
        if result.is_none() {
            result = Some(distance);
        } else {
            if distance < result.unwrap() {
                result = Some(distance)
            }
        }
    }
    return result;
}
