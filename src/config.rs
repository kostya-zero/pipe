use std::{fs, path::Path};

use serde::{Deserialize, Serialize};

#[derive(Deserialize, Serialize)]
pub struct Info {
    pub name: String,
    pub version: String,
    pub main_file: String
}

#[derive(Deserialize, Serialize)]
pub struct Depends {
    pub modules: Vec<String>,
}

#[derive(Deserialize, Serialize)]
pub struct Options {
    pub no_pyi_files: bool,
    pub follow_imports: bool,
}

#[derive(Deserialize, Serialize)]
pub struct Project {
    pub info: Info,
    pub options: Options,
    pub depends: Depends,
}

impl Default for Project {
    fn default() -> Self {
        Project {
            info: Info {
                name: String::from("PipeProject"),
                version: String::from("0.1.0"),
                main_file: String::from("main.py")
            },
            options: Options { no_pyi_files: true, follow_imports: true },
            depends: Depends { modules: vec![] },
        }
    }
}

pub struct Manager;
impl Manager {
    pub fn load() -> Project {
        let content =
            fs::read_to_string("pipe.yml").expect("Failed to read file or file not found.");
        let config: Project =
            serde_yaml::from_str(&content).expect("Failed to fetch config. Maybe syntax issue.");
        config
    }

    pub fn make_default() {
        let config: String = serde_yaml::to_string(&Project { ..Default::default() }).expect("Failed to convert config to string.");
        fs::write("pipe.yml", config).unwrap();
    }

    pub fn write(config: Project) {
        let content: String = serde_yaml::to_string(&config).expect("Failed to convert config to string");
        fs::write("pipe.yml", content).unwrap();
    }

    pub fn check() -> bool {
        if Path::new("pipe.yml").exists() {
            return true;
        }
        false
    }
}
