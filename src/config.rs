use std::fs;

use serde::{Deserialize, Serialize};

#[derive(Deserialize, Serialize)]
pub struct Info {
    pub name: String,
    pub version: String,
}

#[derive(Deserialize, Serialize)]
pub struct Depends {
    pub modules: Vec<String>,
}

#[derive(Deserialize, Serialize)]
pub struct Project {
    pub info: Info,
    pub depends: Depends,
}

impl Default for Project {
    fn default() -> Self {
        Project {
            info: Info {
                name: String::from("PipeProject"),
                version: String::from("0.1.0"),
            },
            depends: Depends { modules: vec![] },
        }
    }
}

pub struct Manager;
impl Manager {
    pub fn load_project() -> Project {
        let content =
            fs::read_to_string("pipe.yml").expect("Failed to read file or file not found.");
        let config: Project =
            serde_yaml::from_str(&content).expect("Failed to fetch config. Maybe syntax issue.");
        config
    }
}
