"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.generateDockerFiles = void 0;
const path_1 = __importDefault(require("path"));
const fs_1 = __importDefault(require("fs"));
const languages_json_1 = __importDefault(require("../languages.json"));
const generateDockerFiles = () => {
    const dockerRootDir = path_1.default.join(process.cwd(), "../docker");
    for (const language of languages_json_1.default) {
        console.log(`Generating Dockerfile for ${language.name}`);
        const languageDir = path_1.default.join(dockerRootDir, language.language);
        fs_1.default.mkdirSync(languageDir, { recursive: true });
        fs_1.default.writeFileSync(path_1.default.join(languageDir, "Dockerfile"), `FROM ${language.dockerImage}\nWORKDIR /code\n${language.additionalDockerCommands
            .map((command) => `${command}\n`)
            .join("")}`);
    }
};
exports.generateDockerFiles = generateDockerFiles;
