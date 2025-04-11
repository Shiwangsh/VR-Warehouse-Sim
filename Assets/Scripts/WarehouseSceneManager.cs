using UnityEngine;

public class WarehouseSceneManager : MonoBehaviour
{
    [Header("Box Settings")]
    [SerializeField] private GameObject[] boxPrefabs;
    [SerializeField] private Vector3 boxSpawnPoint;
    [SerializeField] private float boxSpawnRate;
    [SerializeField] private float boxSpawnRateVariation;
    [SerializeField] private BoxType[] boxTypes;
    private Timer boxTimer;

    [Header("Robot Settings")]
    [SerializeField] private Robot robot;

    void Start()
    {
        if (gameObject.TryGetComponent<Timer>(out boxTimer))
        {
            boxTimer.Reset(boxSpawnRate);
        }
        else
        {
            boxTimer = gameObject.AddComponent<Timer>();
            boxTimer.Reset(boxSpawnRate);
        }

        robot.Init(transform);
    }

    void Update()
    {
        SpawnBoxes();
    }

    private void SpawnBoxes()
    {
        if (boxTimer.isReady)
        {
            GameObject box = Instantiate(boxPrefabs[Random.Range(0, boxPrefabs.Length)], transform);
            box.GetComponent<Box>().type = boxTypes[Random.Range(0, boxTypes.Length)];
            box.transform.position = boxSpawnPoint;
            box.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

            boxTimer.Reset(boxSpawnRate + Random.Range(-boxSpawnRateVariation, boxSpawnRateVariation));
        }
    }
}
