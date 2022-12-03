import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { Router } from '@angular/router';
import { SignupDTO } from 'src/app/interfaces/user.interface';
import { CommonService } from 'src/app/services/common/common.service';
import { AuthService } from 'src/app/services/http/auth/auth.service';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.scss'],
})
export class SignupFormComponent implements OnInit {
  errorMsg = '';
  error = false;
  loader = false;

  signupForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    displayName: new FormControl('', [Validators.required]),
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [
      Validators.required,
      this.passwordValidator,
    ]),
    confirmPassword: new FormControl('', [Validators.required]),
  });

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _authService: AuthService,
    private _router: Router,
    private _commonService: CommonService
  ) {}

  ngOnInit(): void {
    this.onChanges();
  }

  handleOnSubmit() {
    if (!this.error) {
      const { email, displayName, username, password } = this.signupForm.value;

      if (
        displayName !== undefined &&
        displayName !== null &&
        email !== undefined &&
        email !== null &&
        password !== undefined &&
        password !== null &&
        username !== undefined &&
        username !== null
      ) {
        this._commonService.loader$.next(true);

        const signupDTO = new SignupDTO(displayName, email, password, username);
        this.loader = true;
        this._authService.signup(signupDTO).subscribe({
          next: (user) => {
            console.log(user);
            this.loader = false;
            this._router.navigate(['']);
          },
          error: () => this._commonService.loader$.next(false),
          complete: () => this._commonService.loader$.next(false),
        });
      }
    } else {
      console.log('ERRORS form values ', this.signupForm.value);
    }
  }

  onChanges() {
    this.signupForm.valueChanges.subscribe((val) => {
      if (val.email!.length > 0) {
        this.document
          .getElementById('label-email')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-email')
          ?.classList.remove('form__label-active');
      }

      if (val.password!.length > 0) {
        this.document
          .getElementById('label-password')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-password')
          ?.classList.remove('form__label-active');
      }

      if (val.displayName!.length > 0) {
        this.document
          .getElementById('label-displayname')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-displayname')
          ?.classList.remove('form__label-active');
      }

      if (val.username!.length > 0) {
        this.document
          .getElementById('label-username')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-username')
          ?.classList.remove('form__label-active');
      }

      if (val.confirmPassword!.length > 0) {
        this.document
          .getElementById('label-confirm-password')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-confirm-password')
          ?.classList.remove('form__label-active');
      }

      if (
        this.signupForm.dirty &&
        this.signupForm.controls.confirmPassword.touched &&
        this.signupForm.controls.password.touched &&
        val.password !== val.confirmPassword
      ) {
        this.error = true;
        this.errorMsg = 'Passwords do not match';
        this.signupForm.controls.password.setErrors({ incorrect: true });
        this.signupForm.controls.confirmPassword.setErrors({ incorrect: true });
      } else {
        this.error = false;
        this.errorMsg = '';
        this.signupForm.controls.password.setErrors(null);
        this.signupForm.controls.confirmPassword.setErrors(null);
      }
    });
  }

  passwordValidator() {
    return (control: AbstractControl): ValidationErrors | null => {
      const regex = new RegExp(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{12,30}$/);
      const match = regex.test(control.value);

      return !match
        ? null
        : {
            forbiddenName: {
              error:
                'Password must be minimum 12 characters long, have at lease one number, one lowercase letter, one uppercase letter and one non-alphanumeric character',
            },
          };
    };
  }
}
