using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> ApplicationUsers { get; }
    DbSet<Message> Messages { get; }
    DbSet<Group> Groups { get; }
    DbSet<GroupUser> GroupUsers { get; }
    DbSet<Chat> Chats { get; }
    DbSet<ChatUser> ChatUsers { get; }
    DbSet<Contact> Contacts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
