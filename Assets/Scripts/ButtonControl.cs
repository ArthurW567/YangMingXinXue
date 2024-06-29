using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public GameObject StopButton;
    public GameObject ContinueButton;
    public GameObject StartButton;
    public GameObject RestartButton;
    public static ButtonControl Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void Stop()
    {
        ContinueButton.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Paused);
        StopButton.gameObject.SetActive(false);
    }
    public void Continue()
    {
        StopButton.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        ContinueButton.gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Continue);
    }
    public void Restart()
    {
        StartButton.gameObject.SetActive(true);
        StopButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.GameOver);
    }
    public void Start()
    {
        StartButton.gameObject.SetActive(false);
        StopButton.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }
}

