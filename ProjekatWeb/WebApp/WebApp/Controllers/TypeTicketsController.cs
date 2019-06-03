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
    public class TypeTicketsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public TypeTicketsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/TypeTickets
        public IEnumerable<TypeTicket> GetTypeTicket()
        {
            return unitOfWork.TypeTicketRepository.GetAll();
        }

        // GET: api/TypeTickets/5
        [ResponseType(typeof(TypeTicket))]
        public IHttpActionResult GetTypeTicket(int id)
        {
            TypeTicket typeTicket = unitOfWork.TypeTicketRepository.Get(id);
            if (typeTicket == null)
            {
                return NotFound();
            }

            return Ok(typeTicket);
        }

        // PUT: api/TypeTickets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTypeTicket(int id, TypeTicket typeTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeTicket.Id)
            {
                return BadRequest();
            }

            

            try
            {
                unitOfWork.TypeTicketRepository.Update(typeTicket);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeTicketExists(id))
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

        // POST: api/TypeTickets
        [ResponseType(typeof(TypeTicket))]
        public IHttpActionResult PostTypeTicket(TypeTicket typeTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.TypeTicketRepository.Add(typeTicket);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = typeTicket.Id }, typeTicket);
        }

        // DELETE: api/TypeTickets/5
        [ResponseType(typeof(TypeTicket))]
        public IHttpActionResult DeleteTypeTicket(int id)
        {
            TypeTicket typeTicket = unitOfWork.TypeTicketRepository.Get(id);
            if (typeTicket == null)
            {
                return NotFound();
            }

            unitOfWork.TypeTicketRepository.Remove(typeTicket);
            unitOfWork.Complete();

            return Ok(typeTicket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeTicketExists(int id)
        {
            return unitOfWork.TypeTicketRepository.Find(e => e.Id == id).Count() > 0;
        }
    }
}