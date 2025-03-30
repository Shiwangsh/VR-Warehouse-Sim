using UnityEngine;

public class Robot : MonoBehaviour
{
    private State state;
    private Transform boxManagerTransform;

    [Header("Movement")]
    [SerializeField] private float maxBaseDegPerSec = 180;
    [SerializeField] private float maxArmDegPerSec = 30;

    [Header("Pieces")]
    [SerializeField] private GameObject shoulderPivot;
    [SerializeField] private GameObject lowerArm;
    [SerializeField] float lowerArmLength = 0.9f;
    [SerializeField] private GameObject elbowPivot;
    [SerializeField] private GameObject upperArm;
    [SerializeField] float upperArmLength = 0.9f;
    [SerializeField] private GameObject wristPivot;
    [SerializeField] private GameObject hand;
    [SerializeField] float handLength = 0.1f;
    [SerializeField] private GameObject grabPoint;

    [Header("Initial Rotations")]
    [SerializeField] private float initShoulderAng = 5;
    [SerializeField] private float initElbowAng = 60;
    [SerializeField] private float initWristAng = 60;

    [Header("Sorting Boxes")]
    [SerializeField] SortingBox[] sortingBoxes;

    private float currentBaseAngle = 0;
    private float currentWristAngle = 0;
    private float currentShoulderAngle = 0;
    private float currentElbowAngle = 0;

    private Box grabbedBox;
    private SortingBox goalSortingBox;

    private enum State
    {
        INITIALIZE,
        TRACKING,
        GRABBING,
        DROPPING
    }

    void Start()
    {

    }

    public void Init(Transform boxManagerTransform)
    {
        state = State.INITIALIZE;
        this.boxManagerTransform = boxManagerTransform;
    }

    void Update()
    {
        switch (state)
        {
            case State.INITIALIZE:
                StateInitialize();
                break;
            case State.TRACKING:
                StateTracking();
                break;
            case State.GRABBING:
                StateGrabbing();
                break;
            case State.DROPPING:
                StateDropping();
                break;
            default:
                break;
        }
    }

    private void StateInitialize()
    {
        if (Rotate(0, initShoulderAng, initElbowAng, initWristAng))
        {
            state = State.TRACKING;
        }
    }

