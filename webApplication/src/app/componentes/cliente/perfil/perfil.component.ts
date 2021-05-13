import { Component, OnInit } from '@angular/core';

import {ClienteService} from 'src/app/servicios/cliente/cliente.service'

import {Usuario} from "src/app/interfaces/cliente/usuario";

import {Router} from '@angular/router';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {

  constructor(private router:Router, private api:ClienteService) { }


  public datosUsuario:Usuario;
  
  ngOnInit(): void {
    if(localStorage.getItem("email-cliente") != null){
      this.api.obtenerInformacionUsuario(localStorage.getItem("email-cliente"))
      .subscribe((response:Usuario) => {
         this.mostrarInfo(response);
         });
    }
    else{
      this.router.navigate([""]);
    }


  	
  }


   mostrarInfo(response:Usuario){
   this.datosUsuario = response;
   this.datosUsuario.correo = localStorage.getItem('email-cliente');
 }




}
