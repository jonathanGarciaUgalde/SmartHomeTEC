import { Component, OnInit } from '@angular/core';

import {ClienteService} from 'src/app/servicios/cliente/cliente.service';

import {Usuario} from "src/app/interfaces/cliente/usuario";

import {Router} from '@angular/router';

import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';

import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.css']
})
export class PerfilComponent implements OnInit {

  constructor(private router:Router, private api:ClienteService, private modalService: NgbModal) { }


  public datosUsuario:Usuario;
  closeResult: string;
  public direcciones:string = "";


  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = 'Closed with: ${result}';
    }, (reason) => {
      this.closeResult = 'Dismissed ${this.getDismissReason(reason)}';
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }

  
  ngOnInit(): void {
    if(localStorage.getItem("email-cliente") != null){
      this.api.obtenerInformacionUsuario(localStorage.getItem("email-cliente"))
      .subscribe((response:Usuario) => {
         this.mostrarInfo(response);
         });
    }
    else{
      this.router.navigate([""]);
    }  	
  }

  mostrarInfo(response:Usuario){
   this.datosUsuario = response;
   this.datosUsuario.correo = localStorage.getItem('email-cliente');
   this.datosUsuario.password = localStorage.getItem('pass-cliente');
   localStorage.setItem("pais-cliente",this.datosUsuario.region.pais)
   for (var i = 0; i < response.direccion.length; i++) {
       this.direcciones += response.direccion[i].ubicacion + "\n";
   }
  }

 guardarUsuario(usuarioForm: NgForm):void{

    this.modalService.dismissAll();
    this.api.actualizarUsuario(usuarioForm,this.datosUsuario.password).subscribe(response=>location.reload());


  }




}
