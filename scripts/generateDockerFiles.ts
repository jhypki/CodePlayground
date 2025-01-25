import path from "path";
import fs from "fs";
import languages from "../languages.json";

export const generateDockerFiles = (): void => {
  const dockerRootDir = path.join(process.cwd(), "../docker");

  for (const language of languages) {
    console.log(`Generating Dockerfile for ${language.name}`);

    const languageDir = path.join(dockerRootDir, language.language);
    fs.mkdirSync(languageDir, { recursive: true });

    fs.writeFileSync(
      path.join(languageDir, "Dockerfile"),
      `FROM ${
        language.dockerImage
      }\nWORKDIR /code\n${language.additionalDockerCommands
        .map((command) => `${command}\n`)
        .join("")}`
    );
  }
};
