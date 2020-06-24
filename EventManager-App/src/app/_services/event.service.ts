import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event } from '../_models/Event';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class EventService {

  baseUrl = environment.api + 'api/events';


  constructor(private http: HttpClient) { }

  getEvent(): Observable<Event[]> {

    return this.http.get<Event[]>(this.baseUrl);
  }

  getEventById(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.baseUrl}/${id}`);
  }

  searchEvents(query: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.baseUrl}/search/${query}`);
  }

  createEvent(event: Event) {
    return this.http.post(`${this.baseUrl}`, event);
  }

  updateEvent(event: Event) {
    return this.http.put(`${this.baseUrl}/${event.id}`, event);
  }

  deleteEvent(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  uploadDocument(file: File, fileName: string) {
    const fileToUpload = file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileName);
    return this.http.post(`${this.baseUrl}/upload`, formData);
  }
}
