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
  public dataSourceWorks = new MatTableDataSource<NewWork>(this.newGallery.artWorks);
  displayedColumns: string[] = ['name', 'author', 'creationYear', 'askPrice', 'delete'];

  constructor(
    private galleryService: GalleryService,
    private confirmDialog: MatDialog,
    private toastr: ToastrService,
    public newGalleryDialog: MatDialogRef<NewGalleryDialogComponent>,  
    @Inject(MAT_DIALOG_DATA) public data: NewGallery) {

      this.newGallery = data;
      this.refreshTable();      
    }

  refreshTable(): void{
    this.dataSourceWorks.data = [...this.newGallery.artWorks];
  }

  closeDialog(): void {
    const confirmDialog = this.confirmDialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: 'Deseja realmente cancelar esta inclusão?'
    });

    confirmDialog.afterClosed().subscribe(result => {
      if (result) {
        this.newGalleryDialog.close(false);
        this.toastr.warning("Inclusão cancelada pelo utilizador", "Cancelado");
      }
    });
    
  }

  createNewGallery(): void {
    this.galleryService.createGallery(this.newGallery).subscribe(response => {
      this.toastr.success("Nova galeria incluida com sucesso!", "Inclusão");
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
        this.toastr.success("Arte removida da lista com sucesso, utilize o botão salvar para confirmar.", "Sucesso");
      } else {
        this.toastr.warning("Exclusão da arte cancelada", "Cancelado");
      }
    });
  }

  addWork(): void{
    this.newGallery.addWorks(this.newWork);
    this.newWork = new NewWork();
    this.refreshTable();
  }
}