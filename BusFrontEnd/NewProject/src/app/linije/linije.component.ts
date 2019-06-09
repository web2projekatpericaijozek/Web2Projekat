import { Component, OnInit } from '@angular/core';
import { Line } from '../model';
import { AuthHttpService } from '../services/http/auth.service';

@Component({
  selector: 'app-linije',
  templateUrl: './linije.component.html',
  styleUrls: ['./linije.component.css']
})
export class LinijeComponent implements OnInit {

  lines:Line[]=[];

  constructor(private http:AuthHttpService ) { }

  ngOnInit() {
    this.http.GetAllLines().subscribe((lines) => {
      this.lines = lines;
      err => console.log(err);
    });
  }

}
