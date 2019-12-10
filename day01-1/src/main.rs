use std::fs::File;
use std::io::{prelude::*, BufReader};

fn main() {
    let mut total = 0;
    let input = File::open("input.txt").expect("Couldn't read input file!");
    let reader = BufReader::new(input);
    for line in reader.lines() {
        let mass: u32 = line.expect("No line!").trim().parse().expect("Line isn't parseable!");
        total += (mass / 3) - 2;
    }
    println!("{}", total);
}

// fn fuelRequired(mass: u32) -> u32 {
//     return (mass / 3) - 2
// }