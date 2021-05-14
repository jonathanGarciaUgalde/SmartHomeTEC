import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalDispositivosComponent } from './modal-dispositivos.component';

describe('ModalDispositivosComponent', () => {
  let component: ModalDispositivosComponent;
  let fixture: ComponentFixture<ModalDispositivosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalDispositivosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalDispositivosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
