namespace CleanArchitecture.Domain.Entities;

public class ChatUser : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid ApplicationUserId { get; set; }
    public string Name { get; set; }
}