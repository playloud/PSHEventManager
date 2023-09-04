using System.Runtime.InteropServices.JavaScript;

namespace PSHEventManager.EventManager.Model.PostBody;

public class EventBody
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public int OrganizerId { get; set; }
    public int Capacity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}