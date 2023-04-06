using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TMP_Text _scoreTextPenguin;
    [SerializeField] private TMP_Text _scoreTextCheese;

    [SerializeField] private GameObject _winLosePanel;
    [SerializeField] private TMP_Text _winLoseText;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    private int penguinScore = 0;
    private int cheeseScore = 0;

    float cheeseTimer = 0;

    private void Update()
    {
        cheeseTimer += Time.deltaTime;

        if(cheeseTimer >= 10)
        {
            CheeseScore(1);
            cheeseTimer -= 10;
        }

        if(penguinScore >= 50 || cheeseScore <= -10)
        {
            _winLosePanel.SetActive(true);
            _winLoseText.text = "Penguin Win!";
            Time.timeScale = 0.0f;
        }

        if(cheeseScore >= 50 || penguinScore <= -10)
        {
            _winLosePanel.SetActive(true);
            _winLoseText.text = "Cheese Win!";
            Time.timeScale = 0.0f;
        }
    }

    public void CheeseScore(int score)
    {
        cheeseScore += score;
        _scoreTextCheese.text = cheeseScore.ToString() + " : Cheese";
    }

    public void PenguinScore(int score)
    {
        penguinScore += score;
        _scoreTextPenguin.text = "Penguin : " + penguinScore.ToString();
    }
}
