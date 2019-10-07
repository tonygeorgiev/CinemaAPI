namespace CinemAPI.Controllers
{
    using CinemAPI.Data;
    using CinemAPI.Models;
    using CinemAPI.Models.Contracts.Cinema;
    using CinemAPI.Models.Contracts.Movie;
    using CinemAPI.Models.Contracts.Projection;
    using CinemAPI.Models.Contracts.Reservation;
    using CinemAPI.Models.Contracts.Room;
    using CinemAPI.Models.Contracts.Ticket;
    using CinemAPI.Models.Input.Ticket;

    using System;
    using System.Web.Http;

    public class TicketController : ApiController
    {
        private readonly IProjectionRepository projectionRepo;
        private readonly IRoomRepository roomRepo;
        private readonly ITicketRepository ticketRepo;
        private readonly IReservationRepository reservationRepo;
        private readonly ICinemaRepository cinemaRepo;
        private readonly IMovieRepository movieRepo;

        public TicketController(
            IProjectionRepository projectionRepo, 
            IRoomRepository roomRepo, 
            ITicketRepository ticketRepo, 
            IReservationRepository reservationRepo,
            ICinemaRepository cinemaRepo,
            IMovieRepository movieRepo)
        {
            this.projectionRepo = projectionRepo ?? throw new ArgumentNullException();
            this.roomRepo = roomRepo ?? throw new ArgumentNullException();
            this.ticketRepo = ticketRepo ?? throw new ArgumentNullException();
            this.reservationRepo = reservationRepo ?? throw new ArgumentNullException();
            this.cinemaRepo = cinemaRepo ?? throw new ArgumentNullException();
            this.movieRepo = movieRepo ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IHttpActionResult BuyTicketWithoutReservation(TicketWithoutReservationModel model)
        {
            IProjection projection = projectionRepo.Get(model.ProjectionId);

            if (projection == null)
            {
                return BadRequest("Projection doesn't exist.");
            }
            else if(projection.StartDate <= DateTime.Now)
            {
                return BadRequest("Projection has either already started or finished.");
            }
            else if(projection.AvailableSeatsCount == 0)
            {
                return BadRequest("No seats left available.");
            }

            IReservation reservedSeats = this.reservationRepo.GetByProjectionIdAndSeat(model.ProjectionId, model.Row, model.Column);
            ITicket boughtTicket = this.ticketRepo.GetByProjectionIdAndSeat(model.ProjectionId, model.Row, model.Column);
            if (reservedSeats != null || boughtTicket != null)
            {
                return BadRequest("Chosen seats are either already reserved or bought.");
            }

            IRoom room = roomRepo.GetById(projection.RoomId);
            ICinema cinema = cinemaRepo.GetById(room.CinemaId);
            IMovie movie = movieRepo.GetById(projection.MovieId);
            this.ticketRepo.Insert(new Ticket(model.ProjectionId,
                                              model.Row,
                                              model.Column,
                                              projection.StartDate,
                                              movie.Name,
                                              cinema.Name,
                                              room.Number));

           


            ITicket newTicket = this.ticketRepo.GetByProjectionIdAndSeat(model.ProjectionId, model.Row, model.Column);
            if (newTicket != null)
            {
                projection.AvailableSeatsCount -= 1;
                this.projectionRepo.Save();
            }
            else
            {
                return BadRequest("Ticket purchase failed.");
            }


            TicketResponseModel ticketResponseModel = new TicketResponseModel(newTicket.Id, 
                                                                              newTicket.StartDate, 
                                                                              newTicket.MovieName, 
                                                                              newTicket.CinemaName, 
                                                                              newTicket.Row, 
                                                                              newTicket.Column, 
                                                                              newTicket.RoomNumber);

            return Ok(ticketResponseModel);
        }

        [HttpPost]
        public IHttpActionResult BuyTicketWithReservation(TicketWithReservationModel model)
        {
            IReservation reservation = this.reservationRepo.GetById(model.ReservationId);
            
            if (reservation == null)
            {
                return BadRequest("Reservation doesn't exist.");
            }
            else if (reservation.StartDate <= DateTime.Now.Subtract(TimeSpan.FromMinutes(10)))
            {
                return BadRequest("Projection has either already started or finished.");
            }
            else if(reservation.IsCanceled == true)
            {
                return BadRequest("Cannot buy tickets for a canceled reservation.");
            }
            else if(reservation.IsBought == true)
            {
                return BadRequest("Tickets for specified reservation have already been bought.");
            }

            IProjection projection = projectionRepo.Get(reservation.ProjectionId);
            IRoom room = roomRepo.GetById(projection.RoomId);
            ICinema cinema = cinemaRepo.GetById(room.CinemaId);
            IMovie movie = movieRepo.GetById(projection.MovieId);

            this.ticketRepo.Insert(new Ticket(reservation.ProjectionId,
                                                        reservation.Row,
                                                        reservation.Column,
                                                        reservation.StartDate,
                                                        movie.Name,
                                                        cinema.Name,
                                                        room.Number));


            ITicket newTicket = this.ticketRepo.GetByProjectionIdAndSeat(reservation.ProjectionId, reservation.Row, reservation.Column);

            if (newTicket != null)
            {
                reservation.IsBought = true;
                this.reservationRepo.Save();
            }
            else
            {
                return BadRequest("Ticket purchase failed.");
            }

            TicketResponseModel ticketResponseModel = new TicketResponseModel(newTicket.Id, newTicket.StartDate, newTicket.MovieName, newTicket.CinemaName, newTicket.Row, newTicket.Column, newTicket.RoomNumber);

            return Ok(ticketResponseModel);
        }
    }
}