import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { IImage, IUserDTO } from 'src/app/interfaces/user.interface';
import { BasketService } from 'src/app/services/common/basket.service';
import { CommonService } from 'src/app/services/common/common.service';
import { AuthService } from 'src/app/services/http/auth/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  menuToggle: boolean = false;
  userSubmenuToggle = false;
  user: IUserDTO | null = null;
  loggedIn = false;
  userImage!: IImage | null | undefined;
  itemsInCart = 0;

  constructor(
    private _authService: AuthService,
    private _router: Router,
    private _basketService: BasketService,
    private _commonService: CommonService
  ) {}

  ngOnInit(): void {
    this._authService.user$.pipe(takeUntil(this.destroy$)).subscribe((user) => {
      this.user = user;
      this.loggedIn = user !== null;
      this.userImage = user?.image;
    });

    this._basketService.list.pipe(takeUntil(this.destroy$)).subscribe({
      next: (items) => {
        if (items === null) return;

        this.itemsInCart = items.length;
      },
    });

    this._commonService.closeMenus$
      .pipe(takeUntil(this.destroy$))
      .subscribe((flag) => {
        if (flag) {
          this.clearMenus();
        }
      });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  toggleDrawer() {
    this.menuToggle = !this.menuToggle;
  }

  logout() {
    this.clearMenus();
    this._authService.logout();
  }

  login() {
    this.clearMenus();
    this._router.navigate(['login']);
  }

  navigateToDashboard() {
    this.clearMenus();
    this._router.navigate(['dashboard', 'edit']);
  }

  navigateToHome() {
    this.clearMenus();
    this._router.navigate(['']);
  }

  navigateToCatalogue() {
    this.clearMenus();
    this._router.navigate(['catalogue']);
  }

  navigateToBasket() {
    this.clearMenus();
    this._router.navigate(['basket']);
  }

  openSearchContainer() {
    this._commonService.showSpinner$.next(false);
    this._commonService.message$.next('');
    this._commonService.loader$.next(true);
    this._commonService.icon$.next('');
    this._commonService.showInput$.next(true);
  }

  private clearMenus() {
    this.menuToggle = false;
    this.userSubmenuToggle = false;
  }

  @HostListener('document:click', ['$event'])
  toggleUserSubmenuFromDocument(event: PointerEvent) {
    if (
      !(event.target as HTMLElement).closest('.nav__li.nav__profile-container')
    ) {
      this.userSubmenuToggle = false;
    }
  }

  toggleUserSubmenu() {
    this.userSubmenuToggle = !this.userSubmenuToggle;
  }
}
