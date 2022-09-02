using Assets.extensions;
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
    public AudioSource MusicSource;
    public AudioSource SFXSource;
    public AudioClip MusicNormal;
    public AudioClip MusicRunningOutOfTime;
    public AudioClip MusicOutOfTimeTransition;
    public AudioClip MusicPause;
    public AudioClip MusicWin;
    public AudioClip MusicLose;
    public AudioClip PauseSFX;
    public AudioClip WinSFX;
    public AudioClip LoseSFX;
    public AudioRequester AudioRequester;

    public EnemyStateCollection HeckSquirrelStates;
    public PlayerState PlayerState;
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
    bool _playedLowTimeJingle;
    TextMeshProUGUI _timer;

    GameObject _pauseOverlay;
    AudioClip _pauseMusic;
    AudioClip _pauseSFX;
    public void Start()
    {
        SetVolumeToSettings();
        PauseMenuCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        GameWin.Value = false;
        _timeLeft = _nextLevel?.LevelTimer ?? LevelTimeInSeconds;
        _gameOver = false;
        _gameWin = false;
        _playedLowTimeJingle = false;
        _timer = Timer.GetComponent<TextMeshProUGUI>();

        if (_nextLevel == null)
        {
            IntroHelpCanvas.SetActive(true);
            Input.SwitchCurrentActionMap(AnyKey);
            _pauseOverlay = IntroHelpCanvas;
            MusicSource.PlayOneShot(MusicWin);
            GamePaused.Value = true;
            Time.timeScale = 0;
        }
        else
        {
            MusicSource.Stop();
            MusicSource.PlayOneShot(MusicNormal);

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
    private void SetVolumeToSettings()
    {
        var musicVolume = PlayerPrefs.GetFloat(AudioRequester.MusicVolumeKey, 0.2f);
        var sfxVolume = PlayerPrefs.GetFloat(AudioRequester.SFXVolumeKey, 0.4f);
        MusicSource.volume = musicVolume;
        SFXSource.volume = sfxVolume;
    }
    public void Update()
    {
        if (_gameOver)
            return;

        if (_gameWin)
            return;

        PlaySounds();

        if (GameWin.Value && !GamePaused.Value)
        {
            _gameWin = true;
            GamePaused.Value = true;
            _pauseOverlay = NextLevelCanvas;
            _pauseMusic = MusicWin;
            _pauseSFX = WinSFX;
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
                _pauseMusic = MusicLose;
                _pauseSFX = LoseSFX;
                TogglePause(true);
            }
            if(_timeLeft < 30f && !_playedLowTimeJingle)
            {
                _playedLowTimeJingle = true;
                MusicSource.Stop();
                MusicSource.loop = false;
                MusicSource.PlayOneShot(MusicOutOfTimeTransition);
                StartCoroutine(MusicSource.WaitForSound(() =>
                {
                    MusicSource.loop = true;
                    MusicSource.PlayOneShot(MusicRunningOutOfTime);
                }));
            }
        }

        _timer.text = FormatTime(_timeLeft);

    }

    void PlaySounds()
    {
        int count = 0;
        while (count < 10 && !AudioRequester.RequestedAudioClips.IsEmpty)
        {
            if (AudioRequester.RequestedAudioClips.TryDequeue(out AudioClip requested))
            {
                SFXSource.PlayOneShot(requested);
                count++;
            }
            else
                break;
        }
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
            _pauseSFX = PauseSFX;
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
            _pauseSFX = PauseSFX;
            TogglePause(false);
            MusicSource.PlayOneShot(_timeLeft < 30f ? MusicRunningOutOfTime : MusicNormal);
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
        MusicSource.Stop();
        Time.timeScale = pause ? 0 : 1;
        if(!(_pauseSFX == null))
            SFXSource.PlayOneShot(_pauseSFX);
        _pauseSFX = null;
        Input.SwitchCurrentActionMap(pause ? (_gameWin ? AnyKey : Pause) : Gameplay);
        if (pause)
        {
            MusicSource.PlayOneShot(_pauseMusic ?? MusicPause);
        }
        else
            _pauseMusic = null;
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
        AudioRequester.RequestedAudioClips.Clear();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
