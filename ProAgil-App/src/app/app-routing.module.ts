import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EventsComponent } from './_components/events/events.component';
import { GuestsComponent } from './_components/guests/guests.component';
import { DashboardComponent } from './_components/dashboard/dashboard.component';
import { ContactsComponent } from './_components/contacts/contacts.component';
import { UserComponent } from './_components/user/user.component';
import { LoginComponent } from './_components/user/login/login.component';
import { RegistrationComponent } from './_components/user/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';


const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent }
    ]
  },
  { path: 'events', component: EventsComponent, canActivate: [AuthGuard] },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard]  },
  { path: 'guests', component: GuestsComponent, canActivate: [AuthGuard]  },
  { path: 'contacts', component: ContactsComponent, canActivate: [AuthGuard]  },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { 

}
