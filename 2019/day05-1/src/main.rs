use std::io::{self, Read};

fn main() {
	let mut input_text = String::new();
	io::stdin()
		.read_to_string(&mut input_text)
		.expect("Failed reading from stdin.");
	for noun in 0..100 {
		for verb in 0..100 {
			let memory = execute_script(&input_text, noun, verb);
			if memory[0] == 19690720 {
				println!("{}", dump_memory(&memory));
				println!("{}", 100 * noun + verb);
				return;
			}
		}
	}
}

fn execute_script(script: &String, noun: i32, verb: i32) -> Vec<i32> {
	let mut memory = parse_program(script);
	memory[1] = noun;
	memory[2] = verb;
	let mut pointer: usize = 0;
	let mut finished = false;
	while !finished {
		let exec_result = execute_instruction(&mut memory, pointer);
		pointer = exec_result.0;
		finished = exec_result.1;
	}
	return memory;
}

fn execute_instruction(memory: &mut Vec<i32>, pointer: usize) -> (usize, bool) {
	let mut new_pointer = pointer;
	if memory[pointer] == 99 {
		return (pointer, true);
	}

	let arg1 = memory[pointer + 1] as usize;
	let arg2 = memory[pointer + 2] as usize;
	let arg3 = memory[pointer + 3] as usize;
	match memory[pointer] {
		1 => {
			// println!("Adding {}, {} -> {}", arg1, arg2, target);
			memory[arg3] = memory[arg1] + memory[arg2];
			new_pointer += 4;
		}
		2 => {
			// println!("Multiplying {}, {} -> {}", arg1, arg2, target);
			memory[arg3] = memory[arg1] * memory[arg2];
			new_pointer += 4;
		}
		3 => {
			let mut input_text = String::new();
			io::stdin()
				.read_to_string(&mut input_text)
				.expect("Failed reading from stdin.");
			memory[arg1] = input_text.parse().expect("Invalid input");
			new_pointer += 2
		}
		4 => {
			println!("{}", arg1);
			new_pointer += 2;
		}
		_ => {
			println!("Unrecognized optcode: {}", memory[pointer]);
			return (pointer, true);
		}
	};
	return (new_pointer, false);
}

fn parse_program(text: &String) -> Vec<i32> {
	let mut vector = Vec::new();
	for opcode in text.split(",") {
		vector.push(opcode.trim().parse().expect("Unable to parse opcodes."))
	}
	return vector;
}

fn dump_memory(memory: &Vec<i32>) -> String {
	let mut output = String::new();
	for value in memory {
		let value_str = value.to_string();
		output.push_str(&value_str);
		output.push_str(",");
	}
	return output;
}
