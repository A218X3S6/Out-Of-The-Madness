using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    private AudioController audioController;

    [SerializeField] private Slider slider;

    [Header("UIGameObjects")]
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public GameObject pauseFirstButton;

    [Header("")]
    [SerializeField] GameObject pathView;

    private bool activePathView;

    [Header("")]
    [SerializeField] private bool toggleMusic, toggleEffects;

    [Header("")]
    [SerializeField] float closeMenuDelay = 0.5f;

    void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
    }

    void Start()
    {
        if(!PlayerPrefs.HasKey("VolumeSlider"))
        {
            PlayerPrefs.SetFloat("VolumeSlider", 5);
            LoadVolumes();
        }
        else
        {
            LoadVolumes();
        }

        pathView.SetActive(false);
        AudioListener.volume = slider.value;
        audioController.ChangeMusicVolume(slider.value);
        slider.onValueChanged.AddListener(value => audioController.ChangeMusicVolume(value));
    }
    
    public void OnSaveBack()
    {
        StartCoroutine(MenuDelay());
    }

    public void ToggleMusic()
    {
        if (toggleMusic) AudioController.instance.ToggleMusic();
 
    }

    public void ToggleEffects()
    {
        if (toggleEffects) AudioController.instance.ToggleEffect();
    }

    public void ShowPathView()
    {
        activePathView = !activePathView;

        if(activePathView)
        {
            pathView.SetActive(true);
        }
        else
        {
            pathView.SetActive(false);
        }
    }

    IEnumerator MenuDelay()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        float volumeValue = slider.value;
        PlayerPrefs.SetFloat("VolumeSlider", volumeValue);     
        LoadVolumes();
        yield return new WaitForSecondsRealtime(closeMenuDelay);
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }    

    void LoadVolumes()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeSlider");
        slider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}

