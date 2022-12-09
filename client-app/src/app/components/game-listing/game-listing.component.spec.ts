import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameListingComponent } from './game-listing.component';

describe('GameListingComponent', () => {
  let component: GameListingComponent;
  let fixture: ComponentFixture<GameListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameListingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
