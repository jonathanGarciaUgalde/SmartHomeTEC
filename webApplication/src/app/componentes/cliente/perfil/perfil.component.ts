import { Component, OnInit } from '@angular/core';

import {ClienteService} from 'src/app/servicios/cliente/cliente.service'

import {Usuario} from "src/app/interfaces/cliente/usuario";

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {

  constructor(private api:ClienteService) { }


  public datosUsuario:Usuario;
  
  ngOnInit(): void {


  	this.api.obtenerInformacionUsuario(localStorage.getItem("email-cliente"))
    .subscribe((response:Usuario) => {
     this.mostrarInfo(response);

    });
  }


   mostrarInfo(response:Usuario){
   this.datosUsuario = response;
   this.datosUsuario.correo = localStorage.getItem('email-cliente');
 }




}
