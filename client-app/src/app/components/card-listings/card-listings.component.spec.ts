import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardListingsComponent } from './card-listings.component';

describe('CardListingsComponent', () => {
  let component: CardListingsComponent;
  let fixture: ComponentFixture<CardListingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardListingsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardListingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
