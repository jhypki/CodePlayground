"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.generateLanguageHandlerFactory = void 0;
const path_1 = __importDefault(require("path"));
const fs_1 = __importDefault(require("fs"));
const languages_json_1 = __importDefault(require("../languages.json"));
const generateLanguageHandlerFactory = () => {
    const factoriesDir = path_1.default.join(process.cwd(), "../CodePlayground.Core/Factories");
    const fileContent = `using CodePlayground.Core.Enums;
using CodePlayground.Core.Interfaces;
using CodePlayground.Core.Languages;

namespace CodePlayground.Core.Factories;
public abstract class LanguageHandlerFactory
{
    public static ILanguageHandler GetHandler(SupportedLanguages language)
    {
        return language switch
        {
        ${languages_json_1.default
        .map((language) => `    SupportedLanguages.${language.name} => new ${language.name}Handler(),`)
        .join("\n")}
            _ => throw new NotImplementedException()
        };
    }
}`;
    fs_1.default.writeFileSync(path_1.default.join(factoriesDir, "LanguageHandlerFactory.cs"), fileContent);
};
exports.generateLanguageHandlerFactory = generateLanguageHandlerFactory;
