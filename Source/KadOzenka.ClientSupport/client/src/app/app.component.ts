import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, Event } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';  
  currentUrl: string | null = null;

  constructor(private _router: Router, private activatedRoute: ActivatedRoute){
    _router.events
      .subscribe((event: Event) => {
        if (event instanceof NavigationEnd) {
          this.currentUrl = event.url;
        }});
  }
}
