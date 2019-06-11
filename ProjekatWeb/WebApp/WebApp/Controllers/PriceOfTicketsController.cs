using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    [Authorize]
    [RoutePrefix("api/PriceOfTickets")]
    public class PriceOfTicketsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ApplicationDbContext db = new ApplicationDbContext();

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
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [ResponseType(typeof(string))]
        [Route("GetKartaKupi2/{tipKarte}/{tipKorisnika}/{user}")]
        public IHttpActionResult GetKarta(string tipKarte, string tipKorisnika, string user)
        {

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var id = User.Identity.GetUserId();
            ApplicationUser u = new ApplicationUser();
            if (user != "")
            {
                 u = userManager.FindById(user);
            }
            


            List<PriceOfTicket> karte = unitOfWork.PriceOfTicketRepository.GetAll().ToList();
            PriceOfTicket price = new PriceOfTicket();
            double cena = 0;
            string retVal = "";
            foreach (PriceOfTicket k in karte)
            {
                k.TypeTicket = unitOfWork.TypeTicketRepository.Get(k.TypeTicketId);
                if (k.TypeTicket.Type == tipKarte)
                {
                    price = unitOfWork.PriceOfTicketRepository.Get(k.Id);
                    if(tipKorisnika == "student")
                        cena = price.Price * 0.8;
                    else if (tipKorisnika == "penzioner")
                        cena = price.Price * 0.5;
                    else 
                        cena = price.Price;

                    Ticket t = new Ticket();
                    t.CenaKarte = k;
                    t.PriceOfTicketId = k.Id;
                    t.Tip = k.TypeTicket.Type;
                    if(u == null)
                    {
                        t.VaziDo = DateTime.UtcNow;
                        unitOfWork.TicketRepository.Add(t);
                        unitOfWork.Complete();
                    }
                    else
                    {
                        t.ApplicationUserId = u.Id;
                        //t.ApplicationUser = u;
                        t.VaziDo = DateTime.UtcNow;
                        u.Tickets.Add(t);
                        unitOfWork.TicketRepository.Add(t);
                        unitOfWork.Complete();
                    }
                    

                    //t.PriceOfTicket = price;
                    //u.Tickets.Add(k);
                }

                 
            }
            retVal += "Uspesno ste kupili " + tipKarte + ", po ceni od : " + cena.ToString() + " din";
            if (karte == null)
            {
                return NotFound();
            }

            return Ok(retVal);
        }


        [AllowAnonymous]
        [ResponseType(typeof(string))]
        [Route("GetProveriKartu/{id}")]
        public IHttpActionResult GetProveri(string id)
        {

            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            string retVal = "istekla";

            int i = Int32.Parse(id);
            string day;
            string year;
            string month;
       

            Ticket karta = unitOfWork.TicketRepository.Get(i);

            string[] lines = karta.VaziDo.ToString().Split(' ', '/');
            day = lines[1];
            month = lines[0];
            year = lines[2];

            if(karta.Tip == "Vremenska")
            {
                var dateOne = DateTime.Now;
                
                var diff = dateOne.Subtract(karta.VaziDo);
                var res = String.Format("{0}:{1}:{2}", diff.Hours, diff.Minutes, diff.Seconds);
                if(diff.Hours == 0)
                {
                    retVal = "istekla";
                }
            }
            else if(karta.Tip == "Dnevna")
            {
                DateTime dateTime = DateTime.Now;
                string day1;
                string month1;
                string year1;

                string[] lines1 = dateTime.ToString().Split(' ', '/');

                day1 = lines1[1];
                month1 = lines1[0];
                year1 = lines1[2];

                if(day.Equals(day1) && month.Equals(month1) && year.Equals(year1))
                {
                    retVal = "Karta je validna";
                }
            }
            else if (karta.Tip == "Mesecna")
            {
                DateTime dateTime = DateTime.Now;
                string day1;
                string month1;
                string year1;

                string[] lines1 = dateTime.ToString().Split(' ', '/');

                day1 = lines1[1];
                month1 = lines1[0];
                year1 = lines1[2];

                if (month.Equals(month1) && year.Equals(year1))
                {
                    retVal = "Karta je validna";
                }
            }
            else if (karta.Tip == "Godisnja")
            {
                DateTime dateTime = DateTime.Now;
                string day1;
                string month1;
                string year1;

                string[] lines1 = dateTime.ToString().Split(' ', '/');

                day1 = lines1[1];
                month1 = lines1[0];
                year1 = lines1[2];

                if (year.Equals(year1))
                {
                    retVal = "Karta je validna";
                }
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