using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public float scoreAmount;
    public float pointIncreasedPerSecond = 2f;
    public float fullPowerPoints = 1f;
    public float gameSpeedPoint = 1f;

    void Start()
    {
        scoreAmount = 0f;
    }

   void Update()
    {
        ScoreCounting();
    }

    void ScoreCounting()
    {

        scoreText.text = Mathf.Round(scoreAmount) + " m";
        scoreAmount += pointIncreasedPerSecond * gameSpeedPoint * fullPowerPoints * Time.deltaTime;
    }
}