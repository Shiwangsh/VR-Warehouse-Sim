using UnityEngine;

[System.Serializable]
public class SortingBox : MonoBehaviour
{
    [SerializeField] public BoxType.Type boxType;
    [SerializeField] public bool forRobot = false;

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
