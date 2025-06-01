import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Work } from './models';

@Injectable({
  providedIn: 'root'
})
export class WorkService {
  private baseUrl = 'https://localhost:7042/api/art-galleries'

  getBaseUrl(galleryId: string, path: string = ''): string {
    return `${this.baseUrl}/${galleryId}/${path}`;
  }

  constructor(private http: HttpClient) {}

  getArtWorks(galleryId: string): Observable<Work[]> {
    let url = this.getBaseUrl(galleryId, "art-works");
    return this.http.get<Work[]>(url);
  }
}
