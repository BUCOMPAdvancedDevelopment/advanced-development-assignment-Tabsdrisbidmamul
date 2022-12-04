import {
  Component,
  OnInit,
  ViewContainerRef,
  AfterViewInit,
} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit, AfterViewInit {
  constructor(private _router: Router) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    // this._router.navigate(['dashboard', 'edit']);
  }

  handleProfileClick() {}

  handleGamesClick() {}
}
