using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneControler.sceneControler.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
