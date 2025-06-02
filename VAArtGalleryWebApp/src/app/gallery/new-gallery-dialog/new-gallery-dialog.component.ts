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
    @Inject(MAT_DIALOG_DATA) public data: any) {

      this.newGallery = data.gallery;
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

  saveGallery(): void {
    if(this.data.newGallery){
      this.galleryService.createGallery(this.newGallery).subscribe(response => {
        this.toastr.success("Nova galeria incluida com sucesso!", "Inclusão");
        this.newGalleryDialog.close(true);
      });    
    }else{
      this.galleryService.updateGallery(this.newGallery).subscribe(response => {
        this.toastr.success("Galeria alterada com sucesso!", "Alteração");
        this.newGalleryDialog.close(true);
      }); 
    }
    
  }

  removeWork(currentWork: number): void{
    const confirmDialog = this.confirmDialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: 'Deseja realmente excluir esta arte?'
    });

    confirmDialog.afterClosed().subscribe(result => {
      if (result) {
        this.newGallery.artWorks.splice(currentWork,1);
        this.refreshTable();
        this.toastr.success("Arte removida da lista com sucesso, utilize o botão salvar para confirmar.", "Sucesso");
      } else {
        this.toastr.warning("Exclusão da arte cancelada", "Cancelado");
      }
    });
  }

  addWork(): void{
    if(!/^[0-9]{4}$/.test(this.newWork.creationYear)){
      this.toastr.error("Ano de Criação da obra é inválido", "Validação");
      return;
    }
    if(!/^\d+(\.\d{4})?$/.test(this.newWork.askPrice)){
      this.toastr.error("Preço da obra é inválido", "Validação");
      return;
    }   
    this.newGallery.artWorks.push(this.newWork);
    this.newWork = new NewWork();
    this.refreshTable();
  }
}