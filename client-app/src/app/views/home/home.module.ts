import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeroCarouselComponent } from '../../components/hero-carousel/hero-carousel.component';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { CardListingsComponent } from '../../components/card-listings/card-listings.component';
import { SharedModule } from 'src/app/common/shared-module/shared.module';

@NgModule({
  declarations: [HomeComponent, HeroCarouselComponent, CardListingsComponent],
  imports: [CommonModule, HomeRoutingModule, SharedModule],
})
export class HomeModule {}
