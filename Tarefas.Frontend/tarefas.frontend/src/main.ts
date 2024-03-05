import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { Tarefa } from './app/tarefa_model';

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.error(err));
