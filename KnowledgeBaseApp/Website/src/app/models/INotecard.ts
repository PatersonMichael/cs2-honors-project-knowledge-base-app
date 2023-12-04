import { IKeyword } from "./IKeyword";

export interface INoteCard {
    noteId: number | null | undefined,
    title: string | null | undefined,
    body: string,
    creationDate: Date | null | undefined,
    lastUpdateDate?: Date | null | undefined,
    userProfileId: number | null | undefined,
    keywords?: IKeyword[] | null | undefined,
}