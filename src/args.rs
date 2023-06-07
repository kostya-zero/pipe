use clap::{value_parser, Arg, Command};

pub fn get_args() -> Command {
    Command::new("pipe")
        .about(env!("CARGO_PKG_DESCRIPTION"))
        .version(env!("CARGO_PKG_VERSION"))
        .arg_required_else_help(true)
        .subcommand_required(true)
        .subcommands([
            Command::new("init").about("Initialize Pipe config."),
            Command::new("build").about("Build application."),
            Command::new("add")
                .about("Add new module to project.")
                .arg(Arg::new("package")
                        .help("Package to add.")
                        .value_parser(value_parser!(String))
                    )

        ])
}
