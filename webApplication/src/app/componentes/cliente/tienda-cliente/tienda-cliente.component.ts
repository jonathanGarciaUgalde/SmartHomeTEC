import { Component, OnInit } from '@angular/core';
import {Dispositivo} from 'src/app/interfaces/admin/Dispositivo';
import {Distribuidor} from 'src/app/interfaces/cliente/distribuidor';
import {ClienteService} from 'src/app/servicios/cliente/cliente.service';
import {Router} from '@angular/router';
import { PdfMakeWrapper, Table, Txt} from 'pdfmake-wrapper';
import { ITable } from 'pdfmake-wrapper/lib/interfaces';
import pdfFonts from "pdfmake/build/vfs_fonts"; // fonts provided for pdfmake


PdfMakeWrapper.setFonts(pdfFonts);


@Component({
	selector: 'app-tienda-cliente',
	templateUrl: './tienda-cliente.component.html',
	styleUrls: ['./tienda-cliente.component.css']
})
export class TiendaClienteComponent implements OnInit {

	constructor(private api:ClienteService, private router:Router) { }


	public dispositivosEnVenta:Dispositivo[] = [];

	public distribuidores:Distribuidor[] = [];

	

	ngOnInit(): void {
		if(localStorage.getItem("email-cliente") != null){

			this.obtenerDispositivos();
		}
		else{
			this.router.navigate([""]);
		}
	}

	obtenerDispositivos(){
		this.api.obtenerDispositivos()
		.subscribe((response:Dispositivo[])=>this.cargarInfo(response));
	}


	cargarInfo(response:Dispositivo[]){

		this.obtenerCedulas(response);

	}

	obtenerCedulas(dispositivos:Dispositivo[]){

		this.api.obtenerDistribuidoresRegion()
		.subscribe((response:Distribuidor[])=>{

			for (var i = 0; i < response.length; i++) {
				for (var a = 0; a < dispositivos.length; a++) {					
					if(response[i].cedulaJuridica == dispositivos[a].cedulaJuridica){
						if(dispositivos[a].enVenta){

							this.dispositivosEnVenta.push(dispositivos[a]);
						}
					}
				}
			}			

		});

	}

	comprarDispositivo(dispositivo:Dispositivo){

		if (confirm('¿Está seguro que quiere comprar el dispositvo?')){

			console.log(dispositivo);

			this.generarPDF();
		}

	}


	generarPDF(){
		const PDF = new PdfMakeWrapper();

		PDF.pageMargins([ 40, 60, 40, 60 ]);
		
		PDF.header({ 
			text: 'SMARTHOMETEC',
			fontSize: 30,
			bold: true,
			marginLeft: 200,
			marginTop: 4

		});

		PDF.pageSize({
			width: 595.28,
			height: 'auto'
		});

		PDF.defaultStyle({
			fontSize: 15});

		const fecha:string = "13/8/09";
		const numero:number = 12312312;

		PDF.add(new Txt("Fecha de Facturación: " + fecha).end);
		PDF.add(new Txt(" " ).end);
		PDF.add(new Txt("Numero de factura: " + numero).end);
		PDF.add(new Txt(" " ).end);

		PDF.add(new Table([

			["Tipo",'','Marca','',"Descripcion",'', "Precio"],
			[30123, '','Tablet','', 'IPAD PRO','', 4000]
			]).layout('lightHorizontalLines'		).end);

		




		PDF.create().open();


	}

	



}
