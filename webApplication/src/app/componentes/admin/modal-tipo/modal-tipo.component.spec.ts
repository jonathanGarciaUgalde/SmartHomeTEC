import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalTipoComponent } from './modal-tipo.component';

describe('ModalTipoComponent', () => {
  let component: ModalTipoComponent;
  let fixture: ComponentFixture<ModalTipoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalTipoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalTipoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
