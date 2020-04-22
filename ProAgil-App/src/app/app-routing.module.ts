import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EventsComponent } from './_components/events/events.component';
import { GuestsComponent } from './_components/guests/guests.component';
import { DashboardComponent } from './_components/dashboard/dashboard.component';
import { ContactsComponent } from './_components/contacts/contacts.component';


const routes: Routes = [
  {
    path: 'events', component: EventsComponent
  },
  {
    path: 'dashboard', component: DashboardComponent
  },
  {
    path: 'guests', component: GuestsComponent
  },
  {
    path: 'contacts', component: ContactsComponent
  },
  {
    path: '', redirectTo: 'dashboard', pathMatch: 'full'
  },
  {
    path: '**', redirectTo: 'dashboard', pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { 

}
