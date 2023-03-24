<div class="badges" align="center">
    <a href="https://gitlab.com/kostya-zero/pipe/-/blob/main/LICENSE">
        <img src="https://img.shields.io/badge/License-MIT-E18075?style=for-the-badge&logo=bookstack&logoColor=white&colorA=202020&colorB=E18075">
    </a>
    <a href="https://gitlab.com/kostya-zero/pipe/-/releases">
        <img src="https://img.shields.io/gitlab/v/release/kostya-zero/Pipe?style=for-the-badge&logo=gitlab&logoColor=white&colorA=202020&colorB=E8B66B">
    </a>
    <a href="https://github.com/kostya-zero/pipe/actions/workflows/dotnet.yml">
        <img src="https://img.shields.io/github/actions/workflow/status/kostya-zero/pipe/dotnet.yml?style=for-the-badge&logo=github&logoColor=white&colorA=202020&colorB=F3F3F3">
    </a>
    <a href="https://app.codacy.com/gl/kostya-zero/pipe/dashboard?utm_source=gl&utm_medium=referral&utm_content=&utm_campaign=Badge_grade">
        <img src="https://img.shields.io/codacy/grade/6bf12d0bf98345a7baba35c0804a44ef?style=for-the-badge&logo=codacy&logoColor=white&colorA=202020&colorB=776BE8">
    </a>
</div>

---

![Pipe Header Image](.github/images/header.png)

Pipe is a build system for projects written in Python. They use Nuitka, which uses C compilers to compile applications and makes them fast and independent of Python runtime. Currently Pipe supported only on Linux. But, what Pipe really allows you to enchance?

- Allows you to simplify the assembly of applications and modules.
- The project configuration file has a JSON structure for easy editing.
- Easy package management in the project.
- Improves the performance of applications and modules thanks to Nuitka.

## Navigation

- [Pipe](#pipe)
  - [Navigation](#navigation)
  - [Requirements](#requirements)
  - [Installation](#installation)
  - [Usage](#usage)
  - [Configuration](#configuration)

## Requirements

- Python version 3.5 or higher.
- C compiler with support of C11 or C++03 (`gcc`, `clang`).
- Nuitka installed with `pip` (`pip install nuitka`).
- .NET Runtime 7.x.

## Installation

1. Download a binary build that matches your architecture from releases on [Official GitLab Repository](https://gitlab.com/kostya-zero/pipe/-/releases).
    > The reason for moving releases was GitHub. When committing from our Gitea instance, all releases on GitHub disappeared, or rather they became drafts. There is no such thing like this on GitLab, and we decided to move all releases there.

2. Extract binary from downloaded archive.
3. Place it in `/usr/bin` if you installing it on Linux.
4. If it returns `Permission denied` on startup, set to executable file permissions to execute.

   ```shell
    sudo chmod +x /usr/bin/pipe
   ```
  
5. Pipe ready to use.

## Usage

You can use `help` argument to get help about available argument.

```bash
pipe help
```

## Configuration

We need to generate recipe file to make pipe able to build project.

```bash
pipe proj init
```

After dialog pipe will generate `recipe.pipe` file with build settings.

```json
{
  "Project_Name": "pipe_project",
  "Project_MainExecutable": "main.py",
  "Project_Version": "1.0.0",
  "Project_Description": "An App built with Pipe!",
  "Project_Type": "app",
  "Git_CheckoutBranch": "",
  "Nuitka_BackendCompiler": "gcc",
  "Nuitka_LTO": 0,
  "Nuitka_Jobs": 1,
  "Pipe_RunBeforeBuild": [],
  "Pipe_RequiredTools": [],
  "Options_OneFile": false,
  "Options_Standalone": false,
  "Options_FollowImports": true,
  "Options_NoPyiFiles": true,
  "Options_ShowOnlyError": false,
  "Depends_Packages": [],
  "Depends_UseRequirements": false,
  "Depends_IncludeDirs": [],
  "Depends_IgnorePackages": [
    "email",
    "http"
  ]
}
```

> **NOTE**: `email` and `http` packages automatically adding when initializing recipe. Its not configurable at this moment.
