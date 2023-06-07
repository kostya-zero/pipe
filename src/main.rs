use std::process::exit;

use actions::Actions;
use term::Term;

use crate::args::get_args;

mod actions;
mod args;
mod config;
mod term;
mod utils;

fn main() {
    let args = get_args().get_matches();
    match args.subcommand() {
        Some(("init", _sub)) => {
            Actions::init();
        }
        Some(("add", _sub)) => {
            let package: String = _sub.get_one::<String>("package").expect("Failed to get argument.").to_string();

            if package.is_empty() {
                Term::msg("Cannot add an \"empty\" package.");
                exit(1);
            }

            Actions::add(&package);
        }
        _ => {
            println!("Unknown command.")
        }
    }
}
