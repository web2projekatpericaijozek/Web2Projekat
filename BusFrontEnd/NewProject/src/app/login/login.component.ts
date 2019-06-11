import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/salo/osoba';
import { AuthHttpService } from 'src/app/services/http/auth.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private http: AuthHttpService,public router: Router) { }

  ngOnInit() {
  }

  login(user:User,form: NgForm){
    this.http.logIn(user.username, user.password);
    //this.http.logIn2(user.username,user.password);
    this.router.navigate(["/home"]);
    
  }

  

}
