namespace CleanArchitecture.Domain.Entities;

public class Group : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid ApplicationUserId { get; set; }
    public string? Name { get; set; }
    public string? UnRead { get; set; }
    public List<Message> Messages { get; set; } = new List<Message>();

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}