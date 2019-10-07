namespace CinemAPI.Controllers
{
    using CinemAPI.Data;
    using CinemAPI.Domain.Contracts;
    using CinemAPI.Domain.Contracts.Models;
    using CinemAPI.Domain.Contracts.Models.Projection;
    using CinemAPI.Models;
    using CinemAPI.Models.Contracts.Projection;
    using CinemAPI.Models.Input.Projection;

    using System;
    using System.Web.Http;

    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IAvailableSeats availableSeats;

        public ProjectionController(INewProjection newProj, IAvailableSeats availableSeats)
        {
            this.newProj = newProj ?? throw new ArgumentNullException();
            this.availableSeats = availableSeats ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IHttpActionResult Index(ProjectionCreationModel model)
        {
            NewProjectionSummary summary = newProj.New(new Projection(model.MovieId, model.RoomId, model.StartDate, model.AvailableSeatsCount));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
        
        [HttpPost]
        public IHttpActionResult GetAvailableSeatsForProjection(GetAvailableSeatsForProjectionModel model)
        {
            AvailableSeatsSummary summary = availableSeats.AvailableSeatsCheckById(model.ProjectionId);

            if (summary.SeatsAreAvailable)
            {
                return Ok(summary.AvailableSeats);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}