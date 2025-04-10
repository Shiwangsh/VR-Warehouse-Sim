using UnityEngine;
using UnityEngine.UI;

public class BehaviorControl : MonoBehaviour
{
    public Button behaviorButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        behaviorButton.onClick.AddListener(changeBehavior);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void changeBehavior(){
        if(SceneMng.setting1==1){
            SceneMng.setting1=0;
            SceneMng.setting2=1;
        }else{
            SceneMng.setting1=1;
            SceneMng.setting2=0;
        }
    }
}
