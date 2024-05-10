namespace Framework.Domain;

public class OutBoxEventItem
{
    public long Id { get; set; }
    public Guid EventId { get; set; }
    public string AccuredUserId { get; set; }
    public DateTime AccuredOn { get; set; }
    public string AggragateName { get; set; }
    public string AggragateTypeName { get; set; }
    public string AggragateId { get; set; }
    public string EventName { get; set; }
    public string EventTypeName { get; set; }
    public string EventPayload { get; set; }
    public bool IsProcessed { get; set; }
}