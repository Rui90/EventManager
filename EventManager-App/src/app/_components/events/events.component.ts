import { Component, OnInit } from '@angular/core';
import { EventService } from '../../_services/event.service';
import { Event } from '../../_models/Event';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsLocaleService, } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { ToastrService } from 'ngx-toastr';

defineLocale('pt-br', ptBrLocale);
@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  title = 'Events';
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

  file: File;
  fileNameToUpdate: string;

  constructor(
    private eventService: EventService,
    private formbuilder: FormBuilder,
    private localService: BsLocaleService,
    private toastr: ToastrService) {
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
    this.event = Object.assign({}, event);
    this.fileNameToUpdate = event.imageUrl.toString();
    this.event.imageUrl = '';
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
      () => {
        this.toastr.error('Error loading events.');
       }
    );
  }

  getUrl(imageUrl: string) {
    return `http://localhost:5000/resources/images/${imageUrl}`;
  }

  onFileChange(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      this.file = event.target.files;
    }
  }

  uploadDocument() {
    if (this.file) {
      this.showImage = false;
      if (!this.editMode || !this.fileNameToUpdate) {
        const image = this.event.imageUrl.split('\\', 3);
        this.event.imageUrl = image[2];
        this.eventService.uploadDocument(this.file, this.event.imageUrl).subscribe(
        ).add(() => this.showImage = true);
      } else {
        this.event.imageUrl = this.fileNameToUpdate;
        this.eventService.uploadDocument(this.file, this.fileNameToUpdate).subscribe(
        ).add(() => this.showImage = true);
      }
    }
  }

  saveChanges(template: any) {
    if (this.registerForm.valid) {

      if (this.editMode) {
        this.event = Object.assign({id: this.event.id}, this.registerForm.value);
        this.uploadDocument();
        this.eventService.updateEvent(this.event).subscribe(
          (newEvent: Event) => {
            template.hide();
            this.getEvents();
            this.toastr.success('Updated with success.');
          },
          (error) => {
            this.toastr.error('An error ocurred');
          });
      } else {
        this.event = Object.assign({}, this.registerForm.value);
        this.uploadDocument();
        this.eventService.createEvent(this.event).subscribe(
          (newEvent: Event) => {
            template.hide();
            this.getEvents();
            this.toastr.success('Inserted with success.');
          },
          (error) => {
            this.toastr.error('An error ocurred');
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
        this.toastr.success('Dleted with success');
      },
      () => {
        this.toastr.error('An error ocurred');
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
