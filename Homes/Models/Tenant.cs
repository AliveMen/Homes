using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Homes.Models
{
    public class Tenant : Entity
    {
        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime EndRentDate { get; set; }

        #region Navigation Properties

        public string BuildingId { get; set; }
        [JsonIgnore]
        public virtual Building Building { get; set; }

        #endregion

    }
}