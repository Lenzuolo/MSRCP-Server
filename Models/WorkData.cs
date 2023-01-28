namespace MSRCP_Server.Models;

public enum WorkDataStatus
{
    NOT_STARTED = 0,
    COMPLETE = 1,
    WORKING = 2
}

public class WorkData
{
    public int Id { get; set; }
    public int Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    
    public User User { get; set; }
}
