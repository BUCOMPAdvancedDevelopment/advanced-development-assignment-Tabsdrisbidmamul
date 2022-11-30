import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/http/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'client-app';

  constructor(private _authService: AuthService) {}

  ngOnInit(): void {
    this._authService.autoLogin();
  }
}
