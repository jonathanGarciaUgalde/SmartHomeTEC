import { Component, OnInit } from '@angular/core';
import {AdminService} from 'src/app/servicios/admin/admin.service';
import {Router} from '@angular/router';

import {Dispositivo} from 'src/app/interfaces/admin/Dispositivo';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private router:Router, private api: AdminService) { }

  public dispositivos:Dispositivo[];

  ngOnInit(): void {
  	if(localStorage.getItem("email-admin") == null){
  		this.router.navigate(["/admin"]);
  	}
  	else{
  		this.cargarDispositivos();
  	}
  }

  cargarDispositivos(){
  	this.api.obtenerDispositivos()
  	.subscribe((response:Dispositivo[]) => this.cargarInfo(response));
  }

  cargarInfo(response:Dispositivo[]){
  	this.dispositivos = response;
  }

}
