import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GameEditorComponent } from 'src/app/components/game-editor/game-editor.component';
import { OrdersComponent } from 'src/app/components/orders/orders.component';
import { ProfileEditComponent } from 'src/app/components/profile-edit/profile-edit.component';
import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,

    children: [
      {
        path: 'edit',
        component: ProfileEditComponent,
      },
      {
        path: 'games',
        component: GameEditorComponent,
      },
      {
        path: 'orders',
        component: OrdersComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}
