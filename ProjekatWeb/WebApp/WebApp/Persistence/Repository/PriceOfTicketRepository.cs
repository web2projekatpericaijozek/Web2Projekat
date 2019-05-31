using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class PriceOfTicketRepository : Repository<PriceOfTicket, int>, IPriceOfTicketRepository
    {
        public PriceOfTicketRepository(DbContext context) : base(context)
        {
        }
    }
}