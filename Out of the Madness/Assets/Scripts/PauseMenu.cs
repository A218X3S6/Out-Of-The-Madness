using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [Header("Scripts")]
    public GameManager gameManager;
    public EndGameManager endGameManager;

    [Header("UIGameObjects")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject questionPopup;

    [Header("Buttons")]
    public GameObject pauseFirstButton;
    public GameObject optionsFirstButton;
    public GameObject questionFirstButton;

    [HideInInspector] public bool paused = false;

    private bool cliked = false;  // Preventing to bug the UI with mouse

    [Header("Delays")]
    [SerializeField] float sceneLoadDelay = 1f;
    [SerializeField] float closeMenuDelay = 0.5f;

    private void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        questionPopup.SetActive(false);
    }

    void Update()
    {
        NoCursor();
        MouseClick();

        if (gameManager.spacePressed)
        {
            if (gameManager.gameEnd == false)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (pauseMenu.activeInHierarchy)
                    {
                        return;
                    }
                    else
                    {
                        if (!optionsMenu.activeInHierarchy)
                        {
                            cliked = false;
                            PauseUnpause();
                        }
                    }
                }
            }
        }     
    }

    void NoCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseUnpause()
    {
        if (!endGameManager.endGameScreen.activeInHierarchy)
        {

            if (!pauseMenu.activeInHierarchy)
            {
                
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                paused = true;

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);

            }

            // Back to Game with "ESC" (optional)        
            //else
            //{
            //    StartCoroutine(OnResumeDelay());
            //}
        }

    }

    public void OnResume()
    {
        cliked = true;
        StartCoroutine(OnResumeDelay());   
    }

    public void OnOptions()
    {
        StartCoroutine(OptionsMenuDelay());       
    }

    public void OnBackToMenu()
    {
        StartCoroutine(WaitOnNextScene("MainMenu", sceneLoadDelay));
    }
    public void OnQuit()
    {
        StartCoroutine(OnQuitDelay());
    }

    public void OnYes()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OnNo()
    {
        StartCoroutine(QuestionPopupDelay());
    }

    // Preventing to bug the UI with mouse
    void MouseClick()
    {
        if (cliked == false)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            }
        }

        if (!pauseMenu.activeInHierarchy && !questionPopup.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                EventSystem.current.SetSelectedGameObject(optionsFirstButton);
            }
        }

        if (!optionsMenu.activeInHierarchy && questionPopup.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                EventSystem.current.SetSelectedGameObject(questionFirstButton);
            }
        }
    }

    IEnumerator WaitOnNextScene(string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator OnResumeDelay()
    {
        cliked = true;
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForSecondsRealtime(closeMenuDelay);
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
    }

    IEnumerator OptionsMenuDelay()
    {        
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        yield return new WaitForSecondsRealtime(closeMenuDelay);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    
    IEnumerator QuestionPopupDelay()
    {      
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        yield return new WaitForSecondsRealtime(closeMenuDelay);
        questionPopup.SetActive(false);

        if (!endGameManager.endGameScreen.activeInHierarchy)
        {           
            pauseMenu.SetActive(true);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(endGameManager.firstButton);
        }
    }

    public IEnumerator OnQuitDelay()
    {
        EventSystem.current.SetSelectedGameObject(questionFirstButton);
        yield return new WaitForSecondsRealtime(closeMenuDelay);
        EventSystem.current.SetSelectedGameObject(questionFirstButton);
        //pauseMenu.SetActive(false);   // Disable/Enable "PauseMenu" when Question Pop Up is enabled
        questionPopup.SetActive(true);
    }
}
