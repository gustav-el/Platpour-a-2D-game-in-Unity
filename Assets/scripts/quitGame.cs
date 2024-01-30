using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class quitGame : MonoBehaviour
{
    public Button quitButton;

    public void exitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

}
