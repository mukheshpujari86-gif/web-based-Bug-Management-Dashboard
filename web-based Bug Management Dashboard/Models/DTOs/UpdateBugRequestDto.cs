using System.ComponentModel.DataAnnotations;
using web_based_Bug_Management_Dashboard.Models.Domain;

namespace web_based_Bug_Management_Dashboard.Models.DTOs
{
    public class UpdateBugRequestDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        public BugStatus Status { get; set; }

        [Required]
        [MaxLength(100)]
        public string ReporterName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? AssignedTo { get; set; }
    }
}
