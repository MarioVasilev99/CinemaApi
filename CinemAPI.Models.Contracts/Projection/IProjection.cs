﻿using System;

namespace CinemAPI.Models.Contracts.Projection
{
    public interface IProjection
    {
        long Id { get; }

        int RoomId { get; }

        int MovieId { get; }

        int AvailableSeatsCount { get; set; }

        DateTime StartDate { get; }
    }
}