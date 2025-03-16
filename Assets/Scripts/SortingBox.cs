using UnityEngine;

[System.Serializable]
public class SortingBox : MonoBehaviour
{
    [SerializeField] private BoxType.Type boxType;
    [SerializeField] private bool forRobot = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public BoxType.Type GetBoxType()
    {
        return boxType;
    }
}
