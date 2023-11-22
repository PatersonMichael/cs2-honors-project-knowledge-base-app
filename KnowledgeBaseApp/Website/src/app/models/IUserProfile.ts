export interface IUserProfile {
    userProfileId?: number | null | undefined,
    firstName: string | null | undefined,
    lastName: string | null | undefined,
    email: string | null | undefined,
    password: string | null | undefined,
    creationDate: Date | null | undefined,
    birthDate: Date | string | null | undefined,
    nametag: string | null | undefined,

}

export interface IUserLoginCredentials {
    email : string | null | undefined,
    password: string | null | undefined,
}