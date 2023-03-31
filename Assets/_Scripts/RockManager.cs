using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    [SerializeField] private GameObject _topRock;
    [SerializeField] private GameObject _botRock;
    

    float gameplayTimer = 0;
    float genTimer = 0;
    float genInterval = 1;

    float moveSpeed;

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
        topObj.GetComponent<Rock>().SetSpeed(speed, 5.1f);
        topObj.transform.localScale = new Vector3(0.8f, randomHeight - 1.5f, 0.8f);


        GameObject botObj = Instantiate(_botRock, new Vector2(10, -5.1f), Quaternion.identity);
        botObj.GetComponent<Rock>().SetSpeed(speed, -5.1f);
        botObj.transform.localScale = new Vector3(0.8f, randomHeight, 0.8f);
    }
}
