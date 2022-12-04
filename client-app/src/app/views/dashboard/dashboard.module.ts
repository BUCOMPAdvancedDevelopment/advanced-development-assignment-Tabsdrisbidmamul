import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImageCropperModule } from 'ngx-image-cropper';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { ProfileEditComponent } from '../../components/profile-edit/profile-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProfileImageEditComponent } from '../../components/profile-image-edit/profile-image-edit.component';
import { ProfileEditFormComponent } from '../../components/profile-edit-form/profile-edit-form.component';
import { SharedModule } from 'src/app/common/shared-module/shared.module';
import { ProfileChangePasswordComponent } from '../../components/profile-change-password/profile-change-password.component';

@NgModule({
  declarations: [
    DashboardComponent,
    ProfileEditComponent,
    ProfileImageEditComponent,
    ProfileEditFormComponent,
    ProfileChangePasswordComponent,
  ],
  imports: [SharedModule, DashboardRoutingModule, ImageCropperModule],
})
export class DashboardModule {}
