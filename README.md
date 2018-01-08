# Codecov F# Example

| [https://codecov.io](https://codecov.io/) | [@codecov](https://twitter.com/codecov) | [hello@codecov.io](mailto:hello@codecov.io) |
| ----------------------- | ------------- | --------------------- |

[![Build status](https://ci.appveyor.com/api/projects/status/te04aqtjuv4anakr?svg=true)](https://ci.appveyor.com/project/MNie/codecov-fsharp)
[![codecov](https://codecov.io/gh/MNie/codecov-fsharp/branch/master/graph/badge.svg)](https://codecov.io/gh/MNie/codecov-fsharp)

## Solution

Start by restoring the nuget packages and building the solution.

## Generate the Coverage File

Coverage is generated using [OpenCover](https://github.com/OpenCover/opencover). You can obtain it from [NuGet](https://www.nuget.org/packages/opencover) or [Chocolatey](https://chocolatey.org/packages/opencover.portable). If we run the following command in PowerShell to install OpenCover via Chocolatey, 

```powershell
choco install opencover.portable
```

the OpenCover commandline will become available.

Generation of coverage report is slighly different depending on the .NET platform of your test projects.

### .NET Framework project

#### xUnit

First install the xUnit console runner via [Nuget](https://www.nuget.org/packages/xunit.runner.console/2.3.0-beta1-build3642) or [Chocolatey](https://chocolatey.org/packages/XUnit). If we run the following in PowerShell to install xUnit via Chocolatey

```powershell
choco install xunit
```

and execute the following in your solution's root,

```powershell
OpenCover.Console.exe -register:user -target:"xunit.console.x86.exe" -targetargs:".\test\CodecovExample.Tests\bin\Debug\CodecovExample.Tests.dll -noshadow" -filter:"+[CodecovExample.TargetProject*]* -[CodecovExample.Tests*]*" -output:".\CodecovExample-FSharp.xml"
```

Then a coverage report will be generated.

### .NET Core project

[Issue](https://github.com/OpenCover/opencover/issues/787)

## Uploading Report

Many options exit for uploading reports to Codecov. Three commonly used uploaders for .NET are

1. [Codecov-exe](https://github.com/codecov/codecov-exe) (C# source code)
2. [Bash](https://github.com/codecov/codecov-bash)
3. [Python](https://github.com/codecov/codecov-python)

For OS X and Linux builds, the recommended uploader is bash. For windows builds, all three uploaders work, but Codecov-exe does not require any dependencies. For example, the bash uploader and python uploader would require bash or python to be installed. This may or may not be an option.

### Codecov-exe

First install Codecov-exe via [Nuget](https://www.nuget.org/packages/Codecov/) or [Chocolatey](https://chocolatey.org/packages/codecov). If we run the following in PowerShell to install it via Chocolatey

```powershell
choco install codecov
```

and then run the following in PowerShell

```
.\codecov -f "CodecovExample-FSharp.xml" -t <your upload token>
```

the report will be uploaded.

### Bash

In bash run the following to upload the report

```bash
curl -s https://codecov.io/bash > codecov
chmod +x codecov
./codecov -f "CodecovExample-FSharp.xml" -t <your upload token>
```

### Python
 
First installed python (if you don't have it already). A simple way to install python is [Chocolatey](https://chocolatey.org/packages/python)

```powershell
choco install python
```

Next run the following in PowerShell

```
pip install codecov
.\codecov -f "CodecovExample-FSharp.xml" -t <your upload token>
```

### Continous Integration

The previous examples assumed local development. More commonly, you'll use a CI service like [AppVeyor](https://www.appveyor.com/) or [TeamCity](https://www.jetbrains.com/teamcity/). For TeamCity builds please see the [documentation](https://github.com/codecov/codecov-exe#teamcity). For AppVeyor builds using xUnit, your yaml file would look something like

#### Codecov-exe using Chocolatey

```yaml
image: Visual Studio 2017

before_build:
- nuget restore
- choco install opencover.portable
- choco install codecov

build:
  project: CodecovExample.sln
  verbosity: minimal

test_script:
- OpenCover.Console.exe -register:user -target:"%xunit20%\xunit.console.x86.exe" -targetargs:".\test\CodecovExample.Tests\bin\Debug\CodecovExample.Tests.dll -noshadow" -filter:"+[CodecovExample.TargetProject*]* -[CodecovExample.Tests*]*" -output:".\CodecovExample-FSharp.xml"
- codecov -f "CodecovExample-FSharp.xml"
```

#### Codecov-exe using NuGet

Using this method you can cache your packages.config file.

```yaml
image: Visual Studio 2017

before_build:
- nuget restore

build:
  project: CodecovExample.sln
  verbosity: minimal

test_script:
- .\packages\<ADD PATH>\OpenCover.Console.exe -register:user -target:"%xunit20%\xunit.console.x86.exe" -targetargs:".\test\CodecovExample.Tests\bin\Debug\CodecovExample.Tests.dll -noshadow" -filter:"+[CodecovExample.TargetProject*]* -[CodecovExample.Tests*]*" -output:".\CodecovExample-FSharp.xml"
- .\packages\<ADD PATH>\codecov.exe -f "CodecovExample-FSharp.xml"
```

#### Python

```yaml
image: Visual Studio 2017

before_build:
- nuget restore
- choco install opencover.portable

build:
  project: CodecovExample.sln
  verbosity: minimal

test_script:
- OpenCover.Console.exe -register:user -target:"%xunit20%\xunit.console.x86.exe" -targetargs:".\test\CodecovExample.Tests\bin\Debug\CodecovExample.Tests.dll -noshadow" -filter:"+[CodecovExample.TargetProject*]* -[CodecovExample.Tests*]*" -output:".\CodecovExample-FSharp.xml"
- "SET PATH=C:\\Python34;C:\\Python34\\Scripts;%PATH%"
- pip install codecov
- codecov -f "CodecovExample-FSharp.xml"
```

## Fake

If you use [Fake](https://github.com/fsharp/FAKE) (F# Make) for your build automation there is a support for [OpenCover](https://github.com/fsharp/FAKE/tree/master/src/app/Fake.DotNet.Testing.OpenCover).

## Cake

If you use [Cake](http://cakebuild.net/) (C# Make) for your build automation, there is a [Cake.Codecov](http://cakebuild.net/dsl/codecov/) addin available. Cake also has built in support for [OpenCover](http://cakebuild.net/dsl/opencover/).
