namespace DMS.Models
{
    public class FuelType
    {
        public int Id { get; set; }  // Primary Key
        public string Name { get; set; } // Fuel Type Name

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    }
}
