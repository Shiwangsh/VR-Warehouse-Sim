using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    [SerializeField] public BoxType type;
    [SerializeField] private BoxSize size;
    [SerializeField] private Image[] stamps;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxVelocity = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        foreach (Image stamp in stamps)
        {
            stamp.sprite = type.stamp;
            stamp.color = type.color;
        }
    }

    void Update()
    {
        
    }

    public void DisableRB()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        rb.detectCollisions = false;
        rb.isKinematic = true;
        rb.Sleep();
    }

    public void EnableRB()
    {
        rb.detectCollisions = true;
        rb.isKinematic = false;
        rb.WakeUp();

        transform.parent = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            rb.AddForce(Vector3.right * 0.5f, ForceMode.Force);
            rb.linearVelocity = new Vector3(Mathf.Clamp(rb.linearVelocity.x, 0, maxVelocity), 
                                                        rb.linearVelocity.y, 
                                                        Mathf.Clamp(rb.linearVelocity.z, 0, maxVelocity));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When arrives at end of conveyor belt
        if (other.gameObject.layer == 7)
        {
            SimulationMetrics.BoxNotSorted(type.type, size);
            Destroy(this.gameObject);
        }
        // When placed into a sorting box
        else if (other.gameObject.layer == 9)
        {
            SortingBox sortingBox;
            if (other.gameObject.TryGetComponent<SortingBox>(out sortingBox))
            {
                if (sortingBox.GetBoxType() == type.type)
                {
                    SimulationMetrics.BoxCorrectlySorted(sortingBox.forRobot, type.type, size);
                }
                else
                {
                    SimulationMetrics.BoxIncorrectlySorted(sortingBox.forRobot, type.type, sortingBox.boxType, size);
                }
            }

            Destroy(this.gameObject);
        }
        // Attach to robot arm on contact with grab point
        else if (other.gameObject.layer == 10)
        {
            DisableRB();

            transform.parent = other.gameObject.transform;
        }
    }
}
