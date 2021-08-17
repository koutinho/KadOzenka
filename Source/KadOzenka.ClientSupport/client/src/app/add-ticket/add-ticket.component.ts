import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

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

  constructor(private formBuilder: FormBuilder) { }

  public get kadNumber(): AbstractControl {
    return this.ticketForm.get('kadNumber') as AbstractControl;
  }

  public get content(): AbstractControl {
    return this.ticketForm.get('content') as AbstractControl;
  }

  ngOnInit(): void {
  }

}
