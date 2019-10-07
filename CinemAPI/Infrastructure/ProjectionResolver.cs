namespace CinemAPI.Infrastructure
{
    using CinemAPI.Data.EF;
    using CinemAPI.Models.Contracts.Reservation;

    using System;
    using System.Linq;
    using System.Timers;

    public class ProjectionResolver
    {
        private readonly Timer eventTimer;

        // TO DO: add DI repository
        public ProjectionResolver(Timer eventTimer)
        {
            this.eventTimer = eventTimer;
        }
        public void StartEvents(int minutes)
        {
            eventTimer.Elapsed += new ElapsedEventHandler(CancelStartedProjections);
            eventTimer.Interval = minutes * 60 * 1000;
            eventTimer.AutoReset = true;
            eventTimer.Enabled = true;
        }
        private void CancelStartedProjections(object sender, ElapsedEventArgs e)
        {
            using (var db = new CinemaDbContext())
            {
                foreach (IReservation item in db.Reservations.ToList())
                {
                    if (item.StartDate <= DateTime.Now.Subtract(TimeSpan.FromMinutes(10)) && item.IsCanceled == false)
                    {
                        item.IsCanceled = true;
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}