using System;

public enum RobotBehavior
{
    AlwaysLift,
    NeverLift,
    LiftOnRequest
}

public class RobotBehaviorLog
{
    public string LogId { get; set; }
    public string SessionId { get; set; }
    public RobotBehavior Behavior { get; set; }
    public DateTime Timestamp { get; set; }

    public RobotBehaviorLog(string sessionId, RobotBehavior behavior)
    {
        LogId = Guid.NewGuid().ToString();
        SessionId = sessionId;
        Behavior = behavior;
        Timestamp = DateTime.UtcNow;
    }
}
