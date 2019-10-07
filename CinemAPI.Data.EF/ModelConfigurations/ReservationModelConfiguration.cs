using CinemAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    internal sealed class ReservationModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Reservation> reservationModel = modelBuilder.Entity<Reservation>();
            reservationModel.HasKey(model => model.Id);
            reservationModel.Property(model => model.ProjectionId).IsRequired();
            reservationModel.Property(model => model.MovieName).IsRequired();
            reservationModel.Property(model => model.RoomNumber).IsRequired();
            reservationModel.Property(model => model.StartDate).IsRequired();
            reservationModel.Property(model => model.CinemaName).IsRequired();
            reservationModel.Property(model => model.IsCanceled).IsRequired();
            reservationModel.Property(model => model.IsBought).IsRequired();
            reservationModel.Property(model => model.Column)
                            .IsRequired()
                            .IsConcurrencyToken();
            reservationModel.Property(model => model.Row)
                            .IsRequired()
                            .IsConcurrencyToken();
        }
    }
}
