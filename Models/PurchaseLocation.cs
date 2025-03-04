namespace DMS.Models
{
    public class PurchaseLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Area Name (e.g., Showroom A, Dealer X)

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    }
}
