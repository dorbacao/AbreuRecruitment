import { Component, OnInit } from '@angular/core';
import { Gallery, NewGallery } from './models';
import { GalleryService } from './gallery.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { NewGalleryDialogComponent } from './new-gallery-dialog/new-gallery-dialog.component';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrl: './gallery.component.css'
})

export class GalleryComponent implements OnInit {
  galleries: Gallery[] = [];
  displayedColumns: string[] = ['name', 'city', 'manager', 'nbrWorks', 'actions'];

  constructor(private galleryService: GalleryService, 
    private toastr: ToastrService,
    private confirmDialog: MatDialog,
    public dialog: MatDialog, private router: Router) { }

  ngOnInit(): void {
    this.refreshAllGallery();  
  }

  refreshAllGallery(): void{
    this.galleryService.getGalleries().subscribe(galleries => {this.galleries = galleries; console.log(this.galleries);});
  }

  editGalleryClick(galleryId: string) {
    console.log(galleryId);
  }

  openArtWorksList(galleryId: string) {
    console.log(galleryId);
    this.router.navigate(["art-works", galleryId]);

  }

  deleteGallery(gallery: Gallery): void{
    const confirmDialog = this.confirmDialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: `Deseja realmente excluir a galeria de arte "${gallery.name}" com todas as artes ?`
    });

    confirmDialog.afterClosed().subscribe(result => {
      if (result) {

        this.galleryService.deleteGallery(gallery.id).subscribe({
          next: (result) => {
            if(result){
              this.toastr.success("Galeria removida com sucesso", "Sucesso");
            }else{
              this.toastr.error("Não foi poss+ivel excluir a galeria. Tente novamente mais tarde.", "Erro");
            }
            this.refreshAllGallery();
          },
          error: (err) => {
            this.toastr.error("Não foi possível excluir a galeria.", "Erro ao excluir");
          }
        });
      } else {
        this.toastr.warning("Ação cancelada!", "Cancelado");
      }
    });
  }

  addGallery() {
    let dialogResult = this.dialog.open(NewGalleryDialogComponent, {
      maxWidth: '100vw',
      width: '80%',
      data: new NewGallery(),
    });
    dialogResult.afterClosed().subscribe(()=>{
      this.refreshAllGallery();
    });
  }
}
