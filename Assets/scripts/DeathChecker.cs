using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathChecker : MonoBehaviour
{
    public float maxHeight = 20f;
    public float minimumHeight = -20f;
    public GameObject GameOver;
    public GameObject Player;
    public Transform playerTransform;
    public GameObject doubleJumpTimer;
    public Button restartButton;

    private playerMovement playerMovementScript;
    void Start()
    {
        restartButton.onClick.AddListener(restartGame);
        GameOver.SetActive(false);
    }


    void Update()
    {
        {    //check player posistion to see if it is out of the track
            if (playerTransform.position.y < minimumHeight || playerTransform.position.y > maxHeight)
            {
                //Time.timeScale = 0;
                GameOver.SetActive(true);
                Debug.Log("död");


            }
        }
    }
    bool playerDied()
    {
        if (playerTransform.position.y < minimumHeight || playerTransform.position.y > maxHeight)
        {
            Time.timeScale = 0;
            GameOver.SetActive(true);
            Debug.Log("död");
            return true;

        }
        else
        {
            return false;
        }        
    }

        void restartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
            Debug.Log("restarting");
            playerMovementScript.isGravityNormal = true;

        }
}