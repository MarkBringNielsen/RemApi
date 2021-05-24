using System;
using RemApi.DTOs;

namespace RemApi.Models
{
    public class Category
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Category() {}
        public Category(CategoryDTO dto)
        {
            this.ID = Guid.NewGuid();
            this.Name = dto.Name;
            this.Description = dto.Description;
        }

        public Category(CategoryDTO dto, Guid id)
        {
            this.ID = id;
            this.Name = dto.Name;
            this.Description = dto.Description;
        }
    
    }

}