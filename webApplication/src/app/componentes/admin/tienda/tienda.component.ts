import { Component, OnInit } from '@angular/core';

import {Dispositivo} from 'src/app/interfaces/admin/Dispositivo';
import {AdminService} from 'src/app/servicios/admin/admin.service';





import * as XLSX from 'xlsx';



@Component({
  selector: 'app-tienda',
  templateUrl: './tienda.component.html',
  styleUrls: ['./tienda.component.css']
})
export class TiendaComponent implements OnInit {

  constructor(public api:AdminService) { }


  
  ngOnInit(): void {


  }

  onFileChange(evt:any){
  	const target:DataTransfer = <DataTransfer> (evt.target);

  	const reader:FileReader = new FileReader();

  	reader.onload = (e :any)=>{

  		const bstr:string  = e.target.result;
  		const wb : XLSX.WorkBook = XLSX.read(bstr,{type:'binary'});
  		const dispositivos:Dispositivo[] =  XLSX.utils.sheet_to_json(wb.Sheets[wb.SheetNames[0]]);
  		
      for (var i = dispositivos.length - 1; i >= 0; i--) {
        this.api.cargarDispositivo(dispositivos[i])
        .subscribe(response=>console.log("cargado"));
      }

  	}

  	reader.readAsBinaryString(target.files[0]);
  }




}
