
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginAdminComponent } from './componentes/admin/login-admin/login-admin.component';
import { LoginClienteComponent } from './componentes/cliente/login-cliente/login-cliente.component';
import { RegisterComponent } from './componentes/cliente/register/register.component';
import { DispositivosComponent } from './componentes/admin/dispositivos/dispositivos.component';
import { GestionTipoComponent } from './componentes/admin/gestion-tipo/gestion-tipo.component';
import { DashboardComponent } from './componentes/admin/dashboard/dashboard.component';
import { TiendaComponent } from './componentes/admin/tienda/tienda.component';
import { PerfilComponent } from './componentes/cliente/perfil/perfil.component';


const routes: Routes = [
{path:'', component: LoginClienteComponent},
{path:'register', component: RegisterComponent},
{path:'perfil', component: PerfilComponent},
{path:'admin', component: LoginAdminComponent},
{path:'admin/dispositivos', component: DispositivosComponent},
{path:'admin/tipo', component: GestionTipoComponent},
{path:'admin/dashboard', component: DashboardComponent},
{path:'admin/tienda', component: TiendaComponent},
{path: '**', pathMatch:'full', redirectTo: ''}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
