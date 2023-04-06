using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ColorVary : MonoBehaviour
{
    private TMP_Text text;

    float timer;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 2)
        {
            text.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            timer -= 2;
        }

        
    }
}
