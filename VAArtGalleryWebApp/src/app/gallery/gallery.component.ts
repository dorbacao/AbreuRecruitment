import { Component, OnInit } from '@angular/core';
import { Gallery, NewGallery } from './models';
import { GalleryService } from './gallery.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { NewGalleryDialogComponent } from './new-gallery-dialog/new-gallery-dialog.component';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrl: './gallery.component.css'
})

export class GalleryComponent implements OnInit {
  galleries: Gallery[] = [];
  displayedColumns: string[] = ['name', 'city', 'manager', 'nbrWorks', 'actions'];

  constructor(private galleryService: GalleryService, public dialog: MatDialog, private router: Router) { }

  ngOnInit(): void {
    console.log('cenas');
    this.galleryService.getGalleries().subscribe(galleries => {this.galleries = galleries; console.log(this.galleries);});
  }

  editGalleryClick(galleryId: string) {
    console.log(galleryId);
  }

  openArtWorksList(galleryId: string) {
    console.log(galleryId);
    this.router.navigate(["art-works", galleryId]);

  }

  addGallery() {
    this.dialog.open(NewGalleryDialogComponent, {
      maxWidth: '100vw',
      width: '80%',
      data: new NewGallery(),
    });
  }
}
