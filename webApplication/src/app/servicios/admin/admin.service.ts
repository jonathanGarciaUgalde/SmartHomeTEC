import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http"
import {Observable} from "rxjs/internal/Observable";
import {Dispositivo} from 'src/app/interfaces/admin/Dispositivo';
import {NgForm} from "@angular/forms";


@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http:HttpClient) { }

  public dispositivoActual: Dispositivo = {
  	numeroSerie: null,
	marca: null,
	consumoElectrico: null,
	cedulaJuridica: null,
	enVenta: null,
	tipo: null,
	descripcion: null,
	tiempoGarantia: null
  }

  verificarCredenciales(email:string, password:string){
  	const url = "https://localhost:44318/api/Admin/Login"
  	return this.http.post(url,
 		{
 			Correo: email,
 			Password: password

 		});
  }

  obtenerDispositivos(){
  	const url = "https://localhost:44318/api/Dispositivo/GetDispositivoStock";
  	return this.http.get(url);
  }


  insertarDispositivo(dispositivo:NgForm){
 	const url_api = "https://localhost:44381/insertdispositivo";
 	return this.http.post(url_api,
 		{
 			/*Nombre: platillo.value.Nombre,
 			Precio: platillo.value.Precio,
 			Calorias: platillo.value.Calorias,
 			Tipo: platillo.value.Tipo,
 			Descripcion: platillo.value.Descripcion,
 			TiempoPreparacion: platillo.value.TiempoPreparacion*/
 		});
 }

 actualizarDispositivo(dispositivo : NgForm){
 	const url_api = "https://localhost:44381/actualizarPlatillo";
 	return this.http.put(url_api,
 		{
 			
 		});
 }

 eliminarDispositivo(dispositivo: Dispositivo){
 	const url_api = "https://localhost:44381/eliminarPlatillo";
 	return this.http.put(url_api,
 		{
 			
 		});
 }
}
