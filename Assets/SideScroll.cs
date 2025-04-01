using UnityEngine;
using UnityEngine.Rendering;

//arbitrary sample script
public class SideScroll : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector3 myVector=new Vector3(0.001f,0,0);
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
        if(myVector.x>0){
            myVector=new Vector3(-0.001f,0,0);
            Debug.Log(myVector.x);
        }
         else if(myVector.x<0){
            myVector=new Vector3(0.001f,0,0);
            Debug.Log(myVector.x);
        }
    }
}
