using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Web_API.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        [Required]
        [MaxLength(300, ErrorMessage = "Name has to be a maximum 300 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(500, ErrorMessage = "Description has to be a maximum 500 characters")]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
