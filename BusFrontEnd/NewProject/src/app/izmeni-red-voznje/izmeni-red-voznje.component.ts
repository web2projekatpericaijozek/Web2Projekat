import { Component, OnInit } from '@angular/core';
import { TimetableInfo, TimetableType, Day, Line, Timetable } from '../model';

@Component({
  selector: 'app-izmeni-red-voznje',
  templateUrl: './izmeni-red-voznje.component.html',
  styleUrls: ['./izmeni-red-voznje.component.css']
})
export class IzmeniRedVoznjeComponent implements OnInit {

  timetableInfo:TimetableInfo = new TimetableInfo();
  selectedTimetableType: TimetableType = new TimetableType();
  selectedDay: Day = new Day();
  selectedLine: Line = new Line();
  filteredLines: Line[] = [];
  timetable: Timetable = new Timetable();

  constructor() { }

  ngOnInit() {
  }

}
