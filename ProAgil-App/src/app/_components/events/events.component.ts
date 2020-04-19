import { Component, OnInit } from '@angular/core';
import { EventService } from '../../_services/event.service';
import { Event } from '../../_models/Event';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsLocaleService, } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';

defineLocale('pt-br', ptBrLocale);
@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  filteredEvents: Event[] = [];
  events: Event[] = [];
  event: Event;
  imageWidth = 50;
  imageMargin = 2;
  showImage = false;
  modalRef: BsModalRef;
  // tslint:disable-next-line: variable-name
  _eventFilter: string;
  registerForm: FormGroup;
  editMode = false;
  deleteModal = false;
  dialogBoxBodyText: string;

  constructor(
    private eventService: EventService,
    private formbuilder: FormBuilder,
    private localService: BsLocaleService) {
      this.localService.use('pt-br');
     }

  get eventFilter() {
    return this._eventFilter;
  }

  set eventFilter(value: string) {
    this._eventFilter = value;
    this.filteredEvents = this.eventFilter ? this.filterEvents(this.eventFilter) : this.events;
  }

  editEvent(event: Event, template: any) {
    this.editMode = true;
    this.openModal(template);
    this.event = event;
    this.registerForm.patchValue(this.event);
  }

  createEvent(template: any) {
    this.editMode = false; 
    this.openModal(template);
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  ngOnInit() {
    this.validation();
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

  saveChanges(template: any) {
    if (this.registerForm.valid) {
      if (this.editMode) {
        this.event = Object.assign({id: this.event.id}, this.registerForm.value);
        this.eventService.updateEvent(this.event).subscribe(
          (newEvent: Event) => {
            template.hide();
            this.getEvents();
          },
          (error) => {
              console.log(error);
          });
      } else {
        this.event = Object.assign({}, this.registerForm.value);
        this.eventService.createEvent(this.event).subscribe(
          (newEvent: Event) => {
            template.hide();
            this.getEvents();
          },
          (error) => {
              console.log(error);
          });
      }
    }
  }

  deleteEvent(event: Event, template: any) {
    this.openModal(template);
    this.event = event;
    this.dialogBoxBodyText = `Are you sure you want to delete the event: ${event.theme}?`;
  }

  confirmDelete(template: any) {
    this.eventService.deleteEvent(this.event.id).subscribe(
      () => {
        template.hide();
        this.getEvents();
      },
      (errors) => {
        console.log(errors);
      }
    );
  }

  validation() {
    this.registerForm = this.formbuilder.group({
      theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      location: ['', Validators.required],
      date: ['', Validators.required],
      capacity: ['', [Validators.required, Validators.max(120000)]],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageUrl: []
    });
  }

}
