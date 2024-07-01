using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagering : MonoBehaviour
{
    public void nextScene()
    {
        Debug.Log("clicked");
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
        Debug.Log("click success");
    }

    public void backtoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
