import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {RegionInterface} from "src/app/interfaces/cliente/region";
import {Ubicacion} from  "src/app/interfaces/cliente/ubicacion";

import {Usuario} from  "src/app/interfaces/cliente/usuario";

import {NgForm} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

	constructor(private http:HttpClient) { }

  	
  	verificarCredenciales(email:string, password:string){
  	const url = "https://localhost:44318/api/User/Login/" + email + "/" + password;
  	return this.http.post(url,
 		{
 			Correo: email,
 			Password: password

 		});
 	}


 	obtenerInformacionUsuario(email:string){
 		const url = "https://localhost:44318/api/User/Credenciales";
  		return this.http.post(url,
 		{
 			Correo: email

 		});
 	}

 	registrarUsuario(user:Usuario){
 		const url = "https://localhost:44318/api/User/Signin";


 		return this.http.post(url,
 		{
 			Correo: user.correo,
      		Nombre : user.nombre,
      		Apellidos : user.apellidos,
      		Password : user.password,
      		Region :  {
         	Pais : user.region.pais, Continente:user.region.continente},
      		Direccion: user.direccion
            	

 		});  
 	}

  actualizarUsuario(formUsuario: NgForm, password:string){
    const url = "https://localhost:44318/api/User/UpdateUsuario";

    const direcciones= [];

    for(let ubicacion of formUsuario.value.direccion.split("\n")){

      direcciones.push({"Ubicacion": ubicacion});

    }

    console.log(direcciones);
    return this.http.post(url,
    {
      Correo: localStorage.getItem("email-cliente"),
      Nombre: formUsuario.value.nombre,
      Password : formUsuario.value.contrasena,
      Apellidos : formUsuario.value.apellidos,
      Region :{
        Pais: formUsuario.value.pais, Continente:formUsuario.value.continente
      },
      Direccion : direcciones

    });

  }

  obtenerDistribuidoresRegion(){

    const url = "https://localhost:44318/api/GestionRegion/GetDispositivoStock";
    return this.http.post(url,{
      Region :  {
           Pais : localStorage.getItem("pais-cliente")}
    });

  }

  obtenerDispositivos(){
    const url = "https://localhost:44318/api/Dispositivo/GetDispositivoStock";
    return this.http.get(url);
  }

}
