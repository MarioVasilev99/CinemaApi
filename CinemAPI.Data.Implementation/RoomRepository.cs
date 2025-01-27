﻿using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Dtos.Room;
using System.Linq;

namespace CinemAPI.Data.Implementation
{
    public class RoomRepository : IRoomRepository
    {
        private readonly CinemaDbContext db;

        public RoomRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public IRoom GetByCinemaAndNumber(int cinemaId, int number)
        {
            return db.Rooms.FirstOrDefault(x => x.CinemaId == cinemaId &&
                                                x.Number == number);
        }

        public IRoom GetById(int id)
        {
            return db.Rooms.FirstOrDefault(x => x.Id == id);
        }

        public RoomSeatsRowsDto GetRoomSeatsRowsInfo(long projectionId)
        {
            return db.Rooms
                .Where(x => x.Projections.Any(p => p.Id == projectionId))
                .Select(x => new RoomSeatsRowsDto()
                {
                    Rows = x.Rows,
                    SeatsPerRow = x.SeatsPerRow,
                })
                .FirstOrDefault();
        }

        public void Insert(IRoomCreation room)
        {
            Room newRoom = new Room(room.Number, room.SeatsPerRow, room.Rows, room.CinemaId);

            db.Rooms.Add(newRoom);
            db.SaveChanges();
        }
    }
}