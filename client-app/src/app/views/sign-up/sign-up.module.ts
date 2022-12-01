import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SignUpRoutingModule } from './sign-up-routing.module';
import { SignUpComponent } from './sign-up.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SignupFormComponent } from '../../components/signup-form/signup-form.component';

@NgModule({
  declarations: [SignUpComponent, SignupFormComponent],
  imports: [CommonModule, SignUpRoutingModule, ReactiveFormsModule],
})
export class SignUpModule {}
