import path from "path";
import fs from "fs";
import languages from "../languages.json";

export const generateSupportedLanguagesEnum = (): void => {
  const enumsDir = path.join(__dirname, "../CodePlayground.Core/Enums");

  const fileContent = `using System.Runtime.Serialization;

namespace CodePlayground.Core.Enums;

public enum SupportedLanguages
{
${languages
  .map(
    (language) =>
      `    [EnumMember(Value = "${language.language}")] ${language.name},`
  )
  .join("\n")}
}`;

  fs.writeFileSync(path.join(enumsDir, "SupportedLanguages.cs"), fileContent);
};
