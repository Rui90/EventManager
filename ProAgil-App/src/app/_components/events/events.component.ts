import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventService } from '../../_services/event.service';
import { Event } from '../../_models/Event';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  filteredEvents: Event[] = [];
  events: Event[] = [];
  imageWidth = 50;
  imageMargin = 2;
  showImage = false;
  modalRef: BsModalRef;
  // tslint:disable-next-line: variable-name
  _eventFilter: string;

  constructor(
    private eventService: EventService,
    private modalService: BsModalService) { }

  get eventFilter() {
    return this._eventFilter;
  }

  set eventFilter(value: string) {
    this._eventFilter = value;
    this.filteredEvents = this.eventFilter ? this.filterEvents(this.eventFilter) : this.events;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit() {
    this.getEvents();
  }

  filterEvents(filter: string): Event[] {
    return this.events.filter(event => event.theme.toLocaleLowerCase().indexOf(filter.toLocaleLowerCase()) !== -1);
  }

  displayImage() {
    this.showImage = !this.showImage;
  }

  getEvents() {
    this.eventService.getEvent().subscribe(
      (response) => {
        this.events = response;
        this.filteredEvents = this.events;
      },
      error => { console.log(error); }
    );
  }

}
