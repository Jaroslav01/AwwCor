namespace CleanArchitecture.Domain.Entities;

public class GroupUser : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public Guid ApplicationUserId { get; set; }
}