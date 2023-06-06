use crate::{term::Term, config::{Project, Manager}};

pub struct Actions;
impl Actions {
    pub fn init() {
        let name: String = Term::ask("How will be named your project?", "PipeProject".to_string());
        let version: String = Term::ask("Which version of your project? It will be used for executable metadata.", "0.1.0".to_string());
        
        Term::msg("Generating project configuration...");
        let mut base_config: Project = Project { ..Default::default() };
        base_config.info.name = name;
        base_config.info.version = version;
        Manager::write(base_config);
        Term::msg("Project file has been created! It is called \"pipe.yml\".");
    }
}
