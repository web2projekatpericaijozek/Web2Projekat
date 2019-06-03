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
    public class DaysController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public DaysController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Days
        public IEnumerable<Day> GetDay(IUnitOfWork unitOfWork)
        {
            return unitOfWork.DayRepository.GetAll();
        }

        // GET: api/Days/5
        [ResponseType(typeof(Day))]
        public IHttpActionResult GetDay(int id)
        {
            Day day = unitOfWork.DayRepository.Get(id);
            if (day == null)
            {
                return NotFound();
            }

            return Ok(day);
        }

        // PUT: api/Days/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDay(int id, Day day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != day.Id)
            {
                return BadRequest();
            }

            

            try
            {
                unitOfWork.DayRepository.Update(day);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
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

        // POST: api/Days
        [ResponseType(typeof(Day))]
        public IHttpActionResult PostDay(Day day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.DayRepository.Add(day);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = day.Id }, day);
        }

        // DELETE: api/Days/5
        [ResponseType(typeof(Day))]
        public IHttpActionResult DeleteDay(int id)
        {
            Day day = unitOfWork.DayRepository.Get(id);
            if (day == null)
            {
                return NotFound();
            }

            unitOfWork.DayRepository.Remove(day);
            unitOfWork.Complete();

            return Ok(day);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DayExists(int id)
        {
            return unitOfWork.DayRepository.Find(e => e.Id == id).Count() > 0;
        }
    }
}