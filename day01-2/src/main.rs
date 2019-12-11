use std::fs::File;
use std::io::{prelude::*, BufReader};

fn main() {
    let mut total = 0;
    let input = File::open("input.txt").expect("Couldn't read input file!");
    let reader = BufReader::new(input);
    for line in reader.lines() {
        let mass: i32 = line
            .expect("No line!")
            .trim()
            .parse()
            .expect("Line isn't parseable!");
        let fuel = fuel_required(mass);
        total += fuel;
        println!("Mass {} requires {} fuel, total {}", mass, fuel, total)
    }
    println!("{}", total);
}

fn fuel_required(mass: i32) -> i32 {
    let fuel = (mass / 3) - 2;
    if fuel <= 0 {
        return 0;
    } else {
        return fuel + fuel_required(fuel);
    }
}
