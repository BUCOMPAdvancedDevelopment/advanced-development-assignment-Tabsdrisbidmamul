import {
  Component,
  OnInit,
  ViewContainerRef,
  AfterViewInit,
} from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { IUserDTO } from 'src/app/interfaces/user.interface';
import { AuthService } from 'src/app/services/http/auth/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit, AfterViewInit {
  private destroy$ = new Subject<void>();
  user: IUserDTO | null = null;

  constructor(private _authService: AuthService) {}

  ngOnInit(): void {
    this._authService.user$.pipe(takeUntil(this.destroy$)).subscribe((user) => {
      if (user !== null) {
        this.user = user;
      }
    });
  }

  ngAfterViewInit(): void {
    // this._router.navigate(['edit'], { relativeTo: this._route });
  }

  handleProfileClick() {}

  handleGamesClick() {}
}
