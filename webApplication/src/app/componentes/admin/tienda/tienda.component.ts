import { Component, OnInit } from '@angular/core';




import * as XLSX from 'xlsx';



@Component({
  selector: 'app-tienda',
  templateUrl: './tienda.component.html',
  styleUrls: ['./tienda.component.css']
})
export class TiendaComponent implements OnInit {

  constructor() { }


  
  ngOnInit(): void {


  }

  onFileChange(evt:any){
  	const target:DataTransfer = <DataTransfer> (evt.target);

  	const reader:FileReader = new FileReader();

  	reader.onload = (e :any)=>{

  		const bstr:string  = e.target.result;
  		const wb : XLSX.WorkBook = XLSX.read(bstr,{type:'binary'});
  		const excelInfo =  XLSX.utils.sheet_to_json(wb.Sheets[wb.SheetNames[0]],{header:1});
  		console.log(excelInfo);

  	}

  	reader.readAsBinaryString(target.files[0]);
  }




}
