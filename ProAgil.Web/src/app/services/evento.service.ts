import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Observable} from 'rxjs';
import { Evento } from '../models/Evento';
@Injectable({
  providedIn: 'root'
})
export class EventoService {
baseUrl = 'http://localhost:10005/api/evento';
constructor(private http:HttpClient) { }

getAllEvento(): Observable<Evento[]>{
  return this.http.get<Evento[]>(this.baseUrl);
}
getEventoByTema(tema:string): Observable<Evento[]>{
  return this.http.get<Evento[]>(`${this.baseUrl}/getByTema/{tema}`);
}
getEventoById(id:number): Observable<Evento[]>{
  return this.http.get<Evento[]>(`${this.baseUrl}/${id}`);
}
}

