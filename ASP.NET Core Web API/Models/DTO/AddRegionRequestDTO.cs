using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Web_API.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MaxLength(300, ErrorMessage ="Name has to be a maximum of 30 characters")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 1 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
