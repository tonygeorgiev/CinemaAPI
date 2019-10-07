using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models
{
    public class Reservation : IReservation, IReservationCreation
    {
        public Reservation()
        {

        }

        public Reservation(long ProjectionId, byte Row, byte Column, DateTime StartDate, string MovieName, string CinemaName, int RoomNumber)
            : this()
        {
            this.ProjectionId = ProjectionId;
            this.Row = Row;
            this.Column = Column;
            this.StartDate = StartDate;
            this.MovieName = MovieName;
            this.CinemaName = CinemaName;
            this.RoomNumber = RoomNumber;
            this.IsBought = false;
            this.IsCanceled = false;
        }
        public long Id { get; set; }

        public long ProjectionId { get; set; }

        public DateTime StartDate { get; set; }

        public string MovieName { get; set; }

        public string CinemaName { get; set; }
                
        public byte Row { get; set; }

        public byte Column { get; set; }
                                 
        public int RoomNumber { get; set; }

        public bool IsCanceled { get; set; }

        public bool IsBought { get; set; }
    }
}
