import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {RegionInterface} from "src/app/interfaces/cliente/region";
import {Ubicacion} from  "src/app/interfaces/cliente/ubicacion";

import {Usuario} from  "src/app/interfaces/cliente/usuario";

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

 		console.log(user.direccion);


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
}
