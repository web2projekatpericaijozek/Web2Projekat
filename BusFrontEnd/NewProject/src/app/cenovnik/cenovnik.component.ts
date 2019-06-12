import { Component, OnInit } from '@angular/core';
import { AuthHttpService } from '../services/http/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-cenovnik',
  templateUrl: './cenovnik.component.html',
  styleUrls: ['./cenovnik.component.css']
})
export class CenovnikComponent implements OnInit {

  tipovi: string[] = ["Dnevna", "Mesecna", "Godisnja", "Vremenska"];
  korisnici: string[] = ["student", "penzioner", "regularni putnik"];
  korisnik: string;
  tip: string;
  cena1: number;
  user:string;
  novaCena: number;
  tipKarte:string;
  id:number;
  constructor(private http: AuthHttpService) { }

  ngOnInit() {
    let jwtData = localStorage.jwt.split('.')[1]
        //let decodedJwtJsonData = window.atob(jwtData)
        //let decodedJwtData = JSON.parse(decodedJwtJsonData)
        //this.user = decodedJwtData.nameid;
        if(jwtData == undefined)
        {
          this.user = "neregistrovan";
        }
        else
        {
          let decodedJwtJsonData = window.atob(jwtData)
          let decodedJwtData = JSON.parse(decodedJwtJsonData)
          this.user = decodedJwtData.nameid;
        }
  }

  CenaKarte(){
    this.http.GetCenaKarte2(this.tip,this.korisnik).subscribe((cena)=>{
      this.cena1 = cena;
      err => console.log(err);
    });
  }

  NovaCenaKarte(tispKarte:string,novaCena:number,form: NgForm){
    this.http.GetPromeniCenu(this.tipKarte, this.novaCena).subscribe();
    //this.http.logIn2(user.username,user.password);
    
    
  }

  Dodaj()
  {
    this.http.GetDodajCenovnik(this.tipKarte,this.novaCena).subscribe();
  }
  Obrisi()
  {
    this.http.GetObrisiCenovnik(this.id).subscribe();
  }

}
