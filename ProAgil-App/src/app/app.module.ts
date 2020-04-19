import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { EventsComponent } from './_components/events/events.component';
import { NavComponent } from './_components/nav/nav.component';

import { DateTimeFormatPipe } from './_helpers/DateTimeFormat.pipe';

import { EventService } from './_services/event.service';

@NgModule({
   declarations: [
      AppComponent,
      EventsComponent,
      NavComponent,
      DateTimeFormatPipe,
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
   ],
   providers: [
      EventService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
