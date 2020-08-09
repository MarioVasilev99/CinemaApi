using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Room;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemAPI.Domain.NewProjection
{
    public class NewProjectionNextOverlapValidation : INewProjection
    {
        private readonly IProjectionRepository projectRepo;
        private readonly IMovieRepository movieRepo;
        private readonly INewProjection newProj;
        private readonly IRoomRepository roomRepo;

        public NewProjectionNextOverlapValidation(IProjectionRepository projectRepo, IMovieRepository movieRepo, INewProjection newProj, IRoomRepository roomRepo)
        {
            this.projectRepo = projectRepo;
            this.movieRepo = movieRepo;
            this.newProj = newProj;
            this.roomRepo = roomRepo;
        }

        public NewProjectionSummary New(IProjectionCreation proj)
        {
            IEnumerable<IProjection> movieProjectionsInRoom = projectRepo.GetActiveProjections(proj.RoomId);

            IProjection nextProjection = movieProjectionsInRoom.Where(x => x.StartDate > proj.StartDate)
                                                                       .OrderBy(x => x.StartDate)
                                                                       .FirstOrDefault();

            if (nextProjection != null)
            {
                IMovie curMovie = movieRepo.GetById(proj.MovieId);
                IMovie nextProjectionMovie = movieRepo.GetById(nextProjection.MovieId);

                DateTime curProjectionEndTime = proj.StartDate.AddMinutes(curMovie.DurationMinutes);

                if (curProjectionEndTime >= nextProjection.StartDate)
                {
                    return new NewProjectionSummary(false, string.Format(
                        ErrorMessagesHelper.ProjectionOverlapsWithNext,
                        nextProjectionMovie.Name,
                        nextProjection.StartDate));
                }
            }
            

            return newProj.New(proj);
        }
    }
}