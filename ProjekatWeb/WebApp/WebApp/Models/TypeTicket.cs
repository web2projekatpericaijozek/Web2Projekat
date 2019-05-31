using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TypeTicket
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public List<PriceOfTicket> PriceOfTickets { get; set; }
    }
}