import { Component, OnInit } from '@angular/core';

import {Router} from '@angular/router';

@Component({
  selector: 'app-login-admin',
  templateUrl: './login-admin.component.html',
  styleUrls: ['./login-admin.component.css']
})
export class LoginAdminComponent implements OnInit {

  constructor(public router:Router) { }

  ngOnInit(): void {
  }


  verificarCredenciales():void{

  	this.router.navigate(["admin/dispositivos"]);

  }

}
