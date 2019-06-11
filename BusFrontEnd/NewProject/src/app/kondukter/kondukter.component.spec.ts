import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KondukterComponent } from './kondukter.component';

describe('KondukterComponent', () => {
  let component: KondukterComponent;
  let fixture: ComponentFixture<KondukterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KondukterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KondukterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
