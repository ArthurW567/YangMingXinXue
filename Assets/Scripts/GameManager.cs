using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Naninovel;

public enum GameState
{
    Playing,
    GameOver,
    Paused,
    Victory,
    Continue
}


public class GameManager : MonoBehaviour
{
    public GameObject close;
    public GameObject open;
    public static GameManager Instance;
    public GameObject disturbanceGenerator;
    public GameState gameState;
    //生成器

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.Paused);
    }
    public IEnumerator CameraA()
    {
        float FieldView = Camera.main.orthographicSize;
        for (; FieldView > 1.8f; FieldView -= 0.5f*Time.deltaTime)
        for (; FieldView > 1.8f; FieldView -= 0.2f)
        for (; FieldView > 1.8f; FieldView -= 0.5f * Time.deltaTime)
        {
            Camera.main.orthographicSize = FieldView;
            yield return null;
        }

    }
    void HandleVictory()
    {
        Debug.Log("Victory!");
        LevelManager.Instance.Victory();
        Player.falseTime = -99;
        var service = Engine.GetService<ICustomVariableManager>();
        StartCoroutine("CameraA");
        open.SetActive(true);
        close.SetActive(false);
        service.SetVariableValue("level", LevelManager.Instance.GetLevel().ToString());
    }

    void HandleFail()
    {
        Debug.Log("Fail!");
        LevelManager.Instance.Fail();
    }
	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.P))
            UpdateGameState(GameState.Victory);
        if (Input.GetKeyDown(KeyCode.L))
            UpdateGameState(GameState.GameOver);
    }


	public void UpdateGameState(GameState newState)
    {
        gameState = newState;
        switch (newState)
        {
            case GameState.Playing:
                LevelManager.Instance.GenerateSerial();
                disturbanceGenerator.SetActive(true);
                Time.timeScale = 1;
                break;
            case GameState.GameOver:
                //Time.timeScale = 0;
                disturbanceGenerator.SetActive(false);
                DisturbanceGenerator.Instance.Reset();
                HandleFail();
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.Continue:
                Time.timeScale = 1;
                break;
            case GameState.Victory:
                disturbanceGenerator.SetActive(false);
                DisturbanceGenerator.Instance.Reset();
                HandleVictory();
                break;
        }
    }
}
