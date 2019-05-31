using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Timetable
    {
        public int Id { get; set; }

        public string Info { get; set; }

        [ForeignKey("Line")]
        public int LineId { get; set; }

        public Line Line { get; set; }
        [ForeignKey("Day")]
        public int DayId { get; set; }

        public Day Day { get; set; }
    }
}