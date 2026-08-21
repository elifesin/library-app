// Entry point. Uygulama bootstrapApplication ile app.config'deki yapılandırma ve app.ts'deki kök bileşeni kullanarak uygulamayı başlatır.

import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
