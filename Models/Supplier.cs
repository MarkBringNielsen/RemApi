using System;
using RemApi.DTOs;

namespace RemApi.Models
{
    public class Supplier
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }

        public Supplier() {}
        public Supplier(SupplierDTO dto)
        {
            this.ID = Guid.NewGuid();
            this.Name = dto.Name;
            this.Description = dto.Description;
            this.Address = dto.Address;
            this.ZipCode = dto.ZipCode;
            this.Contact = dto.Contact;
            this.Email = dto.Email;
            this.Phonenumber = dto.Phonenumber;
        }
        public Supplier(SupplierDTO dto, Guid id)
        {
            this.ID = id;
            this.Name = dto.Name;
            this.Description = dto.Description;
            this.Address = dto.Address;
            this.ZipCode = dto.ZipCode;
            this.Contact = dto.Contact;
            this.Email = dto.Email;
            this.Phonenumber = dto.Phonenumber;
        }

    }
}