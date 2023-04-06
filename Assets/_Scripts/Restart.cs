using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartOnClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }    
}
