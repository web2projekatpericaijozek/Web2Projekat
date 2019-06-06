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
    
    [RoutePrefix("api/Timetables")]
    public class TimetablesController : ApiController
    {
        private IUnitOfWork unitOfWork;

        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public TimetablesController() { }
        public TimetablesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("IspisReda/{timetableTypeId}/{dayId}/{lineId}")]
        [HttpGet]
        public IHttpActionResult GetTimetableTimes(int timetableTypeId, int dayId, int lineId) //vraca vremena polaska autobusa iz reda voznji
        {
            Timetable t = new Timetable();
            t = unitOfWork.TimetableRepository.Find(x => x.TimetableTypeId == timetableTypeId && x.DayId == dayId && x.LineId == lineId).FirstOrDefault();

            return Ok(t.Info);
        }

        [ResponseType(typeof(TimetableBindingModel))]
        [Route("RedVoznjiInfo")]
        public IHttpActionResult GetScheduleInfo()
        {
            List<TimetableType> timetableTypes = unitOfWork.TimetableTypeRepository.GetAll().ToList();
            List<Day> days = unitOfWork.DayRepository.GetAll().ToList();
            List<Line> lines = unitOfWork.LineRepository.GetAll().ToList();
            
            TimetableBindingModel s = new TimetableBindingModel() { TimetableTypes = timetableTypes, Lines = lines, Days = days };

            return Ok(s);
        }

        // GET: api/Timetables
        public IEnumerable<Timetable> GetTimetable()
        {
            return unitOfWork.TimetableRepository.GetAll();
        }

        // GET: api/Timetables/5
        [ResponseType(typeof(Timetable))]
        public IHttpActionResult GetTimetable(int id)
        {
            Timetable timetable = unitOfWork.TimetableRepository.Get(id);
            if (timetable == null)
            {
                return NotFound();
            }

            return Ok(timetable);
        }

        // PUT: api/Timetables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTimetable(int id, Timetable timetable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timetable.Id)
            {
                return BadRequest();
            }

            

            try
            {
                unitOfWork.TimetableRepository.Update(timetable);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimetableExists(id))
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

        // POST: api/Timetables
        [ResponseType(typeof(Timetable))]
        public IHttpActionResult PostTimetable(Timetable timetable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.TimetableRepository.Add(timetable);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = timetable.Id }, timetable);
        }

        // DELETE: api/Timetables/5
        [ResponseType(typeof(Timetable))]
        public IHttpActionResult DeleteTimetable(int id)
        {
            Timetable timetable = unitOfWork.TimetableRepository.Get(id);
            if (timetable == null)
            {
                return NotFound();
            }

            unitOfWork.TimetableRepository.Remove(timetable);
            unitOfWork.Complete();

            return Ok(timetable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimetableExists(int id)
        {
            return unitOfWork.TimetableRepository.GetAll().Count(e => e.Id == id) > 0;
        }
    }
}