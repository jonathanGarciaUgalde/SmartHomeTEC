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
 	const url_api = "https://localhost:44318/api/Dispositivo/SetDispositivoStock";
 	return this.http.post(url_api,
 		{
 			   numeroSerie: dispositivo.value.numero,
        marca: dispositivo.value.marca,
        consumoElectrico: dispositivo.value.consumo,
        cedulaJuridica: dispositivo.value.cedula,
        tipo: dispositivo.value.tipo,
        descripcion: dispositivo.value.descripcion,
        tiempoGarantia: dispositivo.value.garantia,
        enVenta:true
 		});
 }

   cargarDispositivo(dispositivo:Dispositivo){
   const url_api = "https://localhost:44318/api/Dispositivo/SetDispositivoStock";
   return this.http.post(url_api,
     {
        numeroSerie:dispositivo.numeroSerie,
        marca:dispositivo.marca,
        consumoElectrico:dispositivo.consumoElectrico,
        cedulaJuridica:dispositivo.cedulaJuridica,
        tipo:dispositivo.tipo,
        descripcion:dispositivo.descripcion,
        tiempoGarantia:dispositivo.tiempoGarantia,
        enVenta:true
     });
 }

 actualizarDispositivo(dispositivo : NgForm){
 	const url_api = "https://localhost:44318/api/Dispositivo/UpdateDispositivoStock";
 	return this.http.post(url_api,
 		{
        numeroSerie: dispositivo.value.numero,
        marca: dispositivo.value.marca,
        consumoElectrico: dispositivo.value.consumo,
        cedulaJuridica: dispositivo.value.cedula,
        tipo: dispositivo.value.tipo,
        descripcion: dispositivo.value.descripcion,
        tiempoGarantia: dispositivo.value.garantia 		   	
 		});
 }

 eliminarDispositivo(dispositivo: Dispositivo){
 	const url_api = "https://localhost:44318/api/Dispositivo/DeleteDispStock/" + dispositivo.numeroSerie;
 	return this.http.delete(url_api);
 }
}
