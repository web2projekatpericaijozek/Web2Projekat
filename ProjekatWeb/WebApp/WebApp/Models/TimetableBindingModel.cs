using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TimetableBindingModel
    {
        public List<TimetableType> TimetableTypes { get; set; }
        public List<Line> Lines { get; set; }
        public List<Day> Days { get; set; }
    }
}