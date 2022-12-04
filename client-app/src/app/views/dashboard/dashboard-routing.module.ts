import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileEditComponent } from 'src/app/components/profile-edit/profile-edit.component';
import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
  // { path: '', redirectTo: 'edit', pathMatch: 'full' },
  { path: '', component: DashboardComponent, pathMatch: 'full' },
  // { path: 'edit', component: ProfileEditComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}
