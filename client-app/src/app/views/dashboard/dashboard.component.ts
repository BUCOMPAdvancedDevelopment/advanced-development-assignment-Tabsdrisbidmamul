import { Component, OnInit, ViewContainerRef } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  constructor(public viewContainerRef: ViewContainerRef) {}

  ngOnInit(): void {}

  handleProfileClick() {}

  handleGamesClick() {}
}
