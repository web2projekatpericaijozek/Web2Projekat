import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IzmeniRedVoznjeComponent } from './izmeni-red-voznje.component';

describe('IzmeniRedVoznjeComponent', () => {
  let component: IzmeniRedVoznjeComponent;
  let fixture: ComponentFixture<IzmeniRedVoznjeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IzmeniRedVoznjeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IzmeniRedVoznjeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
