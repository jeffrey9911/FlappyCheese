using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class StartEvents : MonoBehaviour
{
    

    public void PenguinOnClick()
    {
        GameObject.FindGameObjectWithTag("Respawn").GetComponent<SceneDataManager>().gameMode = 0;
        SceneManager.LoadScene(1);
    }

    public void DuoPlayOnClick()
    {
        GameObject.FindGameObjectWithTag("Respawn").GetComponent<SceneDataManager>().gameMode = 1;
        SceneManager.LoadScene(1);
    }

    public void CheeseOnClick()
    {
        GameObject.FindGameObjectWithTag("Respawn").GetComponent<SceneDataManager>().gameMode = 2;
        SceneManager.LoadScene(1);
    }
}
