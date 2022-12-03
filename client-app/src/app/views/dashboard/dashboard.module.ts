import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { ProfileEditComponent } from '../../components/profile-edit/profile-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProfileImageEditComponent } from '../../components/profile-image-edit/profile-image-edit.component';
import { ProfileEditFormComponent } from '../../components/profile-edit-form/profile-edit-form.component';

@NgModule({
  declarations: [DashboardComponent, ProfileEditComponent, ProfileImageEditComponent, ProfileEditFormComponent],
  imports: [CommonModule, DashboardRoutingModule, ReactiveFormsModule],
})
export class DashboardModule {}
