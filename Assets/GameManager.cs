using Assets.state;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    static LevelSetup _nextLevel;
    public GameObject PauseMenuCanvas;
    public GameObject GameOverCanvas;
    public GameObject GameWinCanvas;
    public GameObject IntroHelpCanvas;
    public GameObject NextLevelCanvas;
    public EnemyStateCollection HeckSquirrelStates;
    public BoolValue GamePaused;
    public BoolValue GameWin;
    public PlayerInput Input;
    public GameObject SquirrelPrototype;
    public float LevelTimeInSeconds;
    public GameObject Timer;
    float _timeLeft;
    const string Gameplay = "Gameplay";
    const string Pause = "Paused";
    const string AnyKey = "AnyKey";
    bool _gameOver;
    bool _gameWin;
    TextMeshProUGUI _timer;

    GameObject _pauseOverlay;
    public void Start()
    {
        PauseMenuCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        GameWin.Value = false;
        _timeLeft = _nextLevel?.LevelTimer ?? LevelTimeInSeconds;
        _gameOver = false;
        _gameWin = false;
        _timer = Timer.GetComponent<TextMeshProUGUI>();

        if (_nextLevel == null)
        {
            IntroHelpCanvas.SetActive(true);
            Input.SwitchCurrentActionMap(AnyKey);
            _pauseOverlay = IntroHelpCanvas;
            GamePaused.Value = true;
            Time.timeScale = 0;
        }
        else
        {

            Input.SwitchCurrentActionMap(Gameplay);
            GamePaused.Value = false;
            Time.timeScale = 1;

            for(int i = 1; i < _nextLevel.NumberOfSquirrels; i++)
            {
                var y = Random.Range(-3.33f, 3.33f);
                var x = Random.Range(2f, 7.5f);

                Instantiate(SquirrelPrototype, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
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
            _pauseOverlay = NextLevelCanvas;
            TogglePause(true);
        }
        else
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                _gameOver = true;
                GamePaused.Value = true;
                _nextLevel = null;
                _pauseOverlay = GameOverCanvas;
                TogglePause(true);
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
            _pauseOverlay = PauseMenuCanvas;
            TogglePause(true);
        }
    }

    public void OnResume()
    {
        if (_gameWin)
        {
            GenerateNextLevel();
        }

        if (!_gameOver && !GameWin.Value && GamePaused.Value)
        {
            TogglePause(false);
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

    void TogglePause(bool pause)
    {
        GamePaused.Value = pause;
        _pauseOverlay.SetActive(pause);
        Input.SwitchCurrentActionMap(pause ? (_gameWin ? AnyKey : Pause) : Gameplay);
        Time.timeScale = pause ? 0 : 1;
    }

    void GenerateNextLevel()
    {
        var timer = (_nextLevel?.LevelTimer ?? LevelTimeInSeconds) - 15;
        var squirrels = _nextLevel?.NumberOfSquirrels ?? 1;
        if(timer < 30)
        {
            squirrels++;
            timer = 100 - (squirrels * 5);
        }

        _nextLevel = new LevelSetup
        {
            LevelTimer = timer,
            NumberOfSquirrels = squirrels
        };

        HeckSquirrelStates.Clear();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
