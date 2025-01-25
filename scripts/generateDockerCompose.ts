import languages from "../languages.json";
import fs from "fs";
import path from "path";

export const generateDockerCompose = (): void => {
  const codeplaygroundService = `  codeplayground:
    image: codeplayground
    build:
      context: .
      dockerfile: CodePlayground.API/Dockerfile
    ports:
      - "8080:8080"
    environment:
      DOTNET_ENVIRONMENT: Development
      ASPNETCORE_URLS: "http://+:8080"
    volumes:
      ${languages
        .map((lang) => `- ${lang.language}-project:/code/${lang.language}`)
        .join("\n      ")}
      - /var/run/docker.sock:/var/run/docker.sock
`;

  let languageServices = "";
  let volumes = "";

  for (const language of languages) {
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

  const outputPath = path.join(__dirname, "../docker-compose.yml");
  fs.writeFileSync(outputPath, finalCompose, "utf8");
  console.log(`✅  Generated docker-compose file at: ${outputPath}`);
};
