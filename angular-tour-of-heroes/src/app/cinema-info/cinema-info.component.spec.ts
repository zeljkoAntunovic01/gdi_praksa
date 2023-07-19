import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CinemaInfoComponent } from './cinema-info.component';

describe('CinemaInfoComponent', () => {
  let component: CinemaInfoComponent;
  let fixture: ComponentFixture<CinemaInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CinemaInfoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CinemaInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
