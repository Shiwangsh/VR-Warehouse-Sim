using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor.SearchService;
using UnityEngine.UI;

//uses tokens to enable different setting based on button prompt
public class SceneMng : MonoBehaviour
    { 
        public Button button1, button2, button3;
        public static int setting1=0, setting2=0, setting3=0;
      void Start()
    {
        Debug.Log(setting1);
        button1.onClick.AddListener(HelpOnly);
        button2.onClick.AddListener(HelpOnPrompt);
        button3.onClick.AddListener(NoHelp);
    }
    public void HelpOnly(){
        setting1=1;
        setting2=0;
        setting3=0;
        SceneManager.LoadScene("TestScene");
    }
    public void HelpOnPrompt(){
        setting1=0;
        setting2=1;
        setting3=0;
        SceneManager.LoadScene("TestScene");
    }
    public void NoHelp(){
        setting1=0;
        setting2=0;
        setting3=1;
        SceneManager.LoadScene("TestScene");
    
    }
    }
