use std::cmp::{max, min};
use std::collections::HashSet;
use std::io::{self, Read};
use std::iter::FromIterator;

#[derive(Eq, PartialEq, Hash, Clone)]
struct Point {
    x: i32,
    y: i32,
}

type Path = Vec<Point>;

fn main() {
    let mut input_text = String::new();
    io::stdin()
        .read_to_string(&mut input_text)
        .expect("Failed reading from stdin.");
    let mut lines = input_text.lines();
    let path_a = path_from_string(lines.next().expect("Not enough lines!"));
    let path_b = path_from_string(lines.next().expect("Not enough lines!"));
    let intersections = path_intersections(&path_a, &path_b);
    let manhattan_distance =
        shortest_manhattan_distance(&intersections).expect("No intersections!");
    println!("Manhattan distance: {}", manhattan_distance);
    let length = shortest_wire_length(&intersections, &path_a, &path_b);
    println!("Wire length: {}", length);
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

        let range_start_x = min(start_x, new_x);
        let range_end_x = max(start_x, new_x);
        let range_start_y = min(start_y, new_y);
        let range_end_y = max(start_y, new_y);

        let mut first = true;
        if new_x > start_x || new_y > start_y {
            for x in (range_start_x)..=(range_end_x) {
                for y in (range_start_y)..=(range_end_y) {
                    if first {
                        first = false
                    } else {
                        path.push(Point { x: x, y: y });
                    }
                }
            }
        } else {
            for x in ((range_start_x)..=(range_end_x)).rev() {
                for y in ((range_start_y)..=(range_end_y)).rev() {
                    if first {
                        first = false
                    } else {
                        path.push(Point { x: x, y: y });
                    }
                }
            }
        }
        start_x = new_x;
        start_y = new_y;
    }
    return path;
}

fn path_intersections(path_a: &Path, path_b: &Path) -> Vec<Point> {
    let mut result = Vec::new();
    let path_set_b: HashSet<&Point> = HashSet::from_iter(path_b.iter().clone());
    for point in path_a {
        if path_set_b.contains(point) {
            result.push(point.clone());
        }
    }
    return result;
}

fn shortest_wire_length(intersections: &Vec<Point>, path_a: &Path, path_b: &Path) -> i32 {
    let mut shortest_length = None;
    for intersection in intersections {
        let length_a = path_a
            .iter()
            .position(|point| point == intersection)
            .unwrap()
            + 1;
        let length_b = path_b
            .iter()
            .position(|point| point == intersection)
            .unwrap()
            + 1;
        let total_length = (length_a + length_b) as i32;

        if shortest_length.is_none() {
            shortest_length = Some(total_length);
        } else {
            if total_length < shortest_length.unwrap() {
                shortest_length = Some(total_length)
            }
        }
    }
    return shortest_length.unwrap();
}

fn shortest_manhattan_distance(points: &Vec<Point>) -> Option<i32> {
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
