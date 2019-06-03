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
    public class LinesController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public LinesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        // GET: api/Lines
        public IEnumerable<Line> GetLines()
        {
            return unitOfWork.LineRepository.GetAll();
        }

        // GET: api/Lines/5
        [ResponseType(typeof(Line))]
        public IHttpActionResult GetLine(int id)
        {
            Line line = unitOfWork.LineRepository.Get(id);
            if (line == null)
            {
                return NotFound();
            }

            return Ok(line);
        }

        // PUT: api/Lines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLine(int id, Line line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != line.Id)
            {
                return BadRequest();
            }

            

            try
            {
                unitOfWork.LineRepository.Update(line);
                unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineExists(id))
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

        // POST: api/Lines
        [ResponseType(typeof(Line))]
        public IHttpActionResult PostLine(Line line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.LineRepository.Add(line);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = line.Id }, line);
        }

        // DELETE: api/Lines/5
        [ResponseType(typeof(Line))]
        public IHttpActionResult DeleteLine(int id)
        {
            Line line = unitOfWork.LineRepository.Get(id);
            if (line == null)
            {
                return NotFound();
            }

            unitOfWork.LineRepository.Remove(line);
            unitOfWork.Complete();

            return Ok(line);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LineExists(int id)
        {
            return unitOfWork.LineRepository.Find(e => e.Id == id).Count() > 0;
        }
    }
}