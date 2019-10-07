using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemAPI.Models.Input.Reservation
{
    public class ReservationResponseModel
    {
        public ReservationResponseModel(long Id, DateTime StartDate, string MovieName, string CinemaName, byte Row, byte Column, int RoomNumber)
        {
            this.Id = Id;
            this.StartDate = StartDate;
            this.MovieName = MovieName;
            this.CinemaName = CinemaName;
            this.Row = Row;
            this.Column = Column;
            this.RoomNumber = RoomNumber;
        }
        public long Id { get; set; }

        public DateTime StartDate { get; set; }

        public string MovieName { get; set; }

        public string CinemaName { get; set; }

        public byte Row { get; set; }

        public byte Column { get; set; }

        public int RoomNumber { get; set; }
    }
}