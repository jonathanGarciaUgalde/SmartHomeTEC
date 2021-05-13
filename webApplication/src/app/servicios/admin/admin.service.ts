import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http"
import {Observable} from "rxjs/internal/Observable";

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http:HttpClient) { }

  verificarCredenciales(email:string, password:string){
  	const url = "https://localhost:44318/api/Admin/Login"
  	return this.http.post(url,
 		{
 			Correo: email,
 			Password: password

 		});
  }
}
