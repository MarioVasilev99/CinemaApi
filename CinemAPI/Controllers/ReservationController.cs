using System.Web.Http;

using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Domain.Contracts.Services;
using CinemAPI.Domain.Helpers;
using CinemAPI.Models;
using CinemAPI.Models.Dtos.Reservation;
using CinemAPI.Models.Input.Reservation;

namespace CinemAPI.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly INewReservation newReservation;
        private readonly IReservationService reservationService;
        private readonly IProjectionService projectionService;

        public ReservationController(INewReservation newReservation, IReservationService reservationService, IProjectionService projectionService)
        {
            this.newReservation = newReservation;
            this.reservationService = reservationService;
            this.projectionService = projectionService;
        }

        [HttpPost]
        public IHttpActionResult Index(ReservationCreationModel model)
        {
            NewReservationSummary summary = newReservation.New(new Reservation(model.ProjectionId, model.Row, model.Col));

            if (summary.IsCreated)
            {
                projectionService.DecreaseAvailableSeatsCount(model.ProjectionId);
                ReservationTicket reservationTicket = reservationService.GetReservationTicket(model.ProjectionId, model.Row, model.Col);
                return Ok(reservationTicket);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [Route(EndpointsRoutesHelper.CancelReservationRoute)]
        [HttpPost]
        public IHttpActionResult CancelReservation([FromUri]ReservationCancellationModel model)
        {
            ReservationCancellationSummary summary = reservationService.CancelReservation(model.ReservationId);

            if (summary.IsCreated)
            {
                long projectionId = reservationService.GetReservationProjectionId(model.ReservationId);
                projectionService.IncreaseAvailableSeatsCount(projectionId);
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}
