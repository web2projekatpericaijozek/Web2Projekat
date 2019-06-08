import { Component, OnInit } from '@angular/core';
import { AuthHttpService } from '../services/http/auth.service';

@Component({
  selector: 'app-karta',
  templateUrl: './karta.component.html',
  styleUrls: ['./karta.component.css']
})
export class KartaComponent implements OnInit {

  constructor(private http: AuthHttpService) { }
  tipovi: string[] = ["Dnevna", "Mesecna", "Godisnja", "Vremenska"];
  tip: string;
  cena1: number;
  vaziDo1 : string;
  user: string;
  
  ngOnInit() {
  }

  CenaKarte(){
    this.http.GetCenaKarte(this.tip).subscribe((cena)=>{
      this.cena1 = cena;
      err => console.log(err);
    });
  }
  KupiKartu(){
     let jwtData = localStorage.jwt.split('.')[1]
        let decodedJwtJsonData = window.atob(jwtData)
        let decodedJwtData = JSON.parse(decodedJwtJsonData)


       
        this.user = decodedJwtData.nameid;
      this.http.GetKupiKartu(this.tip, "student", this.user).subscribe((vaziDo)=>
    {
      this.vaziDo1 = vaziDo;
      err => console.log(err);
      });

  }

}