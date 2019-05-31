using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Line
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int StationId { get; set; }
        public List<Station> Stations { get; set; }
        public List<Timetable> Timetables { get; set; }

        public Line(int i, int n)
        {
            Id = i;
            Number = n;
        }

        
    }
}