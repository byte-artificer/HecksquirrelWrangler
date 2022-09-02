using Assets.extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject LeftSprite;
    public GameObject RightSprite;
    public BoolValue GamePaused;
    public AudioClip CursorMove;
    public AudioClip SelectStart;
    public AudioClip SelectExit;
    public AudioSource MenuAudioSource;
    public AudioSource MusicAudioSource;
    public GameObject MainCanvas;
    public GameObject OptionsCanvas;
    public Button OptionsButton;
    public Slider MusicSlider;
    public Slider SFXSlider;
    float _musicVolume = 0f;
    float _sfxVolume = 0f;
    bool _firstSelect = true;
    bool _suppressSFXSliderSound = false;

    private void Start()
    {
        SetVolumeToSettings();

        MainCanvas.SetActive(true);
        OptionsCanvas.SetActive(false);

    }

    private void SetVolumeToSettings()
    {
        _suppressSFXSliderSound = true;
        _musicVolume = PlayerPrefs.GetFloat(AudioRequester.MusicVolumeKey, 0.2f);
        _sfxVolume = PlayerPrefs.GetFloat(AudioRequester.SFXVolumeKey, 0.4f);
        MusicAudioSource.volume = _musicVolume;
        MenuAudioSource.volume = _sfxVolume;
        MusicSlider.value = _musicVolume;
        SFXSlider.value = _sfxVolume;
        _suppressSFXSliderSound = false;
    }

    public void PlayGame()
    {
        MenuAudioSource.PlayOneShot(SelectStart);
        StartCoroutine(MenuAudioSource.WaitForSound(() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)
        ));
    }

    public void OptionsMenu()
    {
        ToggleOptionsMenu(true);
    }

    public void AdjustSFXVolume()
    {
        if (!_suppressSFXSliderSound)
        {
            MenuAudioSource.volume = SFXSlider.value;
            MenuAudioSource.PlayOneShot(SelectStart);
        }
    }

    public void AdjustMusicVolume()
    {
        MusicAudioSource.volume = MusicSlider.value;
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat(AudioRequester.MusicVolumeKey, MusicSlider.value);
        PlayerPrefs.SetFloat(AudioRequester.SFXVolumeKey, SFXSlider.value);

        ToggleOptionsMenu(false);
    }

    public void CancelOptions()
    {
        SetVolumeToSettings();
        ToggleOptionsMenu(false);
    }

    void ToggleOptionsMenu(bool showOptions)
    {
        MenuAudioSource.PlayOneShot(SelectStart);
        StartCoroutine(MenuAudioSource.WaitForSound(() =>
        {
            MainCanvas.SetActive(!showOptions);
            OptionsCanvas.SetActive(showOptions);
            (showOptions ? (Selectable)MusicSlider : OptionsButton).Select();
        }));
    }

    public void ExitGame()
    {
        MenuAudioSource.PlayOneShot(SelectExit);
        StartCoroutine(MenuAudioSource.WaitForSound(() =>
            Application.Quit(0)
         ));
    }

    public void Select(BaseEventData d)
    {
        if (!_firstSelect)
            MenuAudioSource.PlayOneShot(CursorMove, _sfxVolume);
        else
            _firstSelect = false;
        LeftSprite.transform.position = new Vector3(d.selectedObject.transform.position.x - 1.7f, d.selectedObject.transform.position.y, 0);
        RightSprite.transform.position = new Vector3(d.selectedObject.transform.position.x + 1.85f, d.selectedObject.transform.position.y, 0);
    }

    
}
