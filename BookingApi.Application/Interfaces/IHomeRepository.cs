using BookingApi.Domain.Entities;

namespace BookingApi.Application.Interfaces;

public interface IHomeRepository
{
    Task<IEnumerable<Home>> GetAllAsync();
}