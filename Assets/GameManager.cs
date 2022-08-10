using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenuCanvas;
    public GameObject GameOverCanvas;
    public GameObject GameWinCanvas;
    public BoolValue GamePaused;
    public BoolValue GameWin;
    public PlayerInput Input;
    public float LevelTimeInSeconds;
    public GameObject Timer;
    float _timeLeft;
    const string Gameplay = "Gameplay";
    const string Pause = "Paused";
    bool _gameOver;
    bool _gameWin;
    TextMeshProUGUI _timer;

    public void Start()
    {
        PauseMenuCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        GamePaused.Value = false;
        GameWin.Value = false;
        _timeLeft = LevelTimeInSeconds;
        Time.timeScale = 1;
        _gameOver = false;
        _gameWin = false;
        _timer = Timer.GetComponent<TextMeshProUGUI>();
        Input.SwitchCurrentActionMap(Gameplay);
    }

    public void Update()
    {
        if (_gameOver)
            return;

        if (_gameWin)
            return;

        if (GameWin.Value)
        {
            _gameWin = true;
            GamePaused.Value = true;
            TogglePause(true, GameWinCanvas);
        }
        else
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                _gameOver = true;
                GamePaused.Value = true;
                TogglePause(true, GameOverCanvas);
            }
        }

        _timer.text = FormatTime(_timeLeft);

    }

    string FormatTime(float time)
    {
        var t = new TimeSpan(0, 0, (int)Mathf.Ceil(time));
        return t.ToString("mm\\:ss");
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
        if (!_gameOver && !GameWin.Value && GamePaused.Value)
        {
            TogglePause(false, PauseMenuCanvas);
        }
    }

    public void OnQuit()
    {
        if (GamePaused.Value)
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
