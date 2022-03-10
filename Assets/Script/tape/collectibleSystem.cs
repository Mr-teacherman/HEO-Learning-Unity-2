using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectibleSystem : MonoBehaviour
{
    public static collectibleSystem instance;

    public int theScore = 0;
    public Text ScoreText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ScoreText.text = theScore.ToString();
    }
    public void IncreaseScore()
    {
        theScore += 1;
        ScoreText.text = theScore.ToString();
    }
}
