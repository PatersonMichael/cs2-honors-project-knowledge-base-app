import { IKeyword } from "./IKeyword";

export interface INoteCard {
    noteId: number | null | undefined,
    title: string | null | undefined,
    body: string | null | undefined,
    creationDate: Date | null | undefined,
    lastUpdatedDate?: Date | null | undefined,
    userProfileId: number | null | undefined,
    keywords?: IKeyword[] | null | undefined,
}