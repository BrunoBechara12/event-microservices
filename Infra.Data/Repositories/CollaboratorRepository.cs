using Domain.Ports.Output;
using Infra.Data.Context;

namespace Infra.Data.Repositories;
public class CollaboratorRepository : ICollaboratorRepository
{
    private readonly EventDbContext _context;

    public CollaboratorRepository(EventDbContext context)
    {
        _context = context;
    }

}
