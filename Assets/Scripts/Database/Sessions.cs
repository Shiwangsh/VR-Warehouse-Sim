using System;
using System.Collections.Generic;

public class Session
{
    public string SessionId { get; set; }
    public string UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public List<RobotBehaviorLog> BehaviorLogs { get; set; }
    public List<ObjectInteraction> ObjectInteractions { get; set; }

    public Session(string sessionId, string userId)
    {
        SessionId = sessionId;
        UserId = userId;
        StartTime = DateTime.UtcNow;
        BehaviorLogs = new List<RobotBehaviorLog>();
        ObjectInteractions = new List<ObjectInteraction>();
    }

    public void EndSession()
    {
        EndTime = DateTime.UtcNow;
    }
}
