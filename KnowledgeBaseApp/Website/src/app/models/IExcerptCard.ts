import { ICitation } from "./ICitation";
import { IKeyword } from "./IKeyword";

export interface IExcerptCard {
    excerptCardId: number | null | undefined,
    title: string | null | undefined,
    excerpt: string | null | undefined,
    creationDate: Date  | null | undefined,
    lastUpdatedDate: Date | null | undefined,
    userProfileId: number | null | undefined,
    citation: ICitation | null | undefined,
    citationId: number | null | undefined,
    keywords?: IKeyword[] | null | undefined,
}