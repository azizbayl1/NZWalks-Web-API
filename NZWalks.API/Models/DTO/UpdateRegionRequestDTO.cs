using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be 3 characters minimum")]
        [MaxLength(4, ErrorMessage = "Code must be 4 characters maximum")]
        public string Code { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Name must be 3 characters minimum")]
        [MaxLength(100, ErrorMessage = "Name must be 100 characters maximum")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
