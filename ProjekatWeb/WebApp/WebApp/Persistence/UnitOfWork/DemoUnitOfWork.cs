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



        [Dependency]
        public IDayRepository DayRepository { get; set; }
        [Dependency]
        public ILineRepository LineRepository { get; set; }
        [Dependency]
        public IPricelistRepository PricelistRepository { get; set; }
        [Dependency]
        public IPriceOfTicketRepository PriceOfTicketRepository { get; set; }
        [Dependency]
        public IStationRepository StationRepository { get; set; }
        [Dependency]
        public ITimetableRepository TimetableRepository { get; set; }
        [Dependency]
        public ITypeTicketRepository TypeTicketRepository { get; set; }

        [Dependency]
        public ITimetableTypeRepository TimetableTypeRepository { get; set; }
        [Dependency]
        public ITicketRepository TicketRepository { get; set; }

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