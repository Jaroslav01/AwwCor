namespace CleanArchitecture.Domain.Entities;

public class Contact : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid ApplicationUserId { get; set; }
    public string Name { get; set; }

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}