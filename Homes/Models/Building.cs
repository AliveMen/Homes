using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Homes.Models
{
    public class Building : Entity
    {
        [StringLength(30)]
        public string Name { get; set; }

        [Range(1, 200, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Required]
        public int Levels { get; set; }

        [Required]
        [Range(1600, 2100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int FoundationYear { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }
        [RegularExpression(@"^[0-9]{6,6}$")]
        [Required]
        public int PostCode { get; set; }

        [StringLength(120)]
        [Required]
        public string AddressLine { get; set; }

        [Required]
        public string Image { get; set; }


        #region Navigation Properties

        public virtual ObservableCollection<Tenant> Tenants { get; set; } = new ObservableCollection<Tenant>();


        #endregion

    }
}