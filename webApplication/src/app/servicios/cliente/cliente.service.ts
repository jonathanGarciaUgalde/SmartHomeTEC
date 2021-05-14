import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http"

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
}
