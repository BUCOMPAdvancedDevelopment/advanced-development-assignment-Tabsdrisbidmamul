import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  ElementRef,
  AfterViewInit,
} from '@angular/core';
import { Router } from '@angular/router';
import {
  debounceTime,
  distinctUntilChanged,
  filter,
  flatMap,
  fromEvent,
  Subject,
  takeUntil,
  tap,
} from 'rxjs';
import { CommonService } from 'src/app/services/common/common.service';

@Component({
  selector: 'app-splash-screen',
  templateUrl: './splash-screen.component.html',
  styleUrls: ['./splash-screen.component.scss'],
})
export class SplashScreenComponent implements OnInit, OnDestroy, AfterViewInit {
  private destroy$ = new Subject<void>();
  @ViewChild('input') private input!: ElementRef;

  splashScreenIcon = '';
  message = 'Loading...';
  showSpinner = true;
  showInput = false;

  searchValue = '';

  constructor(private _commonService: CommonService, private _router: Router) {
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

    this._commonService.showInput$
      .pipe(takeUntil(this.destroy$))
      .subscribe((flag) => {
        this.showInput = flag;
      });
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    if (this.showInput) {
      fromEvent(this.input.nativeElement, 'keyup')
        .pipe(
          takeUntil(this.destroy$),
          filter(Boolean),
          debounceTime(500),
          distinctUntilChanged(),
          tap(() => {
            this.searchValue = this.input.nativeElement.value;
          })
        )
        .subscribe((value) => {});
    }
  }

  navigateToSearch() {
    if (this.searchValue === '') return;

    this._commonService.loader$.next(false);
    this._commonService.showInput$.next(false);
    this._commonService.closeMenus$.next(true);

    this._router.navigate(['catalogue', 'search', this.searchValue]);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  handleBackdrop() {
    this._commonService.loader$.next(false);
    this._commonService.showInput$.next(false);
  }
}
