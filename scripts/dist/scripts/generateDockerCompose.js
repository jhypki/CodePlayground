"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.generateDockerCompose = void 0;
const languages_json_1 = __importDefault(require("../languages.json"));
const fs_1 = __importDefault(require("fs"));
const path_1 = __importDefault(require("path"));
const generateDockerCompose = () => {
    const codeplaygroundService = `  codeplayground:
    image: codeplayground
    build:
      context: .
      dockerfile: CodePlayground/CodePlayground.API/Dockerfile
    ports:
      - "8080:8080"
    environment:
      DOTNET_ENVIRONMENT: Development
      ASPNETCORE_URLS: "http://+:8080"
    volumes:
      ${languages_json_1.default
        .map((lang) => `- ${lang.language}-project:/code/${lang.language}`)
        .join("\n      ")}
      - /var/run/docker.sock:/var/run/docker.sock
`;
    let languageServices = "";
    let volumes = "";
    for (const language of languages_json_1.default) {
        const langName = language.language;
        const imageName = `code-playground/${langName}`;
        languageServices += `  ${langName}:
    build:
      context: ./docker/${langName}
    image: ${imageName}

`;
        volumes += `  ${langName}-project:\n`;
    }
    const finalCompose = `version: '3.9'

services:
${codeplaygroundService}${languageServices}volumes:
${volumes}
`;
    const outputPath = path_1.default.join(process.cwd(), "../docker-compose.yml");
    fs_1.default.writeFileSync(outputPath, finalCompose, "utf8");
    console.log(`✅  Generated docker-compose file at: ${outputPath}`);
};
exports.generateDockerCompose = generateDockerCompose;
