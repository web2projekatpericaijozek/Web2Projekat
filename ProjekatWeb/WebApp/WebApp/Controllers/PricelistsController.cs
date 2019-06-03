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
    public class PricelistsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public PricelistsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Pricelists
        public IEnumerable<Pricelist> GetPricelist()
        {
            return unitOfWork.PricelistRepository.GetAll();
        }

        // GET: api/Pricelists/5
        [ResponseType(typeof(Pricelist))]
        public IHttpActionResult GetPricelist(int id)
        {
            Pricelist pricelist = unitOfWork.PricelistRepository.Get(id);
            if (pricelist == null)
            {
                return NotFound();
            }

            return Ok(pricelist);
        }

        // PUT: api/Pricelists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPricelist(int id, Pricelist pricelist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pricelist.Id)
            {
                return BadRequest();
            }

            

            try
            {
                unitOfWork.PricelistRepository.Update(pricelist);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PricelistExists(id))
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

        // POST: api/Pricelists
        [ResponseType(typeof(Pricelist))]
        public IHttpActionResult PostPricelist(Pricelist pricelist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.PricelistRepository.Add(pricelist);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = pricelist.Id }, pricelist);
        }

        // DELETE: api/Pricelists/5
        [ResponseType(typeof(Pricelist))]
        public IHttpActionResult DeletePricelist(int id)
        {
            Pricelist pricelist = unitOfWork.PricelistRepository.Get(id);
            if (pricelist == null)
            {
                return NotFound();
            }

            unitOfWork.PricelistRepository.Remove(pricelist);
            unitOfWork.Complete();

            return Ok(pricelist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PricelistExists(int id)
        {
            return unitOfWork.PricelistRepository.Find(e => e.Id == id).Count() > 0;
        }
    }
}