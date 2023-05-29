use clap::{value_parser, Arg, Command};

pub fn get_args() -> Command {
    Command::new("pipe")
        .about(env!("CARGO_PKG_DESCRIPTION"))
        .version(env!("CARGO_PKG_VERSION"))
        .arg_required_else_help(true)
        .subcommand_required(true)
        .subcommands([
            Command::new("init")
                .about("Initialize Pipe workspace."),
            Command::new("add")
                .about("Add package to environment.")
                .arg(
                    Arg::new("packages")
                        .help("Packages to install.")
                        .value_parser(value_parser!(String))
                        .value_delimiter(' ')
                )
        ])
}
