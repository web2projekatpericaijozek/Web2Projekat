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
        public List<Pricelist> Days { get; set; }

        [Dependency]
        public List<Pricelist> Lines { get; set; }

        [Dependency]
        public List<Pricelist> Pricelists { get; set; }

        [Dependency]
        public List<Pricelist> PriceOfTickets { get; set; }

        [Dependency]
        public List<Pricelist> Stations { get; set; }

        [Dependency]
        public List<Pricelist> Timetables { get; set; }

        [Dependency]
        public List<Pricelist> TypeTickets { get; set; }

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