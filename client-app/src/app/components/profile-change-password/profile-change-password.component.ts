import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { PasswordChange } from 'src/app/interfaces/user.interface';
import { CommonService } from 'src/app/services/common/common.service';
import { AuthService } from 'src/app/services/http/auth/auth.service';
import { IUserDTO } from '../../interfaces/user.interface';

@Component({
  selector: 'app-profile-change-password',
  templateUrl: './profile-change-password.component.html',
  styleUrls: ['./profile-change-password.component.scss'],
})
export class ProfileChangePasswordComponent implements OnInit {
  private destroy$ = new Subject<void>();

  passwordChangeForm = new FormGroup({
    currentPassword: new FormControl('', [Validators.required]),
    newPassword: new FormControl('', [Validators.required]),
    confirmNewPassword: new FormControl('', [Validators.required]),
  });

  error = false;
  errorMsg = '';

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _authService: AuthService,
    private _commonService: CommonService
  ) {}

  ngOnInit(): void {
    this.onChanges();
  }

  handleOnSubmit() {
    if (
      this.passwordChangeForm.controls.currentPassword.value!.length > 0 &&
      this.passwordChangeForm.controls.newPassword.value!.length > 0 &&
      this.passwordChangeForm.controls.confirmNewPassword.value!.length > 0
    ) {
      const { currentPassword, newPassword } = this.passwordChangeForm.value;

      if (
        currentPassword !== undefined &&
        currentPassword !== null &&
        newPassword !== undefined &&
        newPassword !== null
      ) {
        this._commonService.loader$.next(true);
        this._commonService.showSpinner$.next(true);

        const passwordDTO = new PasswordChange(currentPassword, newPassword);

        this._authService
          .changePassword(passwordDTO)
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            next: (user) => {
              if (user instanceof String) return;

              this._authService.user$.next(user as IUserDTO);
              this._commonService.showSpinner$.next(false);

              this.passwordChangeForm.reset();
              this._commonService.icon$.next(
                'fa-check splash-screen-icon--success'
              );
              this._commonService.message$.next('Password changed!');
            },
            error: () => {
              this._commonService.icon$.next(
                'fa-xmark splash-screen-icon--error'
              );
              this._commonService.message$.next('Password was not changed!');
            },
            complete: () => {
              setTimeout(() => {
                this._commonService.loader$.next(false);
                this._commonService.showSpinner$.next(true);
              }, 2500);
            },
          });
      }
    } else {
      this.error = true;
      this.errorMsg = 'Password do not match';
    }
  }

  onChanges() {
    this.passwordChangeForm.valueChanges.subscribe((val) => {
      if (val.currentPassword!.length > 0) {
        this.document
          .getElementById('label-currentpassword')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-currentpassword')
          ?.classList.remove('form__label-active');
      }

      if (val.newPassword!.length > 0) {
        this.document
          .getElementById('label-newpassword')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-newpassword')
          ?.classList.remove('form__label-active');
      }

      if (val.confirmNewPassword!.length > 0) {
        this.document
          .getElementById('label-confirmnewpassword')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-confirmnewpassword')
          ?.classList.remove('form__label-active');
      }

      if (val.newPassword !== val.confirmNewPassword) {
        this.error = true;
        this.errorMsg = 'Passwords do not match';
        this.passwordChangeForm.controls.newPassword.setErrors({
          incorrect: true,
        });
        this.passwordChangeForm.controls.confirmNewPassword.setErrors({
          incorrect: true,
        });
      } else {
        this.error = false;
        this.errorMsg = '';
        this.passwordChangeForm.controls.newPassword.setErrors(null);
        this.passwordChangeForm.controls.confirmNewPassword.setErrors(null);
      }
    });
  }
}
