import { Component, OnInit } from '@angular/core';

import {Router} from '@angular/router';

@Component({
  selector: 'app-login-cliente',
  templateUrl: './login-cliente.component.html',
  styleUrls: ['./login-cliente.component.css']
})
export class LoginClienteComponent implements OnInit {

  constructor(public router:Router) { }

  ngOnInit(): void {
  }


  verificarCredenciales():void {

  	this.router.navigate(["/perfil"]);

  }

}
