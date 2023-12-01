// gets around typescript issue of not knowing which kind of error is being thrown
// if error isn't an actual error type, stringifies the error and returns that string


export function getErrorMessage(error: unknown) {
    if (error instanceof Error) {
        return error.message;
    }
    return String(error);
}