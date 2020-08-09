using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Dtos.Projection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemAPI.Data.Implementation
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly CinemaDbContext db;

        public ProjectionRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public IProjection Get(int movieId, int roomId, DateTime startDate)
        {
            return db.Projections.FirstOrDefault(x => x.MovieId == movieId &&
                                                      x.RoomId == roomId &&
                                                      x.StartDate == startDate);
        }

        public bool DoesProjectionExist(long projectionId)
        {
            return db.Projections.Any(x => x.Id == projectionId);
        }

        public IEnumerable<IProjection> GetActiveProjections(int roomId)
        {
            DateTime now = DateTime.UtcNow;

            return db.Projections.Where(x => x.RoomId == roomId &&
                                             x.StartDate > now);
        }

        public ProjectionSeatsInfoDto GetProjectionSeatsInfo(long projectionId)
        {
            return db.Projections
                .Where(x => x.Id == projectionId)
                .Select(x => new ProjectionSeatsInfoDto()
                {
                    AvailableSeatsCount = x.AvailableSeatsCount,
                    StartDate = x.StartDate,
                })
                .FirstOrDefault();
        }

        public void Insert(IProjectionCreation proj)
        {
            Projection newProj = new Projection(proj.MovieId, proj.RoomId, proj.StartDate);
            db.Projections.Add(newProj);

            newProj.AvailableSeatsCount = newProj.Room.Rows * newProj.Room.SeatsPerRow;

            db.SaveChanges();
        }

        public ProjectionStartDto GetProjectionStartDate(long projectionId)
        {
            return db.Projections
                .Where(x => x.Id == projectionId)
                .Select(x => new ProjectionStartDto()
                {
                    StartDate = x.StartDate,
                })
                .FirstOrDefault();
        }

        public void DecreaseAvailableSeats(long projectionId)
        {
            IProjection projection = db.Projections.FirstOrDefault(x => x.Id == projectionId);

            projection.AvailableSeatsCount -= 1;

            db.SaveChanges();
        }

        public void IncreaseAvailableSeats(long projectionId)
        {
            IProjection projection = db.Projections.FirstOrDefault(x => x.Id == projectionId);

            projection.AvailableSeatsCount += 1;

            db.SaveChanges();
        }
    }
}