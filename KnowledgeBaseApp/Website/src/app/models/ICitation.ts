import { ISourceMaterial } from "./ISourceMaterial";

export interface ICitation {
    citationId: number | null | undefined,
    format: string | null | undefined,
    excerptLocation: string | null | undefined,
    creationDate: Date | null | undefined,
    userProfileId: number | null | undefined,
    sourceMaterialId: number | null | undefined,
    sourceMaterial: ISourceMaterial,
}