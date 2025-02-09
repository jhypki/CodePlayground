import languages from '../../../languages.json';
import {Language} from "@/types/Language";

export const getLanguages = (): Language[] => {
    return languages.map((language) => {
        return {
            name: language.name,
            language: language.language,
            template: language.template
        }
    })
}