using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject LeftSprite;
    public GameObject RightSprite;
    public BoolValue GamePaused;
    public AudioClip CursorMove;
    public AudioClip SelectStart;
    public AudioClip SelectExit;
    public AudioSource MenuAudioSource;
    bool firstSelect = true;

    public void PlayGame()
    {
        MenuAudioSource.PlayOneShot(SelectStart);
        StartCoroutine(waitForSound(MenuAudioSource, () =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)
        ));
    }

    public void OptionsMenu()
    {
        //TODO
    }

    public void ExitGame()
    {
        MenuAudioSource.PlayOneShot(SelectExit); 
        StartCoroutine(waitForSound(MenuAudioSource, () =>
            Application.Quit(0)
         ));
    }

    public void Select(BaseEventData d)
    {
        if (!firstSelect)
            MenuAudioSource.PlayOneShot(CursorMove);
        else
            firstSelect = false;
        LeftSprite.transform.position = new Vector3(LeftSprite.transform.position.x, d.selectedObject.transform.position.y, 0);
        RightSprite.transform.position = new Vector3(RightSprite.transform.position.x, d.selectedObject.transform.position.y, 0);
    }

    IEnumerator waitForSound(AudioSource source, Action callback)
    {
        while (source.isPlaying)
            yield return null;

        callback();
    }
}
