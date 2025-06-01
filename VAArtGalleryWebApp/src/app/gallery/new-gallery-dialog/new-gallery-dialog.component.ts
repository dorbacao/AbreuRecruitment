import { Component, Inject, Input, input } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { GalleryService } from '../gallery.service';
import { NewGallery, NewWork } from '../models';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-new-gallery-dialog',
  templateUrl: './new-gallery-dialog.component.html',
  styleUrl: './new-gallery-dialog.component.css'
})
export class NewGalleryDialogComponent {
  
  public newGallery: NewGallery = new NewGallery();
  public newWork: NewWork = new NewWork();
  public dataSourceWorks = new MatTableDataSource<NewWork>(this.newGallery.createArtWorkRequest);
  displayedColumns: string[] = ['name', 'author', 'creationYear', 'askPrice', 'delete'];

  constructor(
    private galleryService: GalleryService,
    private confirmDialog: MatDialog,
    private toastr: ToastrService,
    public newGalleryDialog: MatDialogRef<NewGalleryDialogComponent>,  
    @Inject(MAT_DIALOG_DATA) public data: NewGallery) {

      this.newGallery = data;
      this.refreshTable();
      newGalleryDialog.afterClosed().subscribe(result=>{
        if(!result){
          this.toastr.warning("Utilizador cancelou a inclusão de uma galeria", 'Cancelado');
        }        
      });
    }

  refreshTable(): void{
    this.dataSourceWorks.data = [...this.newGallery.createArtWorkRequest];
  }

  closeDialog(): void {
    this.newGalleryDialog.close(false);
  }

  createNewGallery(): void {
    this.galleryService.createGallery(this.newGallery).subscribe(response => {
      console.log('Galeria criada:', response);
      this.newGalleryDialog.close(true);
    });    
  }

  removeWork(currentWork: number): void{
    const confirmDialog = this.confirmDialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: 'Deseja realmente excluir esta arte?'
    });

    confirmDialog.afterClosed().subscribe(result => {
      if (result) {
        this.newGallery.removeWork(currentWork);
        this.refreshTable();
        this.toastr.success("Arte removida com sucesso", "Sucesso");
      } else {
        this.toastr.warning("Remoção da arte cancelada", "Cancelado");
      }
    });
  }

  addWork(): void{
    this.newGallery.addWorks(this.newWork);
    this.newWork = new NewWork();
    this.toastr.success("Arte incluida com sucesso, utilize o botão salvar para confirmar!", "Sucesso");
    this.refreshTable();
  }
}