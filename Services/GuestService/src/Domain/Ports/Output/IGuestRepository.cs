using Domain.Entities;

namespace Domain.Ports.Output;
public interface IGuestRepository
{
    Task<List<Guest>?> Get();
    Task<Guest?> GetById(int id);
    Task<Guest> Create(Guest guest);
    Task Update(Guest guest);
    Task Delete(Guest guest);
}