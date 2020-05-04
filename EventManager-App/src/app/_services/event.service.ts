import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Event } from '../_models/Event';
@Injectable({
  providedIn: 'root'
})
export class EventService {

  baseUrl = 'http://localhost:5000/api/events';


  constructor(private http: HttpClient) { }

  getEvent(): Observable<Event[]> {

    return this.http.get<Event[]>(this.baseUrl);
  }

  getEventById(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.baseUrl}/${id}`);
  }

  getEventByTheme(theme: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.baseUrl}/getByTheme/${theme}`);
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