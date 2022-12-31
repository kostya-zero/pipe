# Pipe
Pipe is a build system that's allows you to easily build your Python applications.
It use Nuitka as backend and making your compiled applications working a lot faster.
Configuration file has a JSON structure and has a lot of options to use.

## Requirements

Important: 
- `python` - to make your applications work.
- `nutika` - backend for pipe to build applications. Must be installed with `pip`.

Optional:
- `ccache` - required to allow Nuitka use ccache (`pipe_useccache`).

## Installation
You can install Pipe for x64 and ARM platform. 
Also, it has RTR(`Ready to run`) releases and they dont need a runtime.
Let's install it step-by-step:
- **1.** Go to releases and select prefer version.
- **2.** Download binary that's match your architecture and prefer execute method(`rtr/runtime`) (`pipe-<system>-<version>-<executemethod>-<arch>.zip`, example: `pipe-linux-1.0-runtime-arm.zip`).
- **3.** Place it where you want.
- **4.** If you selected `runtime` binary, you need to install .NET runtime to run it. If you selected `rtr` it will work without runtime.
- **5.** Finish.

## Usage
To start building your application generate configuration file for pipe project.
Wizard will ask you for project name and main executable name.
The second one must match your real main executable name.
Or Nuitka will not build it correctly or throw error.
```shell
pipe build genconfig
```
Now you have file `project.pipe` which contains information about your project.
If you want to get explanation about every property in this config visit CONFIGURATION.md.

To build application run command below. 
Nuitka will show you if you project has errors.
```shell
pipe build start
```
To get list of available command run `pipe` with `help` argument. 
To get help for specific command run `pipe` with command and `help` as positional argument.

## 