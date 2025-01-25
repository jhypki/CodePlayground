import { generateDockerFiles } from "./generateDockerFiles";
import { generateHandlers } from "./generateHandlers";
import { generateSupportedLanguagesEnum } from "./generateSupportedLanguagesEnum";
import { generateLanguageHandlerFactory } from "./generateLanguageHandlerFactory";
import { generateDockerCompose } from "./generateDockerCompose";
import { generateReadme } from "./generateReadme";

generateDockerFiles();
generateHandlers();
generateSupportedLanguagesEnum();
generateLanguageHandlerFactory();
generateDockerCompose();
generateReadme();
