import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import { StaticProvider } from '@angular/core';

platformBrowserDynamic().bootstrapModule(AppModule, {
  ngZoneEventCoalescing: true,
  providers:[provideAnimations(),  <any>provideToastr({positionClass: 'custom-toast-center'})]
    
})
  .catch(err => console.error(err));
