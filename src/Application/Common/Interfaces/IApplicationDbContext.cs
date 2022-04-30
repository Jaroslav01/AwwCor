using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; }
    DbSet<Image> Images { get; }
    DbSet<Advertisement> Advertisements { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
