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
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsMenu()
    {
        //TODO
    }

    public void ExitGame()
    {
        Application.Quit(0);
    }

    public void Select(BaseEventData d)
    {
        LeftSprite.transform.position = new Vector3(LeftSprite.transform.position.x, d.selectedObject.transform.position.y, 0);
        RightSprite.transform.position = new Vector3(RightSprite.transform.position.x, d.selectedObject.transform.position.y, 0);
    }
}
