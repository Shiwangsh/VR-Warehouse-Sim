using UnityEngine;

public class SimulationMetrics : MonoBehaviour
{
    private static SimulationMetrics Singleton;

    void Start()
    {
        if (Singleton)
        {
            Destroy(this);
        }
        else
        {
            Singleton = this;
        }
    }

    void Update()
    {
        
    }

    public static void BoxCorrectlySorted(bool forRobot, BoxType.Type type, BoxSize size)
    {
        string userOrRobot;
        if (forRobot)
        {
            userOrRobot = "robot";
        }
        else
        {
            userOrRobot = "user";
        }

        Debug.Log($"{size} box with type {type} correctly sorted by {userOrRobot}");
    }

    public static void BoxIncorrectlySorted(bool forRobot, BoxType.Type boxType, BoxType.Type sortingBoxType, BoxSize size)
    {
        string userOrRobot;
        if (forRobot)
        {
            userOrRobot = "robot";
        }
        else
        {
            userOrRobot = "user";
        }

        Debug.Log($"{size} box with type {boxType} incorrectly sorted into {sortingBoxType} by {userOrRobot}");
    }

    public static void BoxNotSorted(BoxType.Type type, BoxSize size)
    {
        Debug.Log($"{size} box with type {type} was not sorted by user or robot");
    }
}
