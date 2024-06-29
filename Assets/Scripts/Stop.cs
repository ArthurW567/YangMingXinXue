using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    public GameObject StopButton;
    public GameObject ContinueButton;
    public GameObject StartButton;
    public GameObject RestartButton;
    public void OnMouseDown()
    {
        ContinueButton.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Paused);
        StopButton.gameObject.SetActive(false);
    }
}
