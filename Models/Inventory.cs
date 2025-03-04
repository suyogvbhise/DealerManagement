using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMS.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{2}[0-9]{2}[A-Z]{1,2}[0-9]{4}$", ErrorMessage = "Invalid Registration Number Format")]
        public string RegistrationNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

        [NotMapped] // Derived Field
        public int RegistrationYear => RegistrationDate.Year;

        [Required]
        public string VehicleOwnership { get; set; } // Dropdown (1st Owner, 2nd Owner, etc.)

        // New Purchase Location Field
        public int? PurchaseLocationId { get; set; }
        public virtual PurchaseLocation? PurchaseLocations { get; set; }

        public int? FuelTypeId { get; set; } // Foreign Key
        public virtual FuelType? FuelType { get; set; } // Navigation Property

        public virtual ICollection<PurchaseDeliveryNote>? PurchaseDeliveryNotes { get; set; }


    }
}
