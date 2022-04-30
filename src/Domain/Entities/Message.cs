using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities;

public class Message : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid? ChatId { get; set; }
    public Guid? GroupId { get; set; }
    public string? MessageText { get; set; }
    public string? Name { get; set; }
    public string? Profile { get; set; }
    public bool? IsToday { get; set; }
    public string? MessageText2 { get; set; }
    public bool? IsImage { get; set; }
    public bool? IsFile { get; set; }
    public bool? FileContent { get; set; }
    public bool? FileSize { get; set; }
    public bool? IsTyping { get; set; }
    public List<string> ImageContent { get; set; } = new List<string>();

    public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
}
