namespace CinemAPI.Controllers
{
    using CinemAPI.Data;
    using CinemAPI.Models;
    using CinemAPI.Models.Contracts.Movie;
    using CinemAPI.Models.Input.Movie;

    using System;
    using System.Web.Http;

    public class MovieController : ApiController
    {
        private readonly IMovieRepository movieRepo;

        public MovieController(IMovieRepository movieRepo)
        {
            this.movieRepo = movieRepo ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IHttpActionResult Index(MovieCreationModel model)
        {
            IMovie movie = movieRepo.GetByNameAndDuration(model.Name, model.DurationMinutes);

            if (movie == null)
            {
                movieRepo.Insert(new Movie(model.Name, model.DurationMinutes));

                return Ok("Successfuly created new movie.");
            }

            return BadRequest("Movie already exists");
        }
    }
}