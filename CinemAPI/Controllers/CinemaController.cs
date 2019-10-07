namespace CinemAPI.Controllers
{
    using CinemAPI.Data;
    using CinemAPI.Models;
    using CinemAPI.Models.Contracts.Cinema;
    using CinemAPI.Models.Input.Cinema;

    using System;
    using System.Web.Http;

    public class CinemaController : ApiController
    {
        private readonly ICinemaRepository cinemaRepo;

        public CinemaController(ICinemaRepository cinemaRepo)
        {
            this.cinemaRepo = cinemaRepo ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IHttpActionResult Index(CinemaCreationModel model)
        {
            ICinema cinema = cinemaRepo.GetByNameAndAddress(model.Name, model.Address);

            if (cinema == null)
            {
                cinemaRepo.Insert(new Cinema(model.Name, model.Address));

                return Ok("Successfuly created new cinema.");
            }

            return BadRequest("Cinema already exists");
        }
    }
}