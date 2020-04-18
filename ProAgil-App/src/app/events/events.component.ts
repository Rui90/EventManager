import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  _eventFilter: string;
  get eventFilter() {
    return this._eventFilter;
  }

  set eventFilter(value: string) {
    this._eventFilter = value;
    this.filteredEvents = this.eventFilter ? this.filterEvents(this.eventFilter) : this.events;
  }

  filteredEvents: any = [];
  events: any = [];
  imageWidth = 50;
  imageMargin = 2;
  showImage = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEvents();
  }

  filterEvents(filter: string): any {
    return this.events.filter(event => event.theme.toLocaleLowerCase().indexOf(filter.toLocaleLowerCase()) !== -1);
  }

  displayImage() {
    this.showImage = !this.showImage;
  }

  getEvents() {
    this.http.get('http://localhost:5000/values').subscribe(
      response => { this.events = response; },
      error => { console.log(error); }
    );
  }

}
