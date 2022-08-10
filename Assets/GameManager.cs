using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenuCanvas;
    public GameObject GameOverCanvas;
    public BoolValue GamePaused;
    public PlayerInput Input;
    public float LevelTime;
    public GameObject Timer;
    float _timeLeft;
    const string Gameplay = "Gameplay";
    const string Pause = "Paused";
    bool _gameOver;

    public void Start()
    {
        PauseMenuCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        GamePaused.Value = false;
        _timeLeft = LevelTime;
        Time.timeScale = 1;
        _gameOver = false;
        Input.SwitchCurrentActionMap(Gameplay);
    }

    public void OnPauseGame()
    {
        if (!GamePaused.Value)
        {
            TogglePause(true, PauseMenuCanvas);
        }
    }

    public void OnResume()
    {
        if (GamePaused.Value)
        {
            TogglePause(false, PauseMenuCanvas);
        }
    }

    public void OnQuit()
    {
        if (!_gameOver && GamePaused.Value)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    void TogglePause(bool pause, GameObject pauseOverlay)
    {
        GamePaused.Value = pause;
        pauseOverlay.SetActive(pause);
        Input.SwitchCurrentActionMap(pause ? Pause : Gameplay);
        Time.timeScale = pause ? 0 : 1;
    }
}
