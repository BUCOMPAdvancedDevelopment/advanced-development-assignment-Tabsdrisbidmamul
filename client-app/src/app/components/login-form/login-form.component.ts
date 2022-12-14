import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ILoginDTO, LoginDTO } from 'src/app/interfaces/user.interface';
import { CommonService } from 'src/app/services/common/common.service';
import { AuthService } from 'src/app/services/http/auth/auth.service';

/**
 * Log user the in
 */
@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent implements OnInit {
  errorMessage = '';
  error = false;
  loader = false;

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
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
    const { email, password } = this.loginForm.value;

    if (
      email === null ||
      email === undefined ||
      password === null ||
      password === undefined
    ) {
      this.errorMessage = 'Login or password is not filled in';
      return;
    }

    this._commonService.loader$.next(true);

    const loginDTO = new LoginDTO(email, password);

    this.error = false;
    this.errorMessage = '';
    this.loader = true;
    this.loginForm.controls.email.setErrors(null);

    this._authService.login(loginDTO).subscribe({
      next: (user) => {
        this.loader = false;
        this._router.navigate(['']);
      },
      error: (errorMsg) => {
        this.error = true;
        this.errorMessage = errorMsg;
        this.loader = false;
        this.loginForm.controls.email.setErrors({ incorrect: true });
        this._commonService.loader$.next(false);
      },
      complete: () => this._commonService.loader$.next(false),
    });
  }

  onChanges() {
    this.loginForm.valueChanges.subscribe((val) => {
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
    });
  }
}
