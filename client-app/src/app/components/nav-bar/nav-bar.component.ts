import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  menuToggle: boolean = false;

  constructor() {}

  ngOnInit(): void {}

  toggleDrawer() {
    this.menuToggle = !this.menuToggle;
  }
}
