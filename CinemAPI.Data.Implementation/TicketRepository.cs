using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext db;

        public TicketRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public ITicket GetById(int ticketId)
        {
            return db.Tickets.FirstOrDefault(x => x.Id == ticketId);
        }

        public ITicket GetByProjectionIdAndSeat(long projectionId, byte row, byte column)
        {
            return db.Tickets.Where(x => x.ProjectionId == projectionId &&
                                         x.Row == row &&
                                         x.Column == column).FirstOrDefault();
        }

        public void Save()
        {
            this.db.SaveChanges();
        }

        public void Insert(ITicketCreation ticket)
        {
           Ticket newTicket = new Ticket(ticket.ProjectionId, 
                                         ticket.Row, 
                                         ticket.Column, 
                                         ticket.StartDate, 
                                         ticket.MovieName, 
                                         ticket.CinemaName, 
                                         ticket.RoomNumber);
           
           db.Tickets.Add(newTicket);
           db.SaveChanges();
        }
    }
}
