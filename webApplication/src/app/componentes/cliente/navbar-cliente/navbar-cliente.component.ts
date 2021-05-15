import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar-cliente',
  templateUrl: './navbar-cliente.component.html',
  styleUrls: ['./navbar-cliente.component.css']
})
export class NavbarClienteComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  salir():void{

  	localStorage.removeItem("email-cliente");
  	localStorage.removeItem("pass-cliente");
    localStorage.removeItem("pais-cliente");

  }

}
