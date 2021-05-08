import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginAdminComponent } from './componentes/admin/login-admin/login-admin.component';
import { LoginClienteComponent } from './componentes/cliente/login-cliente/login-cliente.component';
import { RegisterComponent } from './componentes/cliente/register/register.component';


const routes: Routes = [
{path:'', component: LoginClienteComponent},
{path:'register', component: RegisterComponent},
{path:'admin', component: LoginAdminComponent}



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
