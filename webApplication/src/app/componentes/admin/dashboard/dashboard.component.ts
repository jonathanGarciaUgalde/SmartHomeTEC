import { Component, OnInit } from '@angular/core';
import {AdminService} from 'src/app/servicios/admin/admin.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private router:Router, private api: AdminService) { }

  ngOnInit(): void {
  	if(localStorage.getItem("email-admin") == null){
  		this.router.navigate(["/admin"]);
  	}
  }

}
