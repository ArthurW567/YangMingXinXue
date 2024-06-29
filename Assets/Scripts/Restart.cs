using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public GameObject StopButton;
    public GameObject ContinueButton;
    public GameObject StartButton;
    public GameObject RestartButton;
    public void OnMouseDown()
    {
        StartButton.gameObject.SetActive(true);
        StopButton.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.GameOver);
    }
}
