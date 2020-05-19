using System.ComponentModel.DataAnnotations;

namespace Homes.Models
{
    public interface IEntity
    {
        [StringLength(128)]
        [Required]
        string Id { get; set; }
        bool IsTransient();
    }
}
