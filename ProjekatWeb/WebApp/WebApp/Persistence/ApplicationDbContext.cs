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
        public DbSet<Day> Day { get; set; }
        public DbSet<Line> Line { get; set; }
        public DbSet<Pricelist> Pricelist { get; set; }
        public DbSet<PriceOfTicket> PriceOfTicket { get; set; }
        public DbSet<Station> Station { get; set; }
        public DbSet<Timetable> Timetable { get; set; }
        public DbSet<TypeTicket> TypeTicket { get; set; }

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