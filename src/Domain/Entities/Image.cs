using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities;
public class Image : AuditableEntity
{
    public int Id { get; set; }
    public int AdvertisementId { get; set; }
    public string Url { get; set; }
}
