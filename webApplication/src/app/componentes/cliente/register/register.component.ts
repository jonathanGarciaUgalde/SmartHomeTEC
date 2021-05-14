import { Component, OnInit } from '@angular/core';
import {Usuario} from "src/app/interfaces/cliente/usuario";
import {ClienteService} from 'src/app/servicios/cliente/cliente.service';
import {RegionInterface} from "src/app/interfaces/cliente/region";
import {Ubicacion} from "src/app/interfaces/cliente/ubicacion";
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private router:Router, private api:ClienteService) { }

  ngOnInit(): void {
  }


  public datosUsuario = {
  	nombre:null,
  	apellidos:null,
  	continente:null,
  	pais:null,
  	email:null,
  	contra: null,
  	confirmeContra:null,
  	direcciones:null
  }
  


  registrar(){

  	const user = this.datosUsuario;
  	const region:RegionInterface = {};
  	region.continente = user.continente;
  	region.pais = user.pais;
  	const direcciones:Ubicacion[] = [];

  	for(let ubicacion of user.direcciones.split("\n")){

  		direcciones.push({"ubicacion": ubicacion});

  	}

  	const usuarioFinal:Usuario ={};
  	usuarioFinal.correo = user.email;
  	usuarioFinal.nombre = user.nombre;
  	usuarioFinal.apellidos = user.apellidos;
  	usuarioFinal.password = user.contra;
  	usuarioFinal.region = region;
  	usuarioFinal.direccion = direcciones;



  	if(user.contra == user.confirmeContra){
  		this.api.registrarUsuario(usuarioFinal)
  	.subscribe(response=>{

  		localStorage.clear();
  		localStorage.setItem("email-cliente", user.email);
  		this.router.navigate(["/perfil"]);

  	});

  	}

  }

}
