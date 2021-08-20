import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Ticket } from '../../api/tickets/Ticket';
import { TicketApiService } from '../../api/tickets/TicketService';

@Component({
  selector: 'app-add-ticket',
  templateUrl: './add-ticket.component.html',
  styleUrls: ['./add-ticket.component.css']
})
export class AddTicketComponent implements OnInit {

  public ticketForm: FormGroup = this.formBuilder.group({
    kadNumber: ['', Validators.required],
    content: ['', Validators.required]
  })

  constructor(private formBuilder: FormBuilder, private ticketService: TicketApiService) { }

  public get kadNumber(): AbstractControl {
    return this.ticketForm.get('kadNumber') as AbstractControl;
  }

  public get content(): AbstractControl {
    return this.ticketForm.get('content') as AbstractControl;
  }

  ngOnInit(): void {
  }

  public addTicket() {
    if (this.ticketForm.valid) {
      this.ticketService.addTicket(this.ticketForm.value).subscribe(ticket => {
        this.reset();
      });
    }
  }

  reset() {
    this.ticketForm.markAsPristine()
    this.ticketForm.reset();
  }

  resetKadNumber() {
    this.kadNumber.reset();
  }

  resetContent() {
    this.content.reset();
  }
}
