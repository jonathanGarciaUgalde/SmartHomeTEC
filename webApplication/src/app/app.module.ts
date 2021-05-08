import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginAdminComponent } from './componentes/admin/login-admin/login-admin.component';
import { LoginClienteComponent } from './componentes/cliente/login-cliente/login-cliente.component';
import { RegisterComponent } from './componentes/cliente/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginAdminComponent,
    LoginClienteComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
