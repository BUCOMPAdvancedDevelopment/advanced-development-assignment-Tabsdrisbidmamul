import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CatalogueRoutingModule } from './catalogue-routing.module';
import { CatalogueComponent } from './catalogue.component';
import { GameListingComponent } from '../../components/game-listing/game-listing.component';
import { SharedModule } from 'src/app/common/shared-module/shared.module';
import { SearchComponent } from '../../components/search/search.component';

@NgModule({
  declarations: [CatalogueComponent, GameListingComponent, SearchComponent],
  imports: [CommonModule, CatalogueRoutingModule, SharedModule],
})
export class CatalogueModule {}
