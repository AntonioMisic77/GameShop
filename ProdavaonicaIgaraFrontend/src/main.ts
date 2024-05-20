import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module'; // Pretpostavka da postoji AppModule u vašem projektu

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch((err) => console.error(err));
