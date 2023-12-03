import { IKeyword } from "./IKeyword";

export interface INoteCard {
    noteId: number | null | undefined,
    title: string | null | undefined,
    body: string | null | undefined,
    creationDate: Date | null | undefined,
    lastUpdateDate: Date | null | undefined,
    userProfileId: string | null | undefined,
    keywords: IKeyword[] | null | undefined,
}