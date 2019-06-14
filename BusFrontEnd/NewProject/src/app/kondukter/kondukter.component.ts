import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthHttpService } from '../services/http/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-kondukter',
  templateUrl: './kondukter.component.html',
  styleUrls: ['./kondukter.component.css']
})
export class KondukterComponent implements OnInit {

  private id:string;
  tekst:string;
  constructor(private http: AuthHttpService,public router: Router) { }

  ngOnInit() {
  }
  proveri(id : string,form: NgForm){
    this.http.proveri(this.id).subscribe((tekstt)=>{
      this.tekst = tekstt;
      err => console.log(err);
    });
    //this.http.logIn2(user.username,user.password);
    //form.reset();
    //window.location.reload();
  }

}
