use std::fs::File;
use std::io::{prelude::*, BufReader};

fn main() {
    let input = File::open("input.txt").expect("Couldn't read input file!");
    let reader = BufReader::new(input);
    let mut numbers = Vec::new();
    for line in reader.lines() {
        numbers.push(
            line.expect("No line!")
                .parse::<i32>()
                .expect("Couldn't parse number"),
        );
    }
    for number1 in &numbers {
        for number2 in &numbers {
            if number1 + number2 == 2020 {
                println!("{}", number1 * number2);
                return ();
            }
        }
    }
}
