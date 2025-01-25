# CodePlayground

CodePlayground is a platform for running code snippets in various programming languages inside Docker containers.

## How to Run
1. **Install Dependencies**: Ensure you have [Docker](https://www.docker.com/) installed and running.
2. **Start the Application**:
   ```
   docker compose up --build
   ```
   This command builds all required Docker images (including `codeplayground` and each language's image) and then starts the containers.
3. **Check the Containers** (Optional): In another terminal, you can run:
   ```
   docker ps
   ```
   to see if the `codeplayground` and language images are up and running.
4. **Use the API**: Send a POST request to the endpoint (e.g. `/api/execute`) with a JSON body like:
   ```json
   {
     "language": "csharp",
     "code": "Console.WriteLine(\"Hello World!\");"
   }
   ```
   The service will return stdout, stderr, and an exit code.

## Scripts
The `./scripts` directory contains scripts for generating required resources based on `languages.json`.
If you want to add support for a custom language, you need to update `languages.json` and run these scripts.

### **bun**
```
cd ./scripts
bun ./scripts/index
```
Uses [bun](https://bun.sh/) to run the scripts.

### **node**
```
cd scripts
npm start
```
Runs the compiled scripts using Node.js.

## How to Add a Custom Language
1. Open the `languages.json` file.
2. Add a new object with the required fields:
   ```json
   {
     "language": "myLanguageId",
     "name": "MyLanguage",
     "dockerImage": "some-docker-image",
     "runCommand": "mkdir -p /code && echo \"{base64Code}\" | base64 -d > /code/temp.myLang && myLanguageCompiler /code/temp.myLang",
     "additionalDockerCommands": [],
     "fileExtension": "myLang",
     "template": "{code}"
   }
   ```
3. Run the code generation scripts (see above), which will create necessary Dockerfiles and handlers.
4. Finally, run `docker compose up --build` again to build the new language image.

## Supported Languages
Below is the list of supported languages, taken from `languages.json`:

- **JavaScript** (image: `node:14`, file extension: `.js`)
- **CSharp** (image: `mcr.microsoft.com/dotnet/sdk:5.0`, file extension: `.cs`)
- **Python** (image: `python:3.9`, file extension: `.py`)
- **TypeScript** (image: `node:18`, file extension: `.ts`)
- **Java** (image: `openjdk:17`, file extension: `.java`)
- **Go** (image: `golang:1.20`, file extension: `.go`)
- **Ruby** (image: `ruby:3.2`, file extension: `.rb`)
- **Rust** (image: `rust:1.66`, file extension: `.rs`)
- **Kotlin** (image: `zenika/kotlin`, file extension: `.kt`)
- **PHP** (image: `php:8.2-cli`, file extension: `.php`)
- **Swift** (image: `swift:5.7`, file extension: `.swift`)
- **Bash** (image: `ubuntu:latest`, file extension: `.sh`)
- **Perl** (image: `perl:5.36`, file extension: `.pl`)
- **Elixir** (image: `elixir:1.14`, file extension: `.exs`)
- **Julia** (image: `julia:1.8`, file extension: `.jl`)
- **Haskell** (image: `haskell:9.0`, file extension: `.hs`)
- **Lua** (image: `lua:5.4`, file extension: `.lua`)
- **Groovy** (image: `groovy:3.0`, file extension: `.groovy`)
- **C** (image: `gcc:11`, file extension: `.c`)


