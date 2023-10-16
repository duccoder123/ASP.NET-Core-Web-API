﻿namespace ASP.NET_Core_Web_API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public string? RegionImageUrl { get; set; } 

    }
}
