use std::process::exit;

use crate::{
    config::{Manager, Project},
    term::Term, utils::pip::Pip,
};

pub struct Actions;
impl Actions {
    pub fn init() {
        let name: String = Term::ask("How will be named your project?", "PipeProject".to_string());
        let version: String = Term::ask(
            "Which version of your project? It will be used for executable metadata.",
            "0.1.0".to_string(),
        );

        Term::msg("Generating project configuration...");
        let mut base_config: Project = Project {
            ..Default::default()
        };
        base_config.info.name = name;
        base_config.info.version = version;
        Manager::write(base_config);
        Term::msg("Project file has been created! It is called \"pipe.yml\".");
    }

    pub fn add(package: &str) {
        let mut config: Project = Manager::load();
        Term::msg(format!("Adding {} to the project...", package.to_string()).as_str());
        if config.depends.modules.iter().any(|i| i == package) {
            Term::msg("This package already added.");
            exit(0);
        }

        if Pip::is_package_installed(package) {
            Term::warn("Package already installed via pip. No need to install")
        } else {
            Term::msg("Installing package via pip.");
            if !Pip::install(package) {
                Term::fatal("Failed to install package. Pip exited with bad exit code.");
                exit(1);
            }
        }

        config.depends.modules.push(package.to_string());
        Manager::write(config);
        Term::msg("Package has been added!");
        exit(0);
    }
}
