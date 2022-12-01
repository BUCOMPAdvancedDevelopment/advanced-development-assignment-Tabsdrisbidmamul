import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { IUserDTO } from 'src/app/interfaces/user.interface';
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

  constructor(private _authService: AuthService, private _router: Router) {}

  ngOnInit(): void {
    this._authService.user$.pipe(takeUntil(this.destroy$)).subscribe((user) => {
      this.user = user;
      this.loggedIn = user !== null;
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }

  toggleDrawer() {
    this.menuToggle = !this.menuToggle;
  }

  logout() {
    this.userSubmenuToggle = false;
    this.menuToggle = false;
    this._authService.logout();
  }

  login() {
    this.menuToggle = false;
    this._router.navigate(['login']);
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
