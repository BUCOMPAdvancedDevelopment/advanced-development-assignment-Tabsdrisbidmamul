import { Component, OnDestroy, OnInit } from '@angular/core';
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
  user: IUserDTO | null = null;
  loggedIn = false;

  constructor(private _authService: AuthService) {}

  ngOnInit(): void {
    this._authService.user$.pipe(takeUntil(this.destroy$)).subscribe((user) => {
      console.log('user ', user);
      this.user = user;
      this.loggedIn = user !== null;
      console.log('logged in ', this.loggedIn);
    });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }

  toggleDrawer() {
    this.menuToggle = !this.menuToggle;
  }
}
