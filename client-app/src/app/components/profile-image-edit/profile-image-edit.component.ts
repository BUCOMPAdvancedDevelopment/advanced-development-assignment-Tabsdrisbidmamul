import { Component, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { IUserDTO } from 'src/app/interfaces/user.interface';
import { AuthService } from 'src/app/services/http/auth/auth.service';

@Component({
  selector: 'app-profile-image-edit',
  templateUrl: './profile-image-edit.component.html',
  styleUrls: ['./profile-image-edit.component.scss'],
})
export class ProfileImageEditComponent implements OnInit {
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
}
