using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] ScoreCounter scoreCounter;

    [Header("Text")]
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScore;

    [Header("")]
    public GameObject endGameScreen;
    public GameObject firstButton;

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString() + " m";
        endGameScreen.SetActive(false);
    }

    void FinalScore()
    {
        finalScore.text = (int)scoreCounter.scoreAmount + " m";

        if ((int)scoreCounter.scoreAmount > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int)scoreCounter.scoreAmount);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
   
    }

    public void OnRestart()
    {
        StartCoroutine(RestartTime());
    }
    public void OnQuitGame()
    {
        pauseMenu.OnQuit();
    }

    IEnumerator RestartTime()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("Game");
        AudioController.instance.ToggleMusic();
    }

    public IEnumerator OnGameEnd()
    {
        yield return new WaitForSecondsRealtime(1f);
        endGameScreen.SetActive(true);
        FinalScore();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
