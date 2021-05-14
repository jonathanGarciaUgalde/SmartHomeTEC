import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'; 

import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginAdminComponent } from './componentes/admin/login-admin/login-admin.component';
import { LoginClienteComponent } from './componentes/cliente/login-cliente/login-cliente.component';
import { RegisterComponent } from './componentes/cliente/register/register.component';
import { DispositivosComponent } from './componentes/admin/dispositivos/dispositivos.component';
import { NavbarAdminComponent } from './componentes/admin/navbar-admin/navbar-admin.component';
import { ModalDispositivosComponent } from './componentes/admin/modal-dispositivos/modal-dispositivos.component';
import { GestionTipoComponent } from './componentes/admin/gestion-tipo/gestion-tipo.component';
import { DashboardComponent } from './componentes/admin/dashboard/dashboard.component';
import { TiendaComponent } from './componentes/admin/tienda/tienda.component';
import { ModalTipoComponent } from './componentes/admin/modal-tipo/modal-tipo.component';
import { PerfilComponent } from './componentes/cliente/perfil/perfil.component';
import { NavbarClienteComponent } from './componentes/cliente/navbar-cliente/navbar-cliente.component';
import { ReportesComponent } from './componentes/cliente/reportes/reportes.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginAdminComponent,
    LoginClienteComponent,
    RegisterComponent,
    DispositivosComponent,
    NavbarAdminComponent,
    ModalDispositivosComponent,
    GestionTipoComponent,
    DashboardComponent,
    TiendaComponent,
    ModalTipoComponent,
    PerfilComponent,
    NavbarClienteComponent,
    ReportesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
