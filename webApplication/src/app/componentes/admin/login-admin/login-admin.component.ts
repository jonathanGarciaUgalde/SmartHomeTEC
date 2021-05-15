import { Component, OnInit } from '@angular/core';

import {Router} from '@angular/router';

import {AdminService} from 'src/app/servicios/admin/admin.service';


@Component({
  selector: 'app-login-admin',
  templateUrl: './login-admin.component.html',
  styleUrls: ['./login-admin.component.css']
})
export class LoginAdminComponent implements OnInit {

  constructor(private router:Router, private api:AdminService) { }

  ngOnInit(): void {

  	if(localStorage.getItem("email-admin") != null){

  		this.router.navigate(["admin/dashboard"]);
  	}
 
  }


  public datosUsuario = {
    Email:null,
    Password:null
  }


  verificarCredenciales():void{

  	this.api.verificarCredenciales(this.datosUsuario.Email,this.datosUsuario.Password)
  	.subscribe(response=>{
  		localStorage.setItem("email-admin", this.datosUsuario.Email);
  		this.router.navigate(["admin/dashboard"]);

  	},(error:any)=>{
        alert("Usuario o contraseña incorrecta, intente de nuevo");
      });
  }  
}
