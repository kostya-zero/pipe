use serde::{Deserialize, Serialize};

#[derive(Serialize, Deserialize)]
pub struct Info {
    pub name: String,
    pub version: String,
}

#[derive(Serialize, Deserialize)]
pub struct Module {
    pub name: String,
    pub version: String,
}

#[derive(Serialize, Deserialize)]
pub struct Project {
    pub info: Info,
    pub modules: Vec<Module>,
}
