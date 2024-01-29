using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// detta skript checkar om spelaren nuddar målstolpen och hanterar bytningen av nivåer
public class passLevel : MonoBehaviour
{
    public GameObject levelPassed;
    public GameObject levelEnvironment;
    public GameObject player;
    public GameObject passLevelButton;
    public GameObject doubleJumpTimer;

    void Start()
    {
      //  passLevelButton.onClick.AddListener(nextLevel);
    }
    
    void Update()
    {

        
    }
    //checkar om vinst saken kolliderar med spelaren
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("vinst");
            levelPassed.SetActive(true);
            levelEnvironment.SetActive(false);
            Time.timeScale = 0;
            doubleJumpTimer.SetActive(false);
        }
    }
    public void nextLevel()
    {
        levelPassed.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        Time.timeScale = 1;
    }

}
