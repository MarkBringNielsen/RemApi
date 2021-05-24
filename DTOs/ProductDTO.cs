using System;

namespace RemApi.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public double PackagedQuantity { get; set; }
        public double Price { get; set; }
        public Guid CategoryID { get; set; }
        public int StockedQuantity { get; set; }
        public Guid SupplierID { get; set; }
    }
}