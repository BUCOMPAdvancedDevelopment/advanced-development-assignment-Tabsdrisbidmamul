import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImagesPipe } from 'src/app/pipes/maps/images.pipe';
import { SpinnerComponent } from 'src/app/components/spinner/spinner.component';
import { SplashScreenComponent } from 'src/app/components/splash-screen/splash-screen.component';

@NgModule({
  declarations: [ImagesPipe, SpinnerComponent, SplashScreenComponent],
  imports: [CommonModule],
  exports: [ImagesPipe, SpinnerComponent, SplashScreenComponent],
})
export class SharedModule {}
