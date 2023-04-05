using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

  


    public void RestartLevel()
    {
        SceneManager.LoadScene("NextLevel");
        Debug.LogError("Restart");
        
    }
    public void QuitGame()
    {
        Debug.LogError("Quit");
        Application.Quit();
    }
}
