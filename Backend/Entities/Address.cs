using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    [Owned]
    public class Address
    {
        [MaxLength(255)]
        public string? StreetOne { get; set; }

        [MaxLength(255)]
        public string? StreetTwo { get; set; }

        [MaxLength(32)]
        public string? PostCode { get; set; }

        [MaxLength(255)]
        public string? CityName { get; set; }

        [MaxLength(255)]
        public string? Country { get; set; }
    }
}
