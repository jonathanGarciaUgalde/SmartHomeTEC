import { Component, OnInit } from '@angular/core';
import {Location} from "@angular/common";
import {NgForm} from "@angular/forms";
import {AdminService} from 'src/app/servicios/admin/admin.service';

@Component({
  selector: 'app-modal-dispositivos',
  templateUrl: './modal-dispositivos.component.html',
  styleUrls: ['./modal-dispositivos.component.css']
})
export class ModalDispositivosComponent implements OnInit {

  constructor(public api:AdminService) { }

  ngOnInit(): void {
  }

   guardarDispositivo(dispositivoForm: NgForm):void{
  	if(dispositivoForm.value.Codigo == null){
  		//this.api.insertarPlato(dispositivoForm).subscribe(response=>location.reload());
  	}else{
  		//this.api.actualizarPlato(dispositivoForm).subscribe(response=>location.reload());
  	}

  }

}
