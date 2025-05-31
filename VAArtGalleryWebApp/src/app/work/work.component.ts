import { Component, OnInit } from '@angular/core';
import { Work } from './models';
import { WorkService } from './work.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-work',
  templateUrl: './work.component.html',
  styleUrl: './work.component.css'
})
export class WorkComponent implements OnInit {
  works: Work[] = [];
  displayedColumns: string[] = ['name'];

  constructor(private workService: WorkService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params=>{
      let galleryId = params.get('galleryId');

      if(galleryId == null){
        throw new Error("gallery cannot be null");
      }
      this.workService.getArtWorks(galleryId).subscribe(works => {this.works = works; console.log(this.works);});
    });    
  }

  // editGalleryClick(galleryId: string) {
  //   console.log(galleryId);
  // }

  // openArtWorksList(galleryId: string) {
  //   console.log(galleryId);
  //   this.router.navigate(["art-works", galleryId]);

  // }
}
