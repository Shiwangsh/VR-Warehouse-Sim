using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float rotationSensitivity = 1;
    [SerializeField] private float movementSpeed = 1;
    private Quaternion goalRot;
    [SerializeField] private float rotationSmoothness;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementVector += transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector -= transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector += transform.right;
        }

        movementVector.y = 0;

        controller.Move(movementVector.normalized * Time.deltaTime * movementSpeed);
    }

    private void Rotate()
    {
        float camRotX = Input.GetAxis("Mouse Y") * rotationSensitivity;
        float camRotY = Input.GetAxis("Mouse X") * rotationSensitivity;

        goalRot = Quaternion.Euler(goalRot.eulerAngles.x - camRotX, goalRot.eulerAngles.y + camRotY, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, goalRot, rotationSmoothness);
    }
}
