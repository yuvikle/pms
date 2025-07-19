import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Trades } from './trades.component';

describe('Trades', () => {
  let component: Trades;
  let fixture: ComponentFixture<Trades>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Trades]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Trades);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
