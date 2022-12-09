import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginFormComponent } from 'src/app/components/login-form/login-form.component';
import { SharedModule } from 'src/app/common/shared-module/shared.module';

@NgModule({
  declarations: [LoginComponent, LoginFormComponent],
  imports: [LoginRoutingModule, SharedModule],
})
export class LoginModule {}
