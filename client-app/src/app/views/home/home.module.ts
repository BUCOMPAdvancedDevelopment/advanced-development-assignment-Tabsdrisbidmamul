import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeroCarouselComponent } from '../../components/hero-carousel/hero-carousel.component';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { SharedModule } from 'src/app/common/shared-module/shared.module';
import { GameListingsComponent } from '../../components/game-listings/game-listings.component';

@NgModule({
  declarations: [HomeComponent, HeroCarouselComponent, GameListingsComponent],
  imports: [HomeRoutingModule, SharedModule],
})
export class HomeModule {}
