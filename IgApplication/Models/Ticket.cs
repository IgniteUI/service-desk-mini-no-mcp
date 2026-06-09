namespace IgApplication.Models;

public class Ticket
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Customer { get; set; } = "";
    public string Assignee { get; set; } = "";
    public string Priority { get; set; } = "Medium";
    public string Status { get; set; } = "New";
    public DateTime CreatedDate { get; set; } = DateTime.Today;
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
    public string Description { get; set; } = "";

    // Computed helpers
    public string CreatedDateDisplay => CreatedDate.ToString("MMM dd, yyyy");
    public string DueDateDisplay => DueDate.ToString("MMM dd, yyyy");
    public bool IsOverdue => DueDate < DateTime.Today && Status != "Resolved" && Status != "Closed";
    public bool IsUrgent => (Priority == "Critical" || Priority == "High") && Status != "Resolved" && Status != "Closed";
}

public static class TicketStatuses
{
    public static readonly string[] All = ["New", "In Progress", "Waiting", "Resolved", "Closed"];
}

public static class TicketPriorities
{
    public static readonly string[] All = ["Low", "Medium", "High", "Critical"];
}

public static class TeamMembers
{
    public static readonly string[] All = ["Alice Johnson", "Bob Martinez", "Carol Williams", "David Chen", "Emma Davis", "Frank Wilson"];
}
