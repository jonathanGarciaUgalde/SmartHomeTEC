import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginAdminComponent } from './componentes/admin/login-admin/login-admin.component';
import { LoginClienteComponent } from './componentes/cliente/login-cliente/login-cliente.component';
import { RegisterComponent } from './componentes/cliente/register/register.component';
import { DispositivosComponent } from './componentes/admin/dispositivos/dispositivos.component';
import { GestionTipoComponent } from './componentes/admin/gestion-tipo/gestion-tipo.component';
import { DashboardComponent } from './componentes/admin/dashboard/dashboard.component';


const routes: Routes = [
{path:'', component: LoginClienteComponent},
{path:'register', component: RegisterComponent},
{path:'admin', component: LoginAdminComponent},
{path:'admin/dispositivos', component: DispositivosComponent},
{path:'admin/tipo', component: GestionTipoComponent},
{path:'admin/dashboard', component: DashboardComponent},
{path: '**', pathMatch:'full', redirectTo: ''}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
