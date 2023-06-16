use std::process::{Command, Stdio, exit};

pub struct Pip;
impl Pip {
    pub fn install(package: &str) -> bool {
        let mut cmd = Command::new("pip");
        cmd.args(vec!["install", package]);
        cmd.stdin(Stdio::inherit());
        cmd.stdout(Stdio::inherit());
        cmd.stderr(Stdio::inherit());
        let result = cmd.output().unwrap();
        if !result.status.success() {
            return false;
        }
        true
    }

    pub fn is_package_installed(package: &str) -> bool {
        let mut cmd = Command::new("pip");
        cmd.args(vec!["show", package]);
        let result = cmd.output().unwrap();
        if !result.status.success() {
            return false;
        }
        true
    }
}
