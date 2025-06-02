import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Gallery, NewGallery } from './models';

@Injectable({
  providedIn: 'root'
})
export class GalleryService {
  private baseUrl = 'https://localhost:7042/api/art-galleries'
  constructor(private http: HttpClient) { }

  getGalleries(): Observable<Gallery[]> {
    return this.http.get<Gallery[]>(`${this.baseUrl}`);
  }

  createGallery(gallery: NewGallery): Observable<NewGallery> {
    return this.http.post<NewGallery>(this.baseUrl, gallery);
  }

  deleteGallery(galleryId: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.baseUrl}/${galleryId}`);
  }
}
