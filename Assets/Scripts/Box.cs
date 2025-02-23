using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    [SerializeField] public BoxType type;
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
        if (other.gameObject.layer == 7)
        {
            Debug.Log("Box Destroyed");
            Destroy(this.gameObject);
        }
    }
}
