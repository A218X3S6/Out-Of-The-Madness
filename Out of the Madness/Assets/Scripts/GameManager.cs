using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{   
    [SerializeField] ScoreCounter scoreCounter;

    [Header("GameObjects")]
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject startInfo;

    [Header("Animators")]
    [SerializeField] Animator backgroundAnimation;
    [SerializeField] Animator PlayerAnimation;
    [SerializeField] Animator speedLinesAnimation;

    //[HideInInspector] public bool timeScore = false;     //If "startinfo" has disappeared, then start counting (optional)
    [HideInInspector] public bool spacePressed = false;  //Is space pressed? If YES, then "startInfo" is does not show again
    [HideInInspector] public bool gameEnd; // Check if the game is over

    void Start()
    {
        speedLinesAnimation.gameObject.SetActive(false);
        startInfo.SetActive(true);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!spacePressed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startInfo.SetActive(false);
                Time.timeScale = 1f;
                //timeScore = true;
                spacePressed = true;
            }
        }

        if (Time.timeScale == 0)
        {
            if (gameEnd == false)
            {
                AudioListener.pause = true;
            }
        }
        else
        {
            AudioListener.pause = false;
        }

       GamePhases();    
    }
    
    void GamePhases()
    {
        if(scoreCounter.scoreAmount <= 10)
        {
            SpawnConfig.maxTimeBtwSpawns = 4f;
            SpawnConfig.minTimeBtwSpawns = 2f;
        }
        if (scoreCounter.scoreAmount >= 150)
        {        
            PhaseOne();          
        }
        if (scoreCounter.scoreAmount >= 350)
        {
            PhaseTwo();
        }
        if (scoreCounter.scoreAmount >= 750)
        {
            PhaseThree();
        }
        if (scoreCounter.scoreAmount >= 2500)
        {
            PhaseFour();
            speedLinesAnimation.gameObject.SetActive(true);
        }
        if (scoreCounter.scoreAmount >= 5000)
        {
            PhaseFive();
        }
    }

    //  Original Values:
    //  gameSpeedPoint = 1f;
    //  moveSpeed = 2f;
    //  scalingSpeed = 1.5f;
    //  Animations speed = 1f;
    //  maxTimeBtwSpawns = 4f;
    //  minTimeBtwSpawns = 2f;

    void PhaseOne()
    {
        scoreCounter.gameSpeedPoint = 1.6f;
        backgroundAnimation.speed = 1.2f;
        PlayerAnimation.speed = 1.2f;

        if (MoveEnemy.instance != null)
        {
            MoveEnemy.instance.moveSpeed = 2.4f;
            MoveEnemy.instance.scalingSpeed = 2f;
            //MoveEnemy.instance.GetComponent<Animator>().speed = 1.2f; //optional
        }
        SpawnConfig.maxTimeBtwSpawns = 3.5f;
        SpawnConfig.minTimeBtwSpawns = 1.5f;
    }

    void PhaseTwo()
    {
        scoreCounter.gameSpeedPoint = 2.2f;
        backgroundAnimation.speed = 1.4f;
        PlayerAnimation.speed = 1.4f;

        if (MoveEnemy.instance != null)
        {
            MoveEnemy.instance.moveSpeed = 2.8f;
            MoveEnemy.instance.scalingSpeed = 2.25f;
            //MoveEnemy.instance.GetComponent<Animator>().speed = 1.4f; //optional
        }
        SpawnConfig.maxTimeBtwSpawns = 3f;
        SpawnConfig.minTimeBtwSpawns = 1f;
    }

    void PhaseThree()
    {
        scoreCounter.gameSpeedPoint = 2.4f;
        backgroundAnimation.speed = 1.6f;
        PlayerAnimation.speed = 1.6f;

        if (MoveEnemy.instance != null)
        {
            MoveEnemy.instance.moveSpeed = 3.2f;
            MoveEnemy.instance.scalingSpeed = 2.6f;
            //MoveEnemy.instance.GetComponent<Animator>().speed = 1.6f; //optional
        }

        SpawnConfig.maxTimeBtwSpawns = 3f;
        SpawnConfig.minTimeBtwSpawns = 1f;
    }

    void PhaseFour()
    {
        scoreCounter.gameSpeedPoint = 3f;
        backgroundAnimation.speed = 1.8f;
        PlayerAnimation.speed = 1.8f;

        if (MoveEnemy.instance != null)
        {
            MoveEnemy.instance.moveSpeed = 3.6f;
            MoveEnemy.instance.scalingSpeed = 3f;
            //MoveEnemy.instance.GetComponent<Animator>().speed = 1.8f; //optional
        }
        SpawnConfig.maxTimeBtwSpawns = 2.5f;
        SpawnConfig.minTimeBtwSpawns = 0.7f;
    }

    void PhaseFive()
    {
        scoreCounter.gameSpeedPoint = 5f;
        backgroundAnimation.speed = 2f;
        PlayerAnimation.speed = 2f;

        if (MoveEnemy.instance != null)
        {
            MoveEnemy.instance.moveSpeed = 4.2f;
            MoveEnemy.instance.scalingSpeed = 3.5f;
            //MoveEnemy.instance.GetComponent<Animator>().speed = 2f; //optional
        }
        SpawnConfig.maxTimeBtwSpawns = 2f;
        SpawnConfig.minTimeBtwSpawns = 0.35f;
    }


}
