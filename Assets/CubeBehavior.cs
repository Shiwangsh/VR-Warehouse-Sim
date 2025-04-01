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
    int lengthSetting=SceneMng.setting1;
    int verticalSetting=SceneMng.setting2;
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
        if (lengthSetting==1){
            SideScrollScript.enabled=true;
        }
        else if (verticalSetting==1){
            lengthScrollScript.enabled=true;
        }
    }
}

