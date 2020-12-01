// I forgot to save the solution to part 1. It wasn't interesting.
fn main() {
    let mut valid_passwords = 0;
    for password in 382345..843167 {
        if valid_password(password.to_string()) {
            valid_passwords += 1;
        }
    }
    println!("Valid passwords (part 2): {}", valid_passwords);
}

fn valid_password(password: String) -> bool {
    let mut adjacent_count = 0;
    let mut adjacent_pair = false;
    let mut first = true;
    let mut last = 0;
    for digit_char in password.chars() {
        let digit = digit_char.to_digit(10).expect("Couldn't parse digit");
        if !first {
            if digit < last {
                return false;
            }
            if last == digit {
                adjacent_count += 1;
            } else {
                if adjacent_count == 1 {
                    adjacent_pair = true;
                }
                adjacent_count = 0;
            }
        }
        last = digit;
        first = false;
    }
    return adjacent_pair || adjacent_count == 1;
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_countup() {
        assert_eq!(valid_password("125689".to_string()), false);
    }

    #[test]
    fn test_reduce() {
        assert_eq!(valid_password("125681".to_string()), false);
    }

    #[test]
    fn test_2dup() {
        assert_eq!(valid_password("125589".to_string()), true);
    }

    #[test]
    fn test_3dup() {
        assert_eq!(valid_password("123444".to_string()), false);
    }

    #[test]
    fn test_4dup() {
        assert_eq!(valid_password("555589".to_string()), false);
    }

    #[test]
    fn test_5dup() {
        assert_eq!(valid_password("555558".to_string()), false);
    }

    #[test]
    fn test_6dup() {
        assert_eq!(valid_password("999999".to_string()), false);
    }

    #[test]
    fn test_2dup_x3() {
        assert_eq!(valid_password("112233".to_string()), true);
    }

    #[test]
    fn test_2dup_end() {
        assert_eq!(valid_password("123455".to_string()), true);
    }

    #[test]
    fn test_4dup_2dup() {
        assert_eq!(valid_password("111122".to_string()), true);
    }
}
