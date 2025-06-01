import { Component, Inject, Input, input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { GalleryService } from '../gallery.service';
import { NewGallery, NewWork } from '../models';

@Component({
  selector: 'app-new-gallery-dialog',
  templateUrl: './new-gallery-dialog.component.html',
  styleUrl: './new-gallery-dialog.component.css'
})
export class NewGalleryDialogComponent {
  
  public newGallery: NewGallery = new NewGallery();
  public newWork: NewWork = new NewWork();
  public dataSourceWorks = new MatTableDataSource<NewWork>(this.newGallery.works);
  displayedColumns: string[] = ['name', 'author', 'creationYear', 'askPrice'];

  constructor(
    private galleryService: GalleryService,
    public dialogRef: MatDialogRef<NewGalleryDialogComponent>,  
    @Inject(MAT_DIALOG_DATA) public data: NewGallery) {

      this.newGallery = data;
      this.refreshTable();    
    }

  refreshTable(): void{
    this.dataSourceWorks.data = [...this.newGallery.works];
  }

  closeDialog(): void {
    this.dialogRef.close();
  }

  createNewGallery(): void {
    this.galleryService.createGallery(this.newGallery).subscribe(response => {
      console.log('Galeria criada:', response);
    });
    this.dialogRef.close();
  }

  addWork(): void{
    this.newGallery.works.push(this.newWork);
    this.newWork = new NewWork();
    this.refreshTable();
  }
}