namespace CinemAPI.Controllers
{
    using CinemAPI.Data;
    using CinemAPI.Domain.Contracts;
    using CinemAPI.Domain.Contracts.Models.Reservation;
    using CinemAPI.Models;
    using CinemAPI.Models.Contracts.Cinema;
    using CinemAPI.Models.Contracts.Movie;
    using CinemAPI.Models.Contracts.Projection;
    using CinemAPI.Models.Contracts.Reservation;
    using CinemAPI.Models.Contracts.Room;
    using CinemAPI.Models.Contracts.Ticket;
    using CinemAPI.Models.Input.Reservation;

    using System;
    using System.Web.Http;
    public class ReservationController : ApiController
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly IRoomRepository roomRepo;
        private readonly IReservationRepository reservationRepo;
        private readonly ICinemaRepository cinemaRepo;
        private readonly IMovieRepository movieRepo;
        private readonly ITicketRepository ticketRepo;
        private readonly IReservationCancellation reservationCancellation;

        public ReservationController(
            IProjectionRepository projectionRepo, 
            IRoomRepository roomRepo, 
            IReservationRepository reservationRepo,
            ICinemaRepository cinemaRepo,
            IMovieRepository movieRepo,
            ITicketRepository ticketRepo,
            IReservationCancellation reservationCancellation)
        {
            this.projectionRepo = projectionRepo ?? throw new ArgumentNullException();
            this.roomRepo = roomRepo ?? throw new ArgumentNullException();
            this.reservationRepo = reservationRepo ?? throw new ArgumentNullException();
            this.cinemaRepo = cinemaRepo ?? throw new ArgumentNullException();
            this.movieRepo = movieRepo ?? throw new ArgumentNullException();
            this.ticketRepo = ticketRepo ?? throw new ArgumentNullException();
            this.reservationCancellation = reservationCancellation ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IHttpActionResult ReserveSeatsForProjection(SeatReservationModel model)
        {
            IProjection projection = projectionRepo.Get(model.ProjectionId);

            if (projection == null)
            {
                return BadRequest("Projection doesn't exist.");
            }
            else if (projection.StartDate <= DateTime.Now.Subtract(TimeSpan.FromMinutes(10)))
            {
                return BadRequest("Can't reserve seats for projection which is starting in less than 10 minutes.");
            }
            else if (projection.AvailableSeatsCount == 0)
            {
                return BadRequest("No seats available.");
            }

            IReservation reservedSeats = this.reservationRepo.GetByProjectionIdAndSeat(model.ProjectionId, model.Row, model.Column);
            ITicket boughtTicket = this.ticketRepo.GetByProjectionIdAndSeat(model.ProjectionId, model.Row, model.Column);
            if (reservedSeats != null || boughtTicket != null)
            {
                return BadRequest("Chosen seats are either already reserved or bought.");
            }
            IRoom room = roomRepo.GetById(projection.RoomId);

            if (model.Row > room.Rows || model.Column > room.SeatsPerRow)
            {
                return BadRequest("Seat doesn't exist in room.");
            }

            ICinema cinema = cinemaRepo.GetById(room.CinemaId);
            IMovie movie = movieRepo.GetById(projection.MovieId);
            this.reservationRepo.Insert(new Reservation(model.ProjectionId,
                                                        model.Row, 
                                                        model.Column, 
                                                        projection.StartDate, 
                                                        movie.Name, 
                                                        cinema.Name, 
                                                        room.Number));

            IReservation reservationTicket = this.reservationRepo.GetByProjectionIdAndSeat(model.ProjectionId, model.Row, model.Column);
            if (reservationTicket != null)
            {
                projection.AvailableSeatsCount -= 1;
                this.projectionRepo.Save();
            }
            else
            {
                return BadRequest("Reservation failed.");
            }

            ReservationResponseModel reservationResponseModel = new ReservationResponseModel(reservationTicket.Id, reservationTicket.StartDate, reservationTicket.MovieName, reservationTicket.CinemaName, reservationTicket.Row, reservationTicket.Column, reservationTicket.RoomNumber);

            return Ok(reservationResponseModel);
        }

        [HttpPost]
        public IHttpActionResult CancelReservation(ReservationCancelationModel model)
        {
            ReservationCancellationSummary summary = reservationCancellation.CancelReservation(model.ReservationId);

            if (summary.ReservationIsCanceled)
            {
                return Ok(summary.Message);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}