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

- **JavaScript** (image: `node:14`, language: `javascript`)
- **CSharp** (image: `mcr.microsoft.com/dotnet/sdk:5.0`, language: `csharp`)
- **Python** (image: `python:3.9`, language: `python`)
- **TypeScript** (image: `node:18`, language: `typescript`)
- **Java** (image: `openjdk:17`, language: `java`)
- **Go** (image: `golang:1.20`, language: `go`)
- **Ruby** (image: `ruby:3.2`, language: `ruby`)
- **Rust** (image: `rust:1.66`, language: `rust`)
- **Kotlin** (image: `zenika/kotlin`, language: `kotlin`)
- **PHP** (image: `php:8.2-cli`, language: `php`)
- **Swift** (image: `swift:5.7`, language: `swift`)
- **Bash** (image: `ubuntu:latest`, language: `bash`)
- **Perl** (image: `perl:5.36`, language: `perl`)
- **Elixir** (image: `elixir:1.14`, language: `elixir`)
- **Julia** (image: `julia:1.8`, language: `julia`)
- **Haskell** (image: `haskell:9.0`, language: `haskell`)
- **Lua** (image: `lua:5.4`, language: `lua`)
- **Groovy** (image: `groovy:3.0`, language: `groovy`)
- **C** (image: `gcc:11`, language: `c`)


