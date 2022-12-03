import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { CommonService } from 'src/app/services/common/common.service';

@Component({
  selector: 'app-splash-screen',
  templateUrl: './splash-screen.component.html',
  styleUrls: ['./splash-screen.component.scss'],
})
export class SplashScreenComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  splashScreenIcon = '';
  message = 'Loading...';
  showSpinner = true;

  constructor(private _commonService: CommonService) {
    this._commonService.icon$
      .pipe(takeUntil(this.destroy$))
      .subscribe((icon) => {
        this.splashScreenIcon = icon;
      });

    this._commonService.message$
      .pipe(takeUntil(this.destroy$))
      .subscribe((msg) => {
        this.message = msg;
      });

    this._commonService.showSpinner$
      .pipe(takeUntil(this.destroy$))
      .subscribe((flag) => {
        this.showSpinner = flag;
      });
  }

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  handleBackdrop() {
    this._commonService.loader$.next(false);
  }
}
