import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { InterceptorAgentService } from './services/http/interceptor/interceptor-agent.service';

@NgModule({
  declarations: [AppComponent, NavBarComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, NgbModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorAgentService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
