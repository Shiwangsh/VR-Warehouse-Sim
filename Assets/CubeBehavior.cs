using System.ComponentModel;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;

//sample of multivariance based on initalized settings
public class CubeController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private SideScroll SideScrollScript;
    private LengthScroll lengthScrollScript;
    void Start()
    {
        SideScrollScript=GetComponent<SideScroll>();
        SideScrollScript.enabled=false;
        
        lengthScrollScript=GetComponent<LengthScroll>();
        lengthScrollScript.enabled=false;
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (SceneMng.setting1==1){
            lengthScrollScript.enabled=false;
            SideScrollScript.enabled=true;
            
        }
        else if (SceneMng.setting2==1){
            lengthScrollScript.enabled=true;
            SideScrollScript.enabled=false;
        }
    }
}

