import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

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
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
