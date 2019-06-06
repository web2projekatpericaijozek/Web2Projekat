import { Component, OnInit } from '@angular/core';
import { TimetableInfo, Day, Line, Timetable, TimetableType } from '../model';
import { RedVoznjeHttpService } from '../services/redvoznje.service';

@Component({
  selector: 'app-redvoznje',
  templateUrl: './redvoznje.component.html',
  styleUrls: ['./redvoznje.component.css']
})
export class RedvoznjeComponent implements OnInit {

  timetableInfo:TimetableInfo = new TimetableInfo();
  selectedTimetableType: TimetableType = new TimetableType();
  selectedDay: Day = new Day();
  selectedLine: Line = new Line();
  filteredLines: Line[] = [];
  timetable: Timetable = new Timetable();

  constructor(private http: RedVoznjeHttpService) { }

  ngOnInit() {
    this.http.getAll().subscribe((timetableInfo) => {
      this.timetableInfo = timetableInfo;
      err => console.log(err);
    });
  }

  changeselectedLine(){
    this.filteredLines.splice(0);
    this.timetableInfo.Lines.forEach(element => {
      if(element.Number == this.selectedLine.Number){
        this.filteredLines.push(element);
      }
    });
  }

  ispisPolaska(){
    this.http.getSelected(this.selectedTimetableType.Id, this.selectedDay.Id,this.selectedLine.Id).subscribe((data)=>{
      this.timetable.Info = data;
      console.log(this.timetable);
      err => console.log(err);
    });
  }


}
