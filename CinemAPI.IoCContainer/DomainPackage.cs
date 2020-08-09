using CinemAPI.Domain;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.NewProjection;
using CinemAPI.Domain.NewReservation;
using CinemAPI.Domain.NewTicket;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer
{
    public class DomainPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<INewProjection, NewProjectionCreation>();
            container.RegisterDecorator<INewProjection, NewProjectionMovieValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionUniqueValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionRoomValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionRoomSeatsValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionPreviousOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionNextOverlapValidation>();

            container.Register<INewReservation, NewReservationCreation>();
            container.RegisterDecorator<INewReservation, NewReservationRowValidation>();
            container.RegisterDecorator<INewReservation, NewReservationColValidation>();
            container.RegisterDecorator<INewReservation, NewReservationSeatAvailableValidation>();
            container.RegisterDecorator<INewReservation, NewReservationProjectionFinishedValidation>();
            container.RegisterDecorator<INewReservation, NewReservationProjectionStartingValidation>();
            container.RegisterDecorator<INewReservation, NewReservationProjectionValidation>();

            container.Register<INewTicket, NewTicketCreation>();
            container.RegisterDecorator<INewTicket, NewTicketSeatFreeValidation>();
            container.RegisterDecorator<INewTicket, NewTicketProjectionStartTimeValidation>();
            container.RegisterDecorator<INewTicket, NewTicketSeatExistValidation>();
            container.RegisterDecorator<INewTicket, NewTicketProjectionExistValidation>();
        }
    }
}