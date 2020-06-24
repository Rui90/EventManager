import { Component, OnInit } from '@angular/core';
import { EventService } from 'src/app/_services/event.service';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Event } from '../../../_models/Event';
import { BsLocaleService, } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';

defineLocale('pt-br', ptBrLocale);
@Component({
  selector: 'app-event-edit',
  templateUrl: './event-edit.component.html',
  styleUrls: ['./event-edit.component.css']
})
export class EventEditComponent implements OnInit {

  title = 'Edit Event';
  registerForm: FormGroup;
  eventModel: Event = new Event();
  imageUrl = 'assets/images.png';
  file: File;
  fileNameToUpdate: string;
  showImage = true;
  isLoading = true;

  get lots(): FormArray {
    return this.registerForm.get('lots') as FormArray;
  }

  get socialNetworks(): FormArray {
    return this.registerForm.get('socialNetworks') as FormArray;
  }

  constructor(
    private eventService: EventService,
    private formbuilder: FormBuilder,
    private localService: BsLocaleService,
    private toastr: ToastrService,
    private router: ActivatedRoute) {
      this.localService.use('pt-br');
     }

     ngOnInit() {
      this.validation();
      this.getEvent();
    }

    getEvent() {
      this.isLoading = true;
      const eventId = +this.router.snapshot.paramMap.get('id');
      this.eventService.getEventById(eventId).subscribe(
        (response: Event) => {
          this.eventModel = Object.assign({}, response),
          this.imageUrl = response.imageUrl ? `${environment.api}/resources/images/${response.imageUrl}` : 'assets/images.png';
          this.fileNameToUpdate = response.imageUrl ? response.imageUrl.toString() : '';
          this.registerForm.patchValue(this.eventModel);
          this.eventModel.lots.forEach(lot => {
            this.lots.push(this.createLot(lot));
          });
          this.eventModel.socialNetworks.forEach(socialNetwork => {
            this.socialNetworks.push(this.createSocialNetwork(socialNetwork));
          });
          this.isLoading = false;
        },
        () => {
          this.toastr.error(`Couldn't load event with id ${eventId}`);
          this.isLoading = false;
        });
    }

    validation() {
      this.registerForm = this.formbuilder.group({
        id: [],
        theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
        location: ['', Validators.required],
        date: ['', Validators.required],
        capacity: ['', [Validators.required, Validators.max(120000)]],
        phoneNumber: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        imageUrl: [],
        lots: this.formbuilder.array([]),
        socialNetworks: this.formbuilder.array([])
      });
    }

    createLot(lot: any): FormGroup {
      return this.formbuilder.group(
        {
          id: [lot.id],
          name: [lot.name, Validators.required],
          quantity: [lot.quantity, Validators.required],
          beginDate: [lot.beginDate],
          endDate: [lot.endDate],
          price: [lot.price, Validators.required]
        }
      );
    }

    createSocialNetwork(socialNetwork: any): FormGroup {
      return this.formbuilder.group(
        {
          id: [socialNetwork.id],
          url: [socialNetwork.url, Validators.required],
          name: [socialNetwork.name, Validators.required],
        }
      );
    }

    onFileChange(file: any) {
      const reader = new FileReader();
      this.file = file;
      reader.onload = (event: any) => this.imageUrl = event.target.result;
      reader.readAsDataURL(file[0]);
    }

    addLot(): void {
      this.lots.push(this.createLot({ id: 0}));
    }

    addSocialNetwork(): void {
      this.socialNetworks.push(this.createSocialNetwork({ id: 0}));
    }

    removeLot(id: number) {
      this.lots.removeAt(id);
    }

    removeSocialNetwork(id: number) {
      this.socialNetworks.removeAt(id);
    }

    saveEvent() {
      if (this.registerForm.valid) {
        this.eventModel = Object.assign({id: this.eventModel.id}, this.registerForm.value);
        this.uploadDocument();
        this.eventService.updateEvent(this.eventModel).subscribe(
          (newEvent: Event) => {
            this.toastr.success('Updated with success.');
          },
          (error) => {
            this.toastr.error('An error ocurred');
          });
      }
    }

    uploadDocument() {
      if (this.file) {
        if (this.fileNameToUpdate !== '') {
          this.eventModel.imageUrl = this.fileNameToUpdate;
        } else {
          const image = this.eventModel.imageUrl.split('\\', 3);
          this.eventModel.imageUrl = image[2];
        }
        this.showImage = false;
        this.eventService.uploadDocument(this.file, this.fileNameToUpdate).subscribe(
          () => {
            this.imageUrl = `${environment.api}/resources/images/${this.eventModel.imageUrl}`;
          }
        ).add(() => this.showImage = true);
      }
    }
}
