﻿namespace CinemAPI.Controllers
{
    using CinemAPI.Data;
    using CinemAPI.Models;
    using CinemAPI.Models.Contracts.Room;
    using CinemAPI.Models.Input.Room;

    using System;
    using System.Web.Http;

    public class RoomController : ApiController
    {
        private readonly IRoomRepository roomRepo;

        public RoomController(IRoomRepository roomRepo)
        {
            this.roomRepo = roomRepo ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IHttpActionResult Index(RoomCreationModel model)
        {
            IRoom room = roomRepo.GetByCinemaAndNumber(model.CinemaId, model.Number);

            if (room == null)
            {
                roomRepo.Insert(new Room(model.Number, model.SeatsPerRow, model.Rows, model.CinemaId));

                return Ok();
            }

            return BadRequest("Room already exists");
        }
    }
}