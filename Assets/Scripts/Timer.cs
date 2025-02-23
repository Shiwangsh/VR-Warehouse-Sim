using UnityEngine;

public class Timer : MonoBehaviour
{
    public float maxTime = 1;
    private float currentTime = 0;
    public bool isReady = false;

    void Start()
    {
        currentTime = maxTime;
    }

    void Update()
    {
        if (currentTime <= 0)
        {
            isReady = true;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    public void Reset()
    {
        currentTime = maxTime;
        isReady = false;
    }

    public void Reset(float time)
    {
        maxTime = time;
        Reset();
    }
}
