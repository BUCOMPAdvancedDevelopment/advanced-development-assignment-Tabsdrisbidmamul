import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GameListingComponent } from 'src/app/components/game-listing/game-listing.component';
import { CatalogueResolverService } from './catalogue-resolver.service';
import { CatalogueComponent } from './catalogue.component';

const routes: Routes = [
  {
    path: '',
    component: CatalogueComponent,
    resolve: [CatalogueResolverService],
  },
  {
    path: 'game',
    redirectTo: '',
  },
  {
    path: 'game/:id',
    pathMatch: 'full',
    component: GameListingComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CatalogueRoutingModule {}
