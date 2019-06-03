using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    public class PriceOfTicketsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public PriceOfTicketsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/PriceOfTickets
        public IEnumerable<PriceOfTicket> GetPriceOfTicket()
        {
            return unitOfWork.PriceOfTicketRepository.GetAll();
        }

        // GET: api/PriceOfTickets/5
        [ResponseType(typeof(PriceOfTicket))]
        public IHttpActionResult GetPriceOfTicket(int id)
        {
            PriceOfTicket priceOfTicket = unitOfWork.PriceOfTicketRepository.Get(id);
            if (priceOfTicket == null)
            {
                return NotFound();
            }

            return Ok(priceOfTicket);
        }

        // PUT: api/PriceOfTickets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPriceOfTicket(int id, PriceOfTicket priceOfTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priceOfTicket.Id)
            {
                return BadRequest();
            }

            

            try
            {
                unitOfWork.PriceOfTicketRepository.Update(priceOfTicket);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceOfTicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PriceOfTickets
        [ResponseType(typeof(PriceOfTicket))]
        public IHttpActionResult PostPriceOfTicket(PriceOfTicket priceOfTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.PriceOfTicketRepository.Add(priceOfTicket);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = priceOfTicket.Id }, priceOfTicket);
        }

        // DELETE: api/PriceOfTickets/5
        [ResponseType(typeof(PriceOfTicket))]
        public IHttpActionResult DeletePriceOfTicket(int id)
        {
            PriceOfTicket priceOfTicket = unitOfWork.PriceOfTicketRepository.Get(id);
            if (priceOfTicket == null)
            {
                return NotFound();
            }

            unitOfWork.PriceOfTicketRepository.Remove(priceOfTicket);
            unitOfWork.Complete();

            return Ok(priceOfTicket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PriceOfTicketExists(int id)
        {
            return unitOfWork.PriceOfTicketRepository.Find(e => e.Id == id).Count() > 0;
        }
    }
}