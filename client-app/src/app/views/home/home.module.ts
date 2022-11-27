import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeroCarouselComponent } from '../../components/hero-carousel/hero-carousel.component';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';

@NgModule({
  declarations: [HomeComponent, HeroCarouselComponent],
  imports: [CommonModule, HomeRoutingModule],
})
export class HomeModule {}
