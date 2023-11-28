
export class TimeUtilities {

    static addHours(date: Date, hours: number){
        const result = new Date(date);
        result.setHours(result.getHours() + hours);
        return result;
    }

}
