use crate::args::get_args;

mod actions;
mod args;
mod config;
mod term;

fn main() {
    let args = get_args().get_matches();
    match args.subcommand() {
        Some(("init", _sub)) => {}
        _ => {
            println!("Unknown command.")
        }
    }
}
