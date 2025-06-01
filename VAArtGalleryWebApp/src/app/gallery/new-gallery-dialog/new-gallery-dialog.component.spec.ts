import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewGalleryDialogComponent } from './new-gallery-dialog.component';

describe('NewGalleryDialogComponent', () => {
  let component: NewGalleryDialogComponent;
  let fixture: ComponentFixture<NewGalleryDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NewGalleryDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NewGalleryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
