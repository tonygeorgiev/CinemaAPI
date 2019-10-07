using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface ITicketRepository
    {
        ITicket GetById(int movieId);
        ITicket GetByProjectionIdAndSeat(long projectionId, byte row, byte column);
        void Insert(ITicketCreation movie);
        void Save();
    }
}
