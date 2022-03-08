﻿using Core.Model.DataTransferObjects;
using Domain.Entieties;

namespace Core.Repositories.Interfaces;

public interface IReservationRepository
{
    Task<List<Reservation>> GetReservationsByUserId(Guid id);
    Task<List<Reservation>> GetReservationsByAnnouncementId(Guid id);
    Task<Reservation> AddNewReservation(CreateReservationDto reservationDto);
    Task<Reservation> GetReservationsById(Guid id);
}