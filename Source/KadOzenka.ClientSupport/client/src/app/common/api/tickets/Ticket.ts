export class Ticket {
    public id: number;
    public kadNumber: string;
    public content: string;

    constructor(id: number, kadNumber: string, content: string) {
        this.id = id;
        this.kadNumber = kadNumber;
        this.content = content;
    }
}