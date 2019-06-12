import { Component, OnInit } from '@angular/core';
import { Line } from '../model';
import { AuthHttpService } from '../services/http/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-linije',
  templateUrl: './linije.component.html',
  styleUrls: ['./linije.component.css']
})
export class LinijeComponent implements OnInit {

  lines:Line[]=[];
  linija: Line;
  linijaOznaka: number;
  user:string;

  constructor(private http:AuthHttpService ) { }

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
    this.http.GetAllLines().subscribe((lines) => {
      this.lines = lines;
      err => console.log(err);
    });
  }

  IzmeniLiniju(tispKarte:number,form: NgForm){
    this.http.GetPromeniLiniju(this.linija.Number, this.linijaOznaka).subscribe();
  }

}
