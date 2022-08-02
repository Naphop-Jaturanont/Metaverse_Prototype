using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text ScoreText;
    public Text HightScore;

    private int Score = 0;
    private int Hightscore = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Hightscore = PlayerPrefs.GetInt("Hightscore", 0);
        ScoreText.text = Score.ToString() + " POINTS";
        HightScore.text = Hightscore.ToString() + " POINTS";
    }

    public void AddPoint()
    {
        Score += 1;
        ScoreText.text = Score.ToString() + " POINTS";
        if (Hightscore < Score)
        {
         PlayerPrefs.SetInt("Hightscore",Score);   
        }
    }
}
