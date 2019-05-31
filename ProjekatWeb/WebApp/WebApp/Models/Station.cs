using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public int LineId { get; set; }
        public List<Line> Lines { get; set; }

        public Station(int i,string n, string a, float xx, float yy)
        {
            Id = i;
            Name = n;
            Adress = a;
            X = xx;
            Y = yy;
        }
    }
}