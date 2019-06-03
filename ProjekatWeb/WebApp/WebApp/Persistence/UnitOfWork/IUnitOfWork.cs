using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using WebApp.Models;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        DayRepository DayRepository { get; set; }
        LineRepository LineRepository { get; set; }
        PricelistRepository PricelistRepository { get; set; }
        PriceOfTicketRepository PriceOfTicketRepository { get; set; }
        StationRepository StationRepository { get; set; }
        TimetableRepository TimetableRepository { get; set; }
        TypeTicketRepository TypeTicketRepository { get; set; }

        int Complete();
    }
}
