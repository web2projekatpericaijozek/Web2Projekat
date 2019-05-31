using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Day> Days { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Pricelist> Pricelists { get; set; }
        public DbSet<PriceOfTicket> PriceOfTickets { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<TypeTicket> TypeTickets { get; set; }

        public ApplicationDbContext()
            : base("name=DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}