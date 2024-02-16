use std::io::{self, Write};

pub struct Term;
impl Term {
    pub fn msg(msg: &str) {
        println!("{}", msg);
    }

    pub fn ask(msg: &str, default: String) -> String {
        print!("{} ({}):", msg, default);
        io::stdout()
            .flush()
            .expect("Failed to push message to stdout.");
        let mut input: String = String::new();
        io::stdin().read_line(&mut input);
        if input.is_empty() {
            return default;
        }
        return input;
    }

    pub fn fatal(msg: &str) {
        println!("fatal : {}", msg);
    }

    pub fn warn(msg: &str) {
        println!(" warn : {}", msg);
    }

    pub fn ok(msg: &str) {
        println!("   ok : {}", msg);
    }
}
