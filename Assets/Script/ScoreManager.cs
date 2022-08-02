using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text ScoreText;

    private int Score = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ScoreText.text = Score.ToString() + " POINTS";
    }

    public void AddPoint()
    {
        Score += 1;
        ScoreText.text = Score.ToString() + " POINTS";
    }
}
