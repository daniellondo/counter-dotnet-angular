namespace Domain.Entities
{
    using System;

    public class CountEntity
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
