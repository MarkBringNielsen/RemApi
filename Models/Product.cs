using System;
using RemApi.DTOs;
using RemApi.Acquaintances;

namespace RemApi.Models
{
    public class Product : IIdentifiable
    {

        public Product() {}
        public Product(ProductDTO dto, Category category, Supplier supplier) 
        {
            this.ID = Guid.NewGuid();
            this.Name = dto.Name;
            this.Description = dto.Description;
            this.Unit = dto.Unit;
            this.PackagedQuantity = dto.PackagedQuantity;
            this.Price = dto.Price;
            this.StockedQuantity = dto.StockedQuantity;
            this.Category = category;
            this.Supplier = supplier;
        }

        public Product(ProductDTO dto, Category category, Supplier supplier, Guid id) 
        {
            this.ID = id;
            this.Name = dto.Name;
            this.Description = dto.Description;
            this.Unit = dto.Unit;
            this.PackagedQuantity = dto.PackagedQuantity;
            this.Price = dto.Price;
            this.StockedQuantity = dto.StockedQuantity;
            this.Category = category;
            this.Supplier = supplier;
        }
        

        public Guid ID { get; set ;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public double PackagedQuantity { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int StockedQuantity { get; set; }
        public Supplier Supplier { get; set; }
    }
}