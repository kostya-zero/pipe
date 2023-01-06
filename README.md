# Pipe
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/6bf12d0bf98345a7baba35c0804a44ef)](https://www.codacy.com/gl/kostya-zero/pipe/dashboard?utm_source=gitlab.com&amp;utm_medium=referral&amp;utm_content=kostya-zero/pipe&amp;utm_campaign=Badge_Grade)

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
You can install Pipe for the x64 and ARM64 platforms.
Let's install it step-by-step:
- **1.** Go to releases section and select your preferred version.
- **2.** Download a binary file that matches your architecture and system (`pipe-<system>-<version>-<arch>.zip`, example: `pipe-linux-1.0-arm64.zip`).
- **3.** Place it where you want.
- **4.** Make sure that .NET Runtime are installed on your system.
- **5.** Finish.

## Usage
To start creating your application, generate a **Recipe** for Pipe.
The wizard will ask you for the name of project and the name of main executable.
The second one must match the real name of the main executable, otherwise Nuitka will not build it correctly and give an error.
```shell
pipe proj init
```
Now you have a file named `recipe.pipe` that contains information about your project.
If you want to get explanation of each property in this configuration file, visit [CONFIGURATION.md](CONFIGURATION.md).

To build an application, run the command below. 
Nuitka will show you if there are errors in your project.
```shell
pipe build
```
To get a list of available command, run `pipe` with `help` argument.
To get help for specific command, run `pipe` with command and `help` as positional argument.
