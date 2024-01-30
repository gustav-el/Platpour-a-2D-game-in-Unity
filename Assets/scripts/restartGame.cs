using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restartGame : MonoBehaviour
{
    public Button restartButton;

    public void restartLogic()
    {
        SceneManager.LoadScene(0);
    }
}
