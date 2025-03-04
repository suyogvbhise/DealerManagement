using System.ComponentModel.DataAnnotations;

namespace DMS.Models
{
    public class PurchaseDeliveryNote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InventoryId { get; set; }
        public virtual Inventory? Inventory { get; set; }  // Linking Inventory

        [Required]
        [StringLength(100)]
        public string BuyerName { get; set; }

        [Required]
        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }
    }
}