    private void StateTracking()
    {
        float minDist;
        GameObject closestBox;
        Track(out minDist, out closestBox);

        if (minDist < upperArmLength + lowerArmLength && minDist > 0)
        {
            state = State.GRABBING;
            grabPoint.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void StateGrabbing()
    {
        float minDist;
        GameObject closestBox;
        Track(out minDist, out closestBox);

        float shoulderAngle = CalcShoulderAng(closestBox);
        float elbowAngle = CalcElbowAng(closestBox);

        Rotate(currentBaseAngle, currentElbowAngle + elbowAngle, currentShoulderAngle + shoulderAngle + 65, 90);

        if (grabPoint.transform.childCount > 0)
        {
            grabbedBox = grabPoint.transform.GetChild(0).GetComponent<Box>();

            foreach (SortingBox sortingBox in sortingBoxes)
            {
                if (grabbedBox.type.type == sortingBox.GetBoxType())
                {
                    goalSortingBox = sortingBox;
                    break;
                }
            }

            state = State.DROPPING;
            grabPoint.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void StateDropping()
    {
        Quaternion q = Quaternion.LookRotation(goalSortingBox.transform.position - transform.position);

        bool hasArrived = Rotate(q.eulerAngles.y - 180, 15, 45, 45);

        if (hasArrived)
        {
            grabbedBox.EnableRB();
            state = State.TRACKING;
        }
    }

    private void Track(out float minDist, out GameObject closestBox)
    {
        if (boxManagerTransform.childCount > 0)
        {
            minDist = Mathf.Infinity;
            closestBox = null;

            foreach (Transform child in boxManagerTransform)
            {
                float dist = Vector3.Distance(transform.position, child.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    closestBox = child.gameObject;
                }
            }

            if (closestBox)
            {
                Quaternion q = Quaternion.LookRotation(closestBox.transform.position - transform.position);

                Rotate(q.eulerAngles.y - 180, currentShoulderAngle, currentElbowAngle, currentWristAngle);
            }
        }
        else
        {
            minDist = -1;
            closestBox = null;
        }
    }

    private float CalcShoulderAng(GameObject closestBox)
    {
        Vector3 boxPos = closestBox.transform.position;
        float dist = Vector3.Distance(transform.position, boxPos);

        return Mathf.Acos((Mathf.Pow(dist, 2) + Mathf.Pow(upperArmLength, 2) - Mathf.Pow(lowerArmLength, 2)) / (2 * upperArmLength * dist)) * Mathf.Rad2Deg;
    }

    private float CalcElbowAng(GameObject closestBox)
    {
        Vector3 boxPos = closestBox.transform.position;
        float dist = Vector3.Distance(transform.position, boxPos);

        return Mathf.Acos((Mathf.Pow(dist, 2) + Mathf.Pow(lowerArmLength, 2) - Mathf.Pow(upperArmLength, 2)) / (2 * lowerArmLength * dist)) * Mathf.Rad2Deg;
    }

    private bool Rotate(float goalBaseAng, float goalShoulderAng, float goalElbowAng, float goalWristAng)
    {
        bool isInPos = true;

        if (currentBaseAngle < goalBaseAng - 1)
        {
            transform.Rotate(new Vector3(0, maxBaseDegPerSec * Time.deltaTime, 0));
            currentBaseAngle += maxBaseDegPerSec * Time.deltaTime;
            isInPos = false;
        }
        else if (currentBaseAngle > goalBaseAng + 1)
        {
            transform.Rotate(new Vector3(0, -maxBaseDegPerSec * Time.deltaTime, 0));
            currentBaseAngle -= maxBaseDegPerSec * Time.deltaTime;
            isInPos = false;
        }

        if (currentElbowAngle < goalElbowAng - 1)
        {
            upperArm.transform.RotateAround(elbowPivot.transform.position, transform.right, maxArmDegPerSec * Time.deltaTime);
            currentElbowAngle += maxArmDegPerSec * Time.deltaTime;
            isInPos = false;
        }
        else if (currentElbowAngle > goalElbowAng + 1)
        {
            upperArm.transform.RotateAround(elbowPivot.transform.position, transform.right, -maxArmDegPerSec * Time.deltaTime);
            currentElbowAngle -= maxArmDegPerSec * Time.deltaTime;
            isInPos = false;
        }

        if (currentShoulderAngle < goalShoulderAng - 1)
        {
            lowerArm.transform.RotateAround(shoulderPivot.transform.position, transform.right, maxArmDegPerSec * Time.deltaTime);
            currentShoulderAngle += maxArmDegPerSec * Time.deltaTime;
            isInPos = false;
        }
        else if (currentShoulderAngle > goalShoulderAng + 1)
        {
            lowerArm.transform.RotateAround(shoulderPivot.transform.position, transform.right, -maxArmDegPerSec * Time.deltaTime);
            currentShoulderAngle -= maxArmDegPerSec * Time.deltaTime;
            isInPos = false;
        }

        if (currentWristAngle < goalWristAng - 1)
        {
            hand.transform.RotateAround(wristPivot.transform.position, transform.right, maxArmDegPerSec * Time.deltaTime);
            currentWristAngle += maxArmDegPerSec * Time.deltaTime;
            isInPos = false;
        }
        else if (currentWristAngle > goalWristAng + 1)
        {
            hand.transform.RotateAround(wristPivot.transform.position, transform.right, -maxArmDegPerSec * Time.deltaTime);
            currentWristAngle -= maxArmDegPerSec * Time.deltaTime;
            isInPos = false;
        }

        return isInPos;
    }
}
