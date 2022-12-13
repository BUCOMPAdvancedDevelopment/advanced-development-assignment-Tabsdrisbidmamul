import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImagesPipe } from 'src/app/pipes/maps/images.pipe';
import { SpinnerComponent } from 'src/app/components/spinner/spinner.component';
import { SplashScreenComponent } from 'src/app/components/splash-screen/splash-screen.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SafePipe } from 'src/app/pipes/URL/safe.pipe';

/**
 * Shared module for all common declarations for other modules
 */
@NgModule({
  declarations: [ImagesPipe, SafePipe, SpinnerComponent, SplashScreenComponent],
  imports: [CommonModule, ReactiveFormsModule],
  exports: [
    ImagesPipe,
    SpinnerComponent,
    SplashScreenComponent,
    CommonModule,
    ReactiveFormsModule,
    SafePipe,
  ],
})
export class SharedModule {}
