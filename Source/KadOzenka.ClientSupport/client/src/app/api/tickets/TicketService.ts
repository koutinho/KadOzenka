import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Http } from "../httpMethods/methods";
import { Ticket } from "./Ticket";

@Injectable()
export class TicketApiService {
    private http: Http;
    constructor(httpClient: HttpClient){
        this.http = new Http(httpClient);
    }

    getTickets(): Observable<Ticket[]> {        
        return this.http.get<Ticket[]>("Tickets");
    }

    addTicket(ticket: Ticket): Observable<Ticket> {
        return this.http.post<Ticket, Ticket>("tickets", ticket);
    }
}