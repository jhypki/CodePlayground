import fs from "fs";
import path from "path";
// Adjust this import path if your languages.json is located elsewhere:
import languages from "../languages.json";

export const generateReadme = (): void => {
  // 1) Common README content:
  let content = `# CodePlayground

CodePlayground is a platform for running code snippets in various programming languages inside Docker containers.

## How to Run
1. **Install Dependencies**: Ensure you have [Docker](https://www.docker.com/) installed and running.
2. **Start the Application**:
   \`\`\`
   docker compose up --build
   \`\`\`
   This command builds all required Docker images (including \`codeplayground\` and each language's image) and then starts the containers.
3. **Check the Containers** (Optional): In another terminal, you can run:
   \`\`\`
   docker ps
   \`\`\`
   to see if the \`codeplayground\` and language images are up and running.
4. **Use the API**: Send a POST request to the endpoint (e.g. \`/api/execute\`) with a JSON body like:
   \`\`\`json
   {
     "language": "csharp",
     "code": "Console.WriteLine(\\"Hello World!\\");"
   }
   \`\`\`
   The service will return stdout, stderr, and an exit code.

## Scripts
The \`./scripts\` directory contains scripts for generating required resources based on \`languages.json\`.
If you want to add support for a custom language, you need to update \`languages.json\` and run these scripts.

### **bun**
\`\`\`
cd ./scripts
bun ./scripts/index
\`\`\`
Uses [bun](https://bun.sh/) to run the scripts.

### **node**
\`\`\`
cd scripts
npm start
\`\`\`
Runs the compiled scripts using Node.js.

## How to Add a Custom Language
1. Open the \`languages.json\` file.
2. Add a new object with the required fields:
   \`\`\`json
   {
     "language": "myLanguageId",
     "name": "MyLanguage",
     "dockerImage": "some-docker-image",
     "runCommand": "mkdir -p /code && echo \\\"{base64Code}\\\" | base64 -d > /code/temp.myLang && myLanguageCompiler /code/temp.myLang",
     "additionalDockerCommands": [],
     "fileExtension": "myLang",
     "template": "{code}"
   }
   \`\`\`
3. Run the code generation scripts (see above), which will create necessary Dockerfiles and handlers.
4. Finally, run \`docker compose up --build\` again to build the new language image.

## Supported Languages
Below is the list of supported languages, taken from \`languages.json\`:

`;

  // 2) Append each language's info from JSON
  for (const lang of languages) {
    content += `- **${lang.name}** (image: \`${lang.dockerImage}\`, language: \`${lang.language}\`)\n`;
  }

  // 3) Optionally add more content, e.g. a license or contact info:
  content += `

`;

  // 4) Write to README.md in the project root (adjust path as necessary)
  const readmePath = path.join(process.cwd(), "../README.md");
  fs.writeFileSync(readmePath, content, "utf-8");
  console.log(`✅ README.md generated at: ${readmePath}`);
};
