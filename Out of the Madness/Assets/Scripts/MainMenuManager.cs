using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{

    private GameObject lastselect;

    [SerializeField] GameObject howToPlayCanvas;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject lastButton;

    [SerializeField] float sceneLoadDelay = 1f;

    void Start()
    {
        buttons.SetActive(true);
        howToPlayCanvas.SetActive(false);
        lastselect = new GameObject();
    }

    private void Update()
    {
        NoCursor();     

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }

        if (howToPlayCanvas.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(OnCloseDelay());
            }
        }
    }

    void NoCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnHowToPlay()
    {
        StartCoroutine(HowToPlayDelay());
    }

    public void NextScene()
    {
        StartCoroutine(WaitOnNextScene("Game", sceneLoadDelay));
    }
    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    IEnumerator WaitOnNextScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator HowToPlayDelay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        howToPlayCanvas.SetActive(true);
        buttons.SetActive(false);
    }

    IEnumerator OnCloseDelay()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        howToPlayCanvas.SetActive(false);
        buttons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(lastButton);
    }
}
