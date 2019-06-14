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
    [RoutePrefix("api/Stations")]
    public class StationsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public StationsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Stations
        public IEnumerable<Station> GetStation()
        {
            return unitOfWork.StationRepository.GetAll();
        }

        // GET: api/Stations/5
        [ResponseType(typeof(Station))]
        public IHttpActionResult GetStation(int id)
        {
            Station station = unitOfWork.StationRepository.Get(id);
            if (station == null)
            {
                return NotFound();
            }

            return Ok(station);
        }

        [AllowAnonymous]
        [ResponseType(typeof(string))]
        [Route("GetDodajLiniju/{id}/{brLinije}")]
        public IHttpActionResult DodajLiniju(int id, int brLinije)
        {
            Line l = new Line();
            l.Id = id;
            l.Number = brLinije;
            l.StationId = 2;
            unitOfWork.LineRepository.Add(l);

            return Ok("super");
        }

        // PUT: api/Stations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStation(int id, Station station)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != station.Id)
            {
                return BadRequest();
            }

            

            try
            {
                unitOfWork.StationRepository.Update(station);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(id))
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

        // POST: api/Stations
        [ResponseType(typeof(Station))]
        public IHttpActionResult PostStation(Station station)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.StationRepository.Add(station);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = station.Id }, station);
        }

        // DELETE: api/Stations/5
        [ResponseType(typeof(Station))]
        public IHttpActionResult DeleteStation(int id)
        {
            Station station = unitOfWork.StationRepository.Get(id);
            if (station == null)
            {
                return NotFound();
            }

            unitOfWork.StationRepository.Remove(station);
            unitOfWork.Complete();

            return Ok(station);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StationExists(int id)
        {
            return unitOfWork.LineRepository.Find(e => e.Id == id).Count() > 0;
        }
    }
}