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
        IDayRepository DayRepository { get; set; }
        ILineRepository LineRepository { get; set; }
        IPricelistRepository PricelistRepository { get; set; }
        IPriceOfTicketRepository PriceOfTicketRepository { get; set; }
        IStationRepository StationRepository { get; set; }
        ITimetableRepository TimetableRepository { get; set; }
        ITypeTicketRepository TypeTicketRepository { get; set; }
        ITimetableTypeRepository TimetableTypeRepository { get; set; }

        int Complete();
    }
}
