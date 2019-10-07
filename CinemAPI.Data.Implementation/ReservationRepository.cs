using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Data.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext db;

        public ReservationRepository(CinemaDbContext db)
        {
            this.db = db;
        }
        public IReservation GetById(int reservationId)
        {
            return this.db.Reservations.FirstOrDefault(x => x.Id == reservationId);
        }

        public void Insert(IReservationCreation reservation)
        {
            Reservation newReservation = new Reservation(reservation.ProjectionId, 
                                                         reservation.Row, 
                                                         reservation.Column, 
                                                         reservation.StartDate,
                                                         reservation.MovieName,
                                                         reservation.CinemaName,
                                                         reservation.RoomNumber);

            db.Reservations.Add(newReservation);

            db.SaveChanges();
        }

        public IReservation GetByProjectionIdAndSeat(long projectionId, byte row, byte column)
        {
            return db.Reservations.Where(x => x.ProjectionId == projectionId &&
                                         x.Row == row &&
                                         x.Column == column).FirstOrDefault();
        }

        public void Save()
        {
            this.db.SaveChanges();
        }
    }
}
