import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameEditorComponent } from './game-editor.component';

describe('GameEditorComponent', () => {
  let component: GameEditorComponent;
  let fixture: ComponentFixture<GameEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameEditorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
