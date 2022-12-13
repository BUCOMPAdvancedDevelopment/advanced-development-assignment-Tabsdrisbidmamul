import { Component, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { CommonService } from './services/common/common.service';
import { AuthService } from './services/http/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  private destroy$ = new Subject<void>();
  showSpinner = false;

  constructor(
    private _authService: AuthService,
    private _commonService: CommonService
  ) {}

  /**
   * subscribe to splash screen flag
   */
  ngOnInit(): void {
    this._authService.autoLogin();

    this._commonService.loader$
      .pipe(takeUntil(this.destroy$))
      .subscribe((flag) => {
        this.showSpinner = flag;
      });
  }
}
