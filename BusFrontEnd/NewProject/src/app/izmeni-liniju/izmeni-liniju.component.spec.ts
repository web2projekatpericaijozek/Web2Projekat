import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IzmeniLinijuComponent } from './izmeni-liniju.component';

describe('IzmeniLinijuComponent', () => {
  let component: IzmeniLinijuComponent;
  let fixture: ComponentFixture<IzmeniLinijuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IzmeniLinijuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IzmeniLinijuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
