import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { EventsComponent } from './_components/events/events.component';
import { NavComponent } from './_components/nav/nav.component';

import { DateTimeFormatPipe } from './_helpers/DateTimeFormat.pipe';

import { EventService } from './_services/event.service';
import { ToastrModule } from 'ngx-toastr';
import { GuestsComponent } from './_components/guests/guests.component';
import { DashboardComponent } from './_components/dashboard/dashboard.component';
import { ContactsComponent } from './_components/contacts/contacts.component';
import { TitleComponent } from './_core/title/title.component';
import { UserComponent } from './_components/user/user.component';
import { LoginComponent } from './_components/user/login/login.component';
import { RegistrationComponent } from './_components/user/registration/registration.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { EventEditComponent } from './_components/events/event-edit/event-edit.component';
import { TabsModule } from 'ngx-bootstrap/tabs';

@NgModule({
   declarations: [
      AppComponent,
      EventsComponent,
      NavComponent,
      DateTimeFormatPipe,
      GuestsComponent,
      DashboardComponent,
      ContactsComponent,
      TitleComponent,
      UserComponent,
      LoginComponent,
      RegistrationComponent,
      EventEditComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      BsDatepickerModule.forRoot(),
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      ToastrModule.forRoot(),
      TabsModule.forRoot()
   ],
   providers: [
      EventService,
      {
         provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true
      }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
