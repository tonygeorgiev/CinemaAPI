using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IReservationRepository
    {
        IReservation GetById(int reservationId);
        IReservation GetByProjectionIdAndSeat(long projectionId, byte row, byte column);
        void Insert(IReservationCreation reservation);
        void Save();
    }
}
