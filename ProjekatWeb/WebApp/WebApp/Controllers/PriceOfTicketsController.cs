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
    [RoutePrefix("api/PriceOfTickets")]
    public class PriceOfTicketsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public PriceOfTicketsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [ResponseType(typeof(float))]
        [Route("GetKarta/{tip}")]
        public IHttpActionResult GetKartaCena(string tip)
        {
            List<TypeTicket> karte = unitOfWork.TypeTicketRepository.GetAll().ToList();
            PriceOfTicket price = new PriceOfTicket();
            float cena = 0;
            foreach (TypeTicket k in karte)
            {
                if (tip == k.Type)
                {
                    price = unitOfWork.PriceOfTicketRepository.Get(k.Id);
                    cena = price.Price;
                }
            }

            if (karte == null)
            {
                return NotFound();
            }

            return Ok(cena);
        }

        [AllowAnonymous]
        [ResponseType(typeof(string))]
        [Route("GetKartaKupi2/{tipKarte}/{tipKorisnika}/{user}")]
        public IHttpActionResult GetKarta(string tipKarte, string tipKorisnika, string user)
        {
            List<TypeTicket> karte = unitOfWork.TypeTicketRepository.GetAll().ToList();
            PriceOfTicket price = new PriceOfTicket();
            double cena = 0;
            string retVal = "";
            foreach (TypeTicket k in karte)
            {
                if (k.Type == tipKarte)
                {
                    price = unitOfWork.PriceOfTicketRepository.Get(k.Id);
                    if(tipKorisnika == "student")
                        cena = price.Price * 0.8;
                    if (tipKorisnika == "penzioner")
                        cena = price.Price * 0.5;
                    else
                        cena = price.Price;
                }

                 retVal += "Uspesno ste kupili " + tipKarte + ", po ceni od : " + cena.ToString() + " din";
            }

            if (karte == null)
            {
                return NotFound();
            }

            return Ok(retVal);
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