using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public bool Checked { get; set; }
        public string Tip { get; set; }
        public DateTime VaziDo { get; set; } //cekirana u trenutku
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int PriceOfTicketId { get; set; }
        public PriceOfTicket CenaKarte { get; set; }
    }

    public enum TipKarte
    {
        Vremenska,
        Dnevna,
        Mesecna,
        Godisnja
    }
}