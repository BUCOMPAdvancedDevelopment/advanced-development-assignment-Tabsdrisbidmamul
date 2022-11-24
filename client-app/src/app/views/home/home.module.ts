import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeroCarouselComponent } from '../../components/hero-carousel/hero-carousel.component';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [HomeComponent, HeroCarouselComponent],
  imports: [CommonModule, HomeRoutingModule, NgbModule],
})
export class HomeModule {}
