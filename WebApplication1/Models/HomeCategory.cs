﻿namespace WebApplication1.Models
{
    public class HomeCategory : BaseEntity<int>
    {
        public string Type { get; set; } = string.Empty;
        public virtual ICollection<HomeProduct> Product { get; set; }
    }
}