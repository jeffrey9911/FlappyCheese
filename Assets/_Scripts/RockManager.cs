using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    public static RockManager instance;

    [SerializeField] private GameObject _topRock;
    [SerializeField] private GameObject _botRock;

    [SerializeField] private Transform parent;

    float gameplayTimer = 0;
    float genTimer = 0;
    
    public float genInterval = 1;

    public float moveSpeed = 0f;

    public float topRockHeight = 0f;
    public float botRockHeight = 0f;
    

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    private void Update()
    {
        gameplayTimer += Time.deltaTime;


        switch (gameplayTimer)
        {
            case > 90f:
                moveSpeed = 11;
                genInterval = 0.9f;
                break;

            case > 70f:
                moveSpeed = 9;
                genInterval = 1.0f;
                break;

            case > 40f:
                moveSpeed = 6;
                genInterval = 1.3f;
                break;

            case > 20f:
                moveSpeed = 4;
                genInterval = 1.5f;
                break;

            case > 10f:
                moveSpeed = 3;
                genInterval = 1.8f;
                break;

            default:
                moveSpeed = 2;
                genInterval = 2.0f;
                break;
        }

        if (genTimer >= genInterval)
        {
            GenerateRock(moveSpeed);
            genTimer -= genInterval;
        }

        genTimer += Time.deltaTime;
    }

    private void GenerateRock(float speed)
    {
        float randomHeight = Random.Range(0.2f, 1.3f);

        GameObject topObj = Instantiate(_topRock, new Vector2(10, 5.1f), Quaternion.identity);
        topObj.transform.SetParent(parent);
        topObj.GetComponent<Rock>().SetSpeed(speed, 5.1f);
        topRockHeight = randomHeight - 1.5f;
        topObj.transform.localScale = new Vector3(0.8f, topRockHeight, 0.8f);


        GameObject botObj = Instantiate(_botRock, new Vector2(10, -5.1f), Quaternion.identity);
        botObj.transform.SetParent(parent);
        botObj.GetComponent<Rock>().SetSpeed(speed, -5.1f);
        botRockHeight = randomHeight;
        botObj.transform.localScale = new Vector3(0.8f, botRockHeight, 0.8f);
    }
}
