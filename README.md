# Pipe
Pipe is a build system that allows you to easily build your Python applications. 
It uses Nuitka as backend and making your compiled applications a lot faster. 
Configuration file is a JSON format file and has a lot of options to use.

## Requirements
Important: 
- `python` - to make your applications work.
- `nutika` - backend for pipe to build applications. Must be installed with `pip`.

Optional:
- `ccache` - required to allow Nuitka use ccache (`pipe_useccache`).

## Installation
You can install Pipe for the x64 and ARM platforms. 
Also, it has RTR(`Ready to run`) versions and they don't need a runtime.
Let's install it step-by-step:
- **1.** Go to releases section and select your preferred version.
- **2.** Download a binary file that matches your architecture and prefer execute method(`rtr/runtime`) (`pipe-<system>-<version>-<executemethod>-<arch>.zip`, example: `pipe-linux-1.0-runtime-arm.zip`).
- **3.** Place it where you want.
- **4.** If you have selected `runtime` binary, you need to install .NET runtime to run it. If you selected `rtr`, it will run without runtime.
- **5.** Finish.

## Usage
To start creating your application, generate a configuration file for Pipe.
The wizard will ask you for the name of project and the name of main executable.
The second one must match the real name of the main executable, otherwise Nuitka will not build it correctly and give an error.
```shell
pipe build genconfig
```
Now you have a file named `project.pipe` that contains information about your project.
If you want to get explanation of each property in this configuration file, visit [CONFIGURATION.md](CONFIGURATION.md).

To build an application, run the command below. 
Nuitka will show you if there are errors in your project.
```shell
pipe build start
```
To get a list of available command, run `pipe` with `help` argument.
To get help for specific command, run `pipe` with command and `help` as positional argument.
