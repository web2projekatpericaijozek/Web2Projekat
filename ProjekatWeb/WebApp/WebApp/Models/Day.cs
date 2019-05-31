using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Day
    {
        public int Id { get; set; }

        public string DayOfWeek { get; set; }
        public List<Timetable> Timetables { get; set; }
    }
}