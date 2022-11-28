import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameListingsComponent } from './game-listings.component';

describe('GameListingsComponent', () => {
  let component: GameListingsComponent;
  let fixture: ComponentFixture<GameListingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameListingsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameListingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
