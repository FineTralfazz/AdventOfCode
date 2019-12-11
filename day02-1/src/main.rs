use std::io::{self, Read};

fn main() {
	let mut input_text = String::new();
	io::stdin()
		.read_to_string(&mut input_text)
		.expect("Failed reading from stdin.");
	let mut memory = parse_program(input_text);
	let mut pointer: usize = 0;
	let mut finished = false;
	while !finished {
		let exec_result = execute_instruction(&mut memory, pointer);
		pointer = exec_result.0;
		finished = exec_result.1;
	}
	println!("{}", dump_memory(&memory));
}

fn execute_instruction(memory: &mut Vec<i32>, pointer: usize) -> (usize, bool) {
	if memory[pointer] == 99 {
		return (pointer, true);
	}

	let arg1 = memory[memory[pointer + 1] as usize];
	let arg2 = memory[memory[pointer + 2] as usize];
	let target = memory[pointer + 3] as usize;
	match memory[pointer] {
		1 => {
			println!("Adding {}, {} -> {}", arg1, arg2, target);
			memory[target] = arg1 + arg2;
		}
		2 => {
			println!("Multiplying {}, {} -> {}", arg1, arg2, target);
			memory[target] = arg1 * arg2;
		}
		_ => {
			println!("Unrecognized optcode: {}", memory[pointer]);
			return (pointer, true);
		}
	};
	return (pointer + 4, false);
}

fn parse_program(text: String) -> Vec<i32> {
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

// #[cfg(test)]
// mod tests {
// 	use super::*;

// 	#[test]
// 	fn test_parse_program() {
// 		assert_eq!([1,2,3], add_1(4));
// 	}
// }
