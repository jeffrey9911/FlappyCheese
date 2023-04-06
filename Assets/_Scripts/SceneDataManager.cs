using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataManager : MonoBehaviour
{
    public int gameMode = -1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Respawn");
        if (gameObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        Time.timeScale = 1.0f;
    }
}
