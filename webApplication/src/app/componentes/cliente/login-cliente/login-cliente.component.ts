import { Component, OnInit } from '@angular/core';

import {Router} from '@angular/router';

import {ClienteService} from 'src/app/servicios/cliente/cliente.service'

@Component({
  selector: 'app-login-cliente',
  templateUrl: './login-cliente.component.html',
  styleUrls: ['./login-cliente.component.css']
})
export class LoginClienteComponent implements OnInit {

  constructor(private router:Router, private api: ClienteService) { }

  ngOnInit(): void { 

  	if(localStorage.getItem("email-cliente") != null){

  		this.router.navigate(["/perfil"]);
  	}

  }

  public datosUsuario = {
    Email:null,
    Password:null
  }


  verificarCredenciales():void {

  	this.api.verificarCredenciales(this.datosUsuario.Email,this.datosUsuario.Password)
  	.subscribe(response=>{
  		localStorage.clear();
  		localStorage.setItem("email-cliente", this.datosUsuario.Email);
  		this.router.navigate(["/perfil"]);

  	},(error:any)=>{
        alert("Usuario o contraseña incorrecta, intente de nuevo");
        this.datosUsuario.Password = "";
      });
  }

}
