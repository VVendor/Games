using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private GUISkin skin;

    void Start()
    {
        skin = Resources.Load("GUISkin") as GUISkin;   
    }

    void OnGUI()
    {
        const int buttonWidth = 84;
        const int buttonHeight = 60;

        GUI.skin = skin;
        Rect buttonRect = new Rect(
            Screen.width / 2 - buttonWidth / 2,
            (2 * Screen.height / 3 - buttonHeight / 2),
            buttonWidth, buttonHeight);
        
        if(GUI.Button(buttonRect, "Start!"))
        {
            SceneManager.LoadScene("SampleScene");
        }        
    }
}
