

namespace web_based_Bug_Management_Dashboard.Models.Domain
{
    public class Bug
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public BugStatus Status { get; set; } = BugStatus.Open;
        public string ReporterName { get; set; } = string.Empty;
        public string? AssignedTo { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
