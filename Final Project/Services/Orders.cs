using System;

namespace Order.Services
{
    public class Orders
    {
        public int id { get; set; }

        public string? name { get; set; }

        public string? lastName { get; set; }

        public string? plate { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

    }
}
