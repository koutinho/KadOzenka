export class Ticket {
    public ticketId: number;
    public kadNumber: string;
    public content: string;

    constructor(id: number, kadNumber: string, content: string) {
        this.ticketId = id;
        this.kadNumber = kadNumber;
        this.content = content;
    }
}