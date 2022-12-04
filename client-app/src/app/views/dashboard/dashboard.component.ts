import {
  Component,
  OnInit,
  ViewContainerRef,
  AfterViewInit,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit, AfterViewInit {
  constructor(private _router: Router, private _route: ActivatedRoute) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this._router.navigate(['edit'], { relativeTo: this._route });
  }

  handleProfileClick() {}

  handleGamesClick() {}
}
