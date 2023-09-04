using System.Runtime.InteropServices.JavaScript;

namespace PSHEventManager.EventManager.Model;

public class Registration
{
    public string Id { get; set; }
    public string Useremail { get; set; }
    public string EventId { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsDeleted { get; set; }

}