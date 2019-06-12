﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Pricelist
    {
        public int Id { get; set; }
        public DateTime From{ get; set; }
        public DateTime To { get; set; }

        public List<PriceOfTicket> PriceOfTickets { get; set; }

        
    }
}