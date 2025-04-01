using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//starting menu script
public class ButtonTransition : MonoBehaviour
{
    
    public Button mybutton;
    public void Start(){
        mybutton.onClick.AddListener(Load);
    }
    public void Load()
    {
        SceneManager.LoadScene("SettingScene");
    }
}
