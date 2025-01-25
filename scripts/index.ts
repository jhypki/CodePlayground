import { generateDockerFiles } from "./generateDockerFiles";
import { generateHandlers } from "./generateHandlers";
import { generateSupportedLanguagesEnum } from "./generateSupportedLanguagesEnum";
import { generateLanguageHandlerFactory } from "./generateLanguageHandlerFactory";
import { generateDockerCompose } from "./generateDockerCompose";

generateDockerFiles();
generateHandlers();
generateSupportedLanguagesEnum();
generateLanguageHandlerFactory();
generateDockerCompose();
