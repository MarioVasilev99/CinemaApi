using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.Services;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models;
using CinemAPI.Models.Input.Projection;
using System;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IProjectionService projectionService;

        public ProjectionController(INewProjection newProj, IProjectionService projectionService)
        {
            this.newProj = newProj;
            this.projectionService = projectionService;
        }

        [HttpPost]
        public IHttpActionResult Index(ProjectionCreationModel model)
        {
            NewProjectionSummary summary = newProj.New(new Projection(model.MovieId, model.RoomId, model.StartDate));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [Route(EndpointsRoutesHelper.GetAvailableSeatsRoute)]
        [HttpGet]
        public IHttpActionResult GetAvailableSeatsCount([FromUri]ProjectionAvailableSeatsModel model)
        {
            var projection = projectionService.GetProjectionSeatsInfoById(model.ProjectionId);

            if (projection == null)
            {
                return this.NotFound();
            }

            DateTime currentDateTime = DateTime.UtcNow;

            if (projection.StartDate < currentDateTime)
            {
                return BadRequest(ErrorMessagesHelper.ProjectionStartedOrFinished);
            }

            return Ok(projection.AvailableSeatsCount);
        }
    }
}