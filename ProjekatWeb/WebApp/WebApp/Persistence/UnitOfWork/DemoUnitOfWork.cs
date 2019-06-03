using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Models;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
      
        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        //[Dependency]
        //public DayRepository Day { get; set; }

        //[Dependency]
        //public ILineRepository Line { get; set; }

        //[Dependency]
        //public IPricelistRepository Pricelist { get; set; }

        //[Dependency]
        //public IPriceOfTicketRepository PriceOfTicket { get; set; }

        //[Dependency]
        //public IStationRepository Station { get; set; }

        //[Dependency]
        //public ITimetableRepository Timetable { get; set; }

        //[Dependency]
        //public ITypeTicketRepository TypeTicket { get; set; }

        [Dependency]
        public DayRepository DayRepository { get; set; }
        [Dependency]
        public LineRepository LineRepository { get; set; }
        [Dependency]
        public PricelistRepository PricelistRepository { get; set; }
        [Dependency]
        public PriceOfTicketRepository PriceOfTicketRepository { get; set; }
        [Dependency]
        public StationRepository StationRepository { get; set; }
        [Dependency]
        public TimetableRepository TimetableRepository { get; set; }
        [Dependency]
        public TypeTicketRepository TypeTicketRepository { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}