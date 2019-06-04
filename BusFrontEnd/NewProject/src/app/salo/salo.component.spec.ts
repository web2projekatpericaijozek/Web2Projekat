import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SaloComponent } from './salo.component';

describe('SaloComponent', () => {
  let component: SaloComponent;
  let fixture: ComponentFixture<SaloComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SaloComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaloComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
