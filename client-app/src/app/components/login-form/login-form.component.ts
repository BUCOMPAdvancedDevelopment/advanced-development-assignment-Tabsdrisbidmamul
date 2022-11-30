import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ILoginDTO, LoginDTO } from 'src/app/interfaces/user.interface';
import { AuthService } from 'src/app/services/http/auth/auth.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent implements OnInit {
  errorMessage = '';
  loader = false;

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _authService: AuthService,
    private _router: Router
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

    const loginDTO = new LoginDTO(email, password);

    console.log('loginDto ', loginDTO);

    this.loader = true;
    this._authService.login(loginDTO).subscribe((user) => {
      console.log('user ', user);
      this.loader = false;
      this._router.navigate(['dashboard']);
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
