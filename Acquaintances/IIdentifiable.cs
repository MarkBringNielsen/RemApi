using System;

namespace RemApi.Acquaintances
{
    public interface IIdentifiable
    {
        public Guid ID { get; set; }
    }
}