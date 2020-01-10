using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private GUISkin skin;

    void Start()
    {
        skin = Resources.Load("GUISkin") as GUISkin; 
    }
    void OnGUI()
    {
        const int buttonHeight = 60;
        const int buttonWidth = 180;

        GUI.skin = skin;

        if(GUI.Button(new Rect(Screen.width/2 - buttonWidth/2,
            Screen.height/3 - buttonHeight/2, buttonWidth, buttonHeight), "Try again!"))
            {
            SceneManager.LoadScene("SampleScene");
        }

        if (GUI.Button(new Rect(Screen.width / 2 - buttonWidth / 2,
            2*Screen.height / 3 - buttonHeight / 2, buttonWidth, buttonHeight), "Back to menu!"))
        {
            SceneManager.LoadScene("Menu");
        }

    }
}
