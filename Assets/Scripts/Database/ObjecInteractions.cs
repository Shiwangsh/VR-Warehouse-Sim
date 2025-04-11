using System;

public class ObjectInteraction
{
	public string InteractionId { get; set; }
	public string SessionId { get; set; }
	public string ObjectId { get; set; }
	public string Action { get; set; } // e.g., "picked_up", "sorted", "dropped"
	public bool Success { get; set; }
	public RobotBehavior CurrentBehavior { get; set; }
	public DateTime Timestamp { get; set; }

	public ObjectInteraction(string sessionId, string objectId, string action, bool success, RobotBehavior currentBehavior)
	{
		InteractionId = Guid.NewGuid().ToString();
		SessionId = sessionId;
		ObjectId = objectId;
		Action = action;
		Success = success;
		CurrentBehavior = currentBehavior;
		Timestamp = DateTime.UtcNow;
	}
}
