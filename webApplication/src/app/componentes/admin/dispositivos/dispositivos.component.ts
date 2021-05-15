import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {AdminService} from 'src/app/servicios/admin/admin.service';
import {Dispositivo} from 'src/app/interfaces/admin/Dispositivo';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';


import {NgForm} from "@angular/forms";


@Component({
  selector: 'app-dispositivos',
  templateUrl: './dispositivos.component.html',
  styleUrls: ['./dispositivos.component.css']
})
export class DispositivosComponent implements OnInit {

  constructor(private router:Router, public api:AdminService, private modalService: NgbModal) { }

  public dispositivos:Dispositivo[];

  title = 'appBootstrap';

  public actualizar:boolean;


  closeResult: string;

  ngOnInit(): void {
  	if(localStorage.getItem("email-admin") != null){
  		this.cargarDispositivos();
  	}
  	else{
  		this.router.navigate(["/admin"]);
  	}
  }

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


  cargarDispositivos(){
  	this.api.obtenerDispositivos()
  	.subscribe((response:Dispositivo[]) => this.cargarInfo(response));
  }

  cargarInfo(response:Dispositivo[]){
  	this.dispositivos = response;

  }

  preActualizar(content, dispositvo: Dispositivo): void{
    this.api.dispositivoActual = Object.assign({},dispositvo);
    this.actualizar = true; 
    this.open(content);
  }

  eliminar(dispositvo: Dispositivo):void{
    if (confirm('¿Está seguro que quiere eliminar el dispositvo?')){
      this.api.eliminarDispositivo(dispositvo)
      .subscribe(response=>location.reload());
    }    
  }

  crearNuevo(content){
    this.limpiarForm();
    this.actualizar = false;

    this.open(content);
  }

  limpiarForm():void{
    this.api.dispositivoActual.numeroSerie = null,
    this.api.dispositivoActual. marca= null,
    this.api.dispositivoActual.consumoElectrico= null,
    this.api.dispositivoActual.cedulaJuridica= null,
    this.api.dispositivoActual.enVenta= null,
    this.api.dispositivoActual.tipo= null,
    this.api.dispositivoActual.descripcion= null,
    this.api.dispositivoActual.tiempoGarantia= null


  }


  guardarDispositivo(dispositivoForm: NgForm):void{

    this.modalService.dismissAll();

    if(this.actualizar){
      this.api.actualizarDispositivo(dispositivoForm).subscribe(response=>location.reload());
    }else{
      this.api.insertarDispositivo(dispositivoForm).subscribe(response=>location.reload());
    }

  }


}
