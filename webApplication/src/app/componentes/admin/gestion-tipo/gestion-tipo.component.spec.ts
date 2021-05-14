import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GestionTipoComponent } from './gestion-tipo.component';

describe('GestionTipoComponent', () => {
  let component: GestionTipoComponent;
  let fixture: ComponentFixture<GestionTipoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GestionTipoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GestionTipoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
