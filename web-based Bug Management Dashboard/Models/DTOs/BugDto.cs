using web_based_Bug_Management_Dashboard.Models.Domain;

namespace web_based_Bug_Management_Dashboard.Models.DTOs
{
    public class BugDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public BugStatus Status { get; set; }
        public string ReporterName { get; set; } = string.Empty;
        public string? AssignedTo { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
