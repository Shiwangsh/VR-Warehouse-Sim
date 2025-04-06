using UnityEngine;
using UnityEngine.Rendering;

//arbitrary sample script
public class LengthScroll : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector3 myVector=new Vector3(0,0,1f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+=myVector;
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(myVector.x);
        if(myVector.z>0){
            myVector=new Vector3(0,0,-1f);
            Debug.Log(myVector.x);
        }
         else if(myVector.z<0){
            myVector=new Vector3(0,0,1f);
            Debug.Log(myVector.x);
        }
    }
}
