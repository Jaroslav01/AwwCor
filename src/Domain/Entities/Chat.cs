using System.Diagnostics.CodeAnalysis;

namespace CleanArchitecture.Domain.Entities;

public class Chat : AuditableEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Status { get; set; }
    public string? LastMessage { get; set; }
//    public DateTime Time { get; set; }
    public string? UnRead { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsTyping { get; set; }
    public List<Message> Messages { get; set; } = new List<Message>();

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}