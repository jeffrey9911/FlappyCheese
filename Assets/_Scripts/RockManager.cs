using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    public static RockManager instance;

    [SerializeField] private GameObject _topRock;
    [SerializeField] private GameObject _botRock;
    [SerializeField] private GameObject _goalArea;

    [SerializeField] private Transform parent;

    //float gameplayTimer = 0;
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
        moveSpeed = 8;
        genInterval = 0.8f;

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

        GameObject topObj = Instantiate(_topRock, parent);
        topObj.transform.localPosition = new Vector2(10, 5.1f);
        topRockHeight = randomHeight - 1.5f;
        topObj.transform.localScale = new Vector3(0.8f, topRockHeight, 0.8f);
        topObj.GetComponent<Rock>().SetSpeed(speed, 5.1f);


        GameObject botObj = Instantiate(_botRock, parent);
        botObj.transform.localPosition = new Vector2(10, -5.1f);
        botRockHeight = randomHeight;
        botObj.transform.localScale = new Vector3(0.8f, botRockHeight, 0.8f);
        botObj.GetComponent<Rock>().SetSpeed(speed, -5.1f);


        Bounds botBounds = botObj.GetComponent<SpriteRenderer>().bounds;
        float botTop = botBounds.size.y;
        botTop += -5.1f;

        GameObject goalArea = Instantiate(_goalArea, parent);
        goalArea.transform.localPosition = new Vector2(10, botTop);
        goalArea.GetComponent<Goal>().SetSpeed(speed, botTop);

    }
}
