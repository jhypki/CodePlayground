"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.generateHandlers = void 0;
const path_1 = __importDefault(require("path"));
const fs_1 = __importDefault(require("fs"));
const languages_json_1 = __importDefault(require("../languages.json"));
const generateHandlers = () => {
    const handlersDir = path_1.default.join(process.cwd(), "../CodePlayground/CodePlayground.Core/Languages");
    for (const language of languages_json_1.default) {
        fs_1.default.mkdirSync(handlersDir, { recursive: true });
        const fileContent = `using CodePlayground.Core.Enums;
using CodePlayground.Core.Helpers;
using CodePlayground.Core.Interfaces;
using System;

namespace CodePlayground.Core.Languages;
public class ${language.name}Handler : ILanguageHandler
{
    public string GetDockerImage()
    {
        return "code-playground/${language.language}";
    }

    public string GetExecutionCommand(string code)
    {
        var base64Code = CodeSanitizer.ToBase64(code);

        var runCommand = $"${language.runCommand}";

        return runCommand;
    }
}`;
        fs_1.default.writeFileSync(path_1.default.join(handlersDir, `${language.name}Handler.cs`), fileContent);
    }
};
exports.generateHandlers = generateHandlers;
