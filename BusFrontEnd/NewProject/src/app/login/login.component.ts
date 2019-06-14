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
    let jwtData = localStorage.jwt.split('.')[1]
    //let decodedJwtJsonData = window.atob(jwtData)
    //let decodedJwtData = JSON.parse(decodedJwtJsonData)
    //this.user = decodedJwtData.nameid;
    if(jwtData == undefined)
    {
      alert("Neuspesno logovanje!");
      
    }
    else
    {
      alert("Uspesno ste se ulogovali!");
      this.router.navigate(["/home"]);
    }
    
    
  }

  

}
