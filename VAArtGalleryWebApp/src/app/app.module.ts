import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { registerLocaleData } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GalleryComponent } from './gallery/gallery.component';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { HttpClientModule } from '@angular/common/http';
import { WorkComponent } from './work/work.component';
import localePt from '@angular/common/locales/pt';
import { MoneyPipe } from './pipes/money';
import { MatDialogModule } from '@angular/material/dialog';
import { NewGalleryDialogComponent } from './gallery/new-gallery-dialog/new-gallery-dialog.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { ToastrModule } from 'ngx-toastr';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorMessageHttpInterceptor } from './interceptors/http.interceptor';

registerLocaleData(localePt);

@NgModule({
  declarations: [
    AppComponent,
    GalleryComponent,
    NewGalleryDialogComponent,
    WorkComponent,
    MoneyPipe,
    ConfirmDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatTableModule,
    MatIconModule,
    MatDialogModule,
    HttpClientModule,
    MatIconModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  exports:[MoneyPipe],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-PT' },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorMessageHttpInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
