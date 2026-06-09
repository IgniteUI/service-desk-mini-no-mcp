using System.Text.Json;
using IgApplication.Models;

namespace IgApplication.Services;

public class TicketService
{
    private readonly LocalStorageService _storage;
    private List<Ticket> _tickets = new();
    private const string StorageKey = "servicedesk_tickets_v1";
    private bool _initialized = false;

    public event Action? OnTicketsChanged;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = false
    };

    public TicketService(LocalStorageService storage)
    {
        _storage = storage;
    }

    public async Task EnsureInitializedAsync()
    {
        if (_initialized) return;

        var json = await _storage.GetItemAsync(StorageKey);
        if (!string.IsNullOrEmpty(json))
        {
            try
            {
                var data = JsonSerializer.Deserialize<List<Ticket>>(json, _jsonOptions);
                if (data != null && data.Count > 0)
                {
                    _tickets = data;
                    _initialized = true;
                    return;
                }
            }
            catch { /* fall through to seed */ }
        }

        _tickets = SeedMockData();
        await PersistAsync();
        _initialized = true;
    }

    public List<Ticket> GetAll() => _tickets.ToList();

    public Ticket? GetById(string id) => _tickets.FirstOrDefault(t => t.Id == id);

    public async Task AddAsync(Ticket ticket)
    {
        ticket.Id = GenerateId();
        _tickets.Insert(0, ticket);
        await PersistAsync();
        OnTicketsChanged?.Invoke();
    }

    public async Task UpdateAsync(Ticket ticket)
    {
        var idx = _tickets.FindIndex(t => t.Id == ticket.Id);
        if (idx >= 0)
        {
            _tickets[idx] = ticket;
            await PersistAsync();
            OnTicketsChanged?.Invoke();
        }
    }

    public async Task DeleteAsync(string id)
    {
        _tickets.RemoveAll(t => t.Id == id);
        await PersistAsync();
        OnTicketsChanged?.Invoke();
    }

    private async Task PersistAsync()
    {
        var json = JsonSerializer.Serialize(_tickets, _jsonOptions);
        await _storage.SetItemAsync(StorageKey, json);
    }

    private static string GenerateId()
    {
        return $"TKT-{Math.Abs(Guid.NewGuid().GetHashCode()) % 90000 + 10000}";
    }

    private static List<Ticket> SeedMockData()
    {
        var customers = new[] { "Acme Corp", "TechNova Inc", "Blue Sky Ltd", "Vertex Systems", "DataCore Inc", "Pinnacle Group", "Nexus Labs", "Horizon Ltd" };
        var assignees = TeamMembers.All;
        var statuses = TicketStatuses.All;
        var priorities = TicketPriorities.All;
        var rnd = new Random(42);

        var titles = new[]
        {
            "Cannot login to customer portal",
            "Payment gateway timeout error",
            "Dashboard charts not loading",
            "Email notifications not being sent",
            "API rate limit exceeded on prod",
            "Mobile app crashes on startup",
            "Report export fails for large datasets",
            "SSO integration authentication error",
            "Incorrect invoice amount displayed",
            "Performance degradation on peak hours",
            "User role assignment broken",
            "Search returning no results",
            "File upload size limit issue",
            "Two-factor auth not working",
            "Data sync delay between regions",
            "Password reset email not received",
            "Bulk import validation errors",
            "Chart drilldown not responding",
            "Session timeout too aggressive",
            "Missing audit log entries",
            "Integration webhook failures",
            "Slow query on analytics page",
            "Dark mode display glitches",
            "CSV export encoding problem",
            "Notification preferences not saving"
        };

        var descriptions = new[]
        {
            "Customer reported they cannot access the portal. Issue started after last deployment.",
            "Payment processing system timing out. Multiple customers affected. Revenue impact.",
            "Dashboard not rendering charts. Console shows JavaScript errors.",
            "Email notifications stopped firing. SMTP logs show no attempts.",
            "API returning 429 errors. Rate limit configuration needs review.",
            "Application crashes immediately after launch on iOS devices.",
            "Large dataset exports fail with memory exception after 30 minutes.",
            "SSO login returns error code AUTH_FAILED for enterprise accounts.",
            "Invoice totals not matching expected values after discount application.",
            "System slows significantly during 9-11am peak usage window.",
            "Admin panel role assignments not persisting after save.",
            "Search returning empty results even for exact matches.",
            "Files over 10MB rejected by upload endpoint unexpectedly.",
            "2FA codes not being accepted even when correct.",
            "Data between US and EU regions showing 20+ minute sync delay.",
            "Password reset emails going to spam or not delivered.",
            "Batch import failing validation for records that appear valid.",
            "Drill-down on bar charts does nothing when clicked.",
            "Users being logged out after 15 minutes of activity.",
            "Some admin actions not appearing in audit trail.",
            "Webhook events for order_created not being dispatched.",
            "Analytics page taking 45+ seconds to load.",
            "UI elements misaligned when dark mode is enabled.",
            "Special characters corrupted in CSV export files.",
            "Notification preference changes reverting after page refresh."
        };

        var tickets = new List<Ticket>();
        for (int i = 0; i < 25; i++)
        {
            var createdDays = rnd.Next(2, 60);
            var created = DateTime.Today.AddDays(-createdDays);
            var dueOffset = rnd.Next(-5, 20);
            var due = created.AddDays(dueOffset < 3 ? 3 : dueOffset);

            tickets.Add(new Ticket
            {
                Id = $"TKT-{10000 + i}",
                Title = titles[i % titles.Length],
                Customer = customers[rnd.Next(customers.Length)],
                Assignee = assignees[rnd.Next(assignees.Length)],
                Priority = priorities[rnd.Next(priorities.Length)],
                Status = statuses[rnd.Next(statuses.Length)],
                CreatedDate = created,
                DueDate = due,
                Description = descriptions[i % descriptions.Length]
            });
        }
        return tickets;
    }
}
