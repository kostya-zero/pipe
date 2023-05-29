use crate::args::get_args;

mod args;
mod config;

fn main() {
    let args = get_args().get_matches(); 
}
