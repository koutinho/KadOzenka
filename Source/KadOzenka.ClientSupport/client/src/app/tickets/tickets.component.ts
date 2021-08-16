import { Component, OnInit } from '@angular/core';
import { Ticket } from '../common/api/tickets/Ticket';
import { TicketApiService } from '../common/api/tickets/TicketService';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css']
})
export class TicketsComponent implements OnInit {

  public ticketService: TicketApiService;

  public tickets: Ticket[] = [];

  constructor(ticketService: TicketApiService) {
    this.ticketService = ticketService;
    
    this.ticketService.getTickets().subscribe(
      (res) => this.tickets = res);
  }

  ngOnInit(): void {
  }

}
