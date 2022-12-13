import { DOCUMENT } from '@angular/common';
import {
  AfterViewInit,
  Component,
  Inject,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { IUserDTO } from 'src/app/interfaces/user.interface';
import { CommonService } from 'src/app/services/common/common.service';
import { AuthService } from 'src/app/services/http/auth/auth.service';
import { ProfileEditService } from 'src/app/services/http/profile/profile-edit.service';

@Component({
  selector: 'app-profile-edit-form',
  templateUrl: './profile-edit-form.component.html',
  styleUrls: ['./profile-edit-form.component.scss'],
})
export class ProfileEditFormComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  private destroy$ = new Subject<void>();
  user: IUserDTO | null = null;

  errorMsg = '';
  error = false;
  loader = false;

  profileForm = new FormGroup({
    displayName: new FormControl(''),
  });

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _authService: AuthService,
    private _profileEditService: ProfileEditService,
    private _commonService: CommonService
  ) {}

  ngOnInit(): void {
    this._authService.user$
      .pipe(takeUntil(this.destroy$))
      .subscribe((_user) => {
        this.user = _user;

        if (_user !== null) {
          this.profileForm.controls.displayName.setValue(_user.displayName);
        }
      });

    this.onChanges();
  }

  ngAfterViewInit(): void {
    this.document
      .getElementById('label-displayname')
      ?.classList.add('form__label-active');
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Send over the edited profile to the api
   */
  handleOnSubmit() {
    const { displayName } = this.profileForm.value;

    this.error = false;
    this.errorMsg = '';

    if (
      displayName !== undefined &&
      displayName !== null &&
      displayName.length > 0
    ) {
      this._commonService.loader$.next(true);
      this._commonService.showSpinner$.next(true);

      this._profileEditService.editDisplayName(displayName).subscribe({
        next: (response) => {
          this.user!.displayName = response.displayName;

          this._authService.user$.next(this.user!);
          this._commonService.showSpinner$.next(false);

          this._commonService.icon$.next(
            'fa-check splash-screen-icon--success'
          );
          this._commonService.message$.next('Profile updated');
        },
        error: () => {
          this._commonService.icon$.next('fa-xmark splash-screen-icon--error');
          this._commonService.message$.next('Could not update profile!');
          this._commonService.showSpinner$.next(false);

          setTimeout(() => {
            this._commonService.loader$.next(false);
            this._commonService.showSpinner$.next(true);
          }, 2500);
        },
        complete: () => {
          setTimeout(() => {
            this._commonService.loader$.next(false);
            this._commonService.showSpinner$.next(true);
          }, 2500);
        },
      });
    } else {
      this.error = true;
      this.errorMsg = 'A display name is required';
    }
  }

  onChanges() {
    this.profileForm.valueChanges.subscribe((val) => {
      if (val.displayName?.length) {
        this.document
          .getElementById('label-displayname')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-displayname')
          ?.classList.remove('form__label-active');
      }

      if (
        this.profileForm.dirty &&
        this.profileForm.controls.displayName.touched &&
        this.profileForm.controls.displayName.value?.length === 0
      ) {
        this.error = true;
        this.errorMsg = 'Please enter a display name';
        this.profileForm.controls.displayName.setErrors({ incorrect: true });
      } else {
        this.profileForm.controls.displayName.setErrors(null);
      }
    });
  }
}
