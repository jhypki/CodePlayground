"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.generateSupportedLanguagesEnum = void 0;
const path_1 = __importDefault(require("path"));
const fs_1 = __importDefault(require("fs"));
const languages_json_1 = __importDefault(require("../languages.json"));
const generateSupportedLanguagesEnum = () => {
    const enumsDir = path_1.default.join(process.cwd(), "../CodePlayground/CodePlayground.Core/Enums");
    const fileContent = `using System.Runtime.Serialization;

namespace CodePlayground.Core.Enums;

public enum SupportedLanguages
{
${languages_json_1.default
        .map((language) => `    [EnumMember(Value = "${language.language}")] ${language.name},`)
        .join("\n")}
}`;
    fs_1.default.writeFileSync(path_1.default.join(enumsDir, "SupportedLanguages.cs"), fileContent);
};
exports.generateSupportedLanguagesEnum = generateSupportedLanguagesEnum;
